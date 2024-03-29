﻿using CsvHelper;
using CsvHelper.Configuration;
using Darren.Base.Model;
using Darren.Base.Model.XmlModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using v2cshtml.Models;
using v2cshtml.Services.Interface;

namespace v2cshtml.Services
{
    public class ValidateFileService : IValidateFileService
    {
        private readonly int MAX_FILE_SIZE = 1024 * 1024;
        private readonly String[] SUPPORTED_EXTENSIONS = { ConstValues.CSV, ConstValues.XML };
        private readonly IUploadFileService iupload;
        private readonly IEnqueueService iqueue;
        public ValidateFileService(IUploadFileService _iupload, IEnqueueService _iqueue)
        {
            iupload = _iupload;
            iqueue = _iqueue;
        }

        public async Task<ValidateFileResponseModel> ValidateFile(IFormFile file1)
        {
            String ext = file1.FileName.Split('.').Last().ToUpper();
            ValidateFileResponseModel vfrm = ValidateFileExt(ext, file1);
            vfrm = ValidateSize(vfrm, file1);
            if (ext == ConstValues.CSV)
            {
                vfrm = await ValidateCsvColumn(ext, vfrm, file1);
            }else if (ext == ConstValues.XML)
            {
                vfrm = await ValidateXmlColumn(ext, vfrm, file1);
            }
            return vfrm;
        }
        public ValidateFileResponseModel ValidateFileExt(String ext, IFormFile file1)
        {
            ValidateFileResponseModel vfrm = new ValidateFileResponseModel()
            {
                Success = true,
                ErrorMessage = String.Empty,
            };
            if (file1 == null)
            {
                vfrm.Success = false;
                vfrm.ErrorMessage += "File is empty!\r\n";
            }
            if (file1 != null && !SUPPORTED_EXTENSIONS.Contains(ext))
            {
                vfrm.Success = false;
                vfrm.ErrorMessage += "Unknown format\r\n";
            }
            return vfrm;
        }

        public ValidateFileResponseModel ValidateSize(ValidateFileResponseModel vfrm, IFormFile file1)
        {
            if (file1 == null || file1.Length == 0)
            {
                vfrm.Success = false;
                vfrm.ErrorMessage += "File is empty!\r\n";
            }
            if (file1 != null && file1.Length > MAX_FILE_SIZE)
            {
                vfrm.Success=false;
                vfrm.ErrorMessage += "Max file size is 1MB\r\n";
            }
            return vfrm;
        }
        public async Task<ValidateFileResponseModel> ValidateCsvColumn(String ext, ValidateFileResponseModel vfrm, IFormFile file1) { 

            if(file1 != null && ext == ConstValues.CSV)
            {
                try
                {
                    await ValidateColumnGroomData(ext, file1, 1);
                }
                catch(Exception e)
                {
                    vfrm.Success = false;
                    vfrm.ErrorMessage += e.Message;
                }
            }
            return vfrm;
        }
        public async Task<ValidateFileResponseModel> ValidateXmlColumn(String ext, ValidateFileResponseModel vfrm, IFormFile file1) { 

            if(file1 != null && ext == ConstValues.XML)
            {
                try
                {
                    await ValidateColumnGroomData(ext, file1, 1);
                }
                catch(Exception e)
                {
                    vfrm.Success = false;
                    vfrm.ErrorMessage += e.Message;
                }
            }
            return vfrm;
        }
        public async Task ValidateColumnGroomData(String ext, IFormFile file1, int dataGroomingLevel = 0)
        {
            String fileguid1 = Guid.NewGuid().ToString() + "." + ext;
            if (ext == ConstValues.CSV) await ValidateCsvColumnGroomData(fileguid1, ext, file1, dataGroomingLevel);
            else /* if (ext == ConstValues.XML) */ await ValidateXmlColumnGroomData(fileguid1, ext, file1, dataGroomingLevel);
        }
        public async Task ValidateXmlColumnGroomData(String fileguid1, String ext, IFormFile file1, int dataGroomingLevel = 0)
        {
            if(dataGroomingLevel > 0)
            {
                string wholeXml = string.Empty;
                var result = new StringBuilder();
                using (var reader = new StreamReader(file1.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        string theLine = reader.ReadLine();
                        //replace unicode quote and bracket
                        theLine = theLine.Replace("“", "\"");
                        theLine = theLine.Replace("”", "\"");
                        theLine = theLine.Replace("《", "<");
                        theLine = theLine.Replace("》", ">");
                        result.AppendLine(theLine);
                    }
                    wholeXml = result.ToString();
                }

                if (string.IsNullOrWhiteSpace(wholeXml))
                {
                    throw new Exception("Wrong xml file format. ");
                }
                else
                {
                    XmlRootAttribute xRoot = new XmlRootAttribute();
                    xRoot.ElementName = "Transactions";
                    xRoot.IsNullable = true;

                    var serializer = new XmlSerializer(typeof(TransactionsXML), xRoot);
                    StringReader reader = new StringReader(wholeXml);
                    using (XmlReader xmlReader = XmlReader.Create(reader))
                    {
                        TransactionsXML resultObj = (TransactionsXML)serializer.Deserialize(xmlReader);
                        if (resultObj != null) { 
                            foreach (TransactionXML tran in resultObj.Transaction)
                            {
                                tran.PaymentDetails.Amount = GroomColumns("amount", tran.PaymentDetails.Amount, 1);
                                AssertColumns("transactionid", tran.Id, 1);
                            }
                            using (var memStream = new MemoryStream())
                            {
                                serializer.Serialize(memStream, resultObj);
                                byte[] filebyte = memStream.ToArray();
                                String uploadUri = await iupload.WriteToStorageReturnUri(fileguid1, true, filebyte);
                            }
                        }
                    }
                }

            }
        }

