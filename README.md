# technical-assignment-20220218

This is a CSHTML Web Application to upload International Transaction Data for Currencies

Third Party Tools To Install:
1. Azurite Emulator
1. MS SQL Express 2019
1. MS SQL Management Studio
1. Visual studio extension: Azure Functions and Web Jobs Tools

How to run this project:
1. use Visual Stido 2022, open 05-V2-Cshtml/v2cshtml/v2cshtml.sln
1. ensure that v2cshtml project is set as default project
1. run v2cshtml project (both are fine for ISS Express, or console) 

User Guide:
1. at the welcome page click button for Choose A File
1. you can choose csv or xml with the format according to Technical-Assignment.pdf
1. click upload
1. if your file has passed validation, it would be queue for processing
1. if fail validation, then you will see error message(s)

Overall flow:
1. user uploads a file
1. cshtml page and controller grooms the data
1. validation is done on the groomed data
1. validation passed, groomed file will uploaded to one of these csv-good-file, xml-good-file blob folders
1. validation failed, original file will be uploaded to one of these csv-bad-file, xml-bad-file blob folders
1. once uploaded, the blob file has a URI
1. file URI and original file name are send to azurite queue
1. the queue message is picked up by azure function
1. azure function reads from one of these queues: csv-good-file, csv-bad-file, xml-bad-file, xml-good-file
1. azure function processes the file by reading from Uri of the queue item
1. the Uri is the csv or xml file itself, azure function process the rows in the file
1. good-file generally get stored in Transaction

ERD Descriptions:
1. ProcessingStageCsv, ProcessingStageXml, TransactionRecord, 
1. DirtyFile, DirtyRecordXml, DirtyRecordCsv

Other Assumptions:
1. there are many more items to consider in order to fulfill production readiness: CICD, API Gateway, CDN etc
1. the production considerations serve as a discussion
1. azure functions technology is used due its serverless nature. azure function can be triggered by azure queue 
1. azure function based on queue, and on-demand cloud resource as compared to webjob always-on cloud resource, therefore azure functions is chosen.
1. original transaction dates are not required to be stored in db. Instead, the datetime are stored in db column with one single format only.