        public async Task ValidateCsvColumnGroomData(String fileguid1, String ext, IFormFile file1, int dataGroomingLevel = 0)
        {
            if(dataGroomingLevel == 0)
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = ",",
                };
                using (var reader = new StreamReader(file1.OpenReadStream()))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<CsvTransactionItem>().ToList();
                }
                String uploadUri = await iupload.WriteToStorageReturnUri(fileguid1, true, file1);
            }
            if(dataGroomingLevel > 0)
            {
                List<CsvTransactionItem> lsCsv = new List<CsvTransactionItem>();
                if (dataGroomingLevel == 1)
                {
                    using (var reader = new StreamReader(file1.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                        {
                            string theLine = reader.ReadLine();
                            //replace unicode quote and comma
                            theLine = theLine.Replace("“", "\"");
                            theLine = theLine.Replace("”" , "\"");
                            theLine = theLine.Replace("，", ",");
                            //remove whitespace between quote and comma
                            theLine = Regex.Replace(theLine, "[\"][\\s]+[,]", "\",");
                            theLine = Regex.Replace(theLine, "[,][\\s]+[\"]", ",\"");

                            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                            {
                                HasHeaderRecord = false,
                                Delimiter = ",",
                            };
                            using (var lineReader = new StringReader(theLine))
                            using (var csvReader = new CsvReader(lineReader, config))
                            {
                                var records = csvReader.GetRecords<CsvTransactionItem>().ToList();
                                records[0].Amount = GroomColumns("amount", records[0].Amount, 1);

                                AssertColumns("transactionid", records[0].TransactionId, 1);
                                lsCsv.Add(records[0]);
                            }
                        }
                        //after go through data grooming then upload to good-file in grommed format

                        using (var memoryStream = new MemoryStream())
                        {
                            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                            {
                                HasHeaderRecord = false,
                                Delimiter = ",",
                            };
                            using (var streamWriter = new StreamWriter(memoryStream))
                            using (var csvWriter = new CsvWriter(streamWriter, config))
                            {
                                csvWriter.WriteRecords<CsvTransactionItem>(lsCsv);
                            }

                            byte[] fileByte = memoryStream.ToArray();
                            string uploadUri = await iupload.WriteToStorageReturnUri(fileguid1, true, fileByte);
                            bool queueResponse = await iqueue.EnqueueFile(uploadUri,file1.FileName, ext, true);
                        }
                    }
                }
            }
        }

        public string GroomColumns(string columnName, string oriValue, int groomLevel = 0)
        {
            string ret = oriValue;
            if (groomLevel > 0)
            {
                if (columnName.ToLower() == "amount")
                {
                    ret = oriValue.Replace(",", string.Empty);
                }
                else if (columnName.ToLower() == "status")
                {
                    if(oriValue.ToUpper() == "APPROVED" ) ret = oriValue.ToUpper().Replace("APPROVED", "A");
                    if(oriValue.ToUpper() == "FAILED"   ) ret = oriValue.ToUpper().Replace("FAILED"  , "R");
                    if(oriValue.ToUpper() == "REJECTED" ) ret = oriValue.ToUpper().Replace("REJECTED", "R");
                    if(oriValue.ToUpper() == "FINISHED" ) ret = oriValue.ToUpper().Replace("FINISHED", "D");
                    if(oriValue.ToUpper() == "DONE"     ) ret = oriValue.ToUpper().Replace("DONE"    , "D");
                }
            }
            return ret;
        }

        public void AssertColumns(string columnName, string oriValue, int assertLevel = 0)
        {
            int ASSERT_MAX_TRXID_LEN = 50;
            if(assertLevel > 0)
            {
                if(columnName.ToLower() == "transactionid" 
                    && oriValue.Length > ASSERT_MAX_TRXID_LEN)
                {
                    throw new Exception("Transaction Id cannot be more than 50 characters!");
                }
            }
            //more asserts to be implemented on : amount format, currency iso 4217, date validation, no future date time by UTC, status enumeration
        }

    }
}
