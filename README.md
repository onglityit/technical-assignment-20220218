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
1. azure function is triggered by new upoaded file in these blob folders: good-file, bad-file
1. azure function process the rows in the file
1. good-file generally get stored in Table transactionrecord

ERD Descriptions:
1. fileblobdata - contains fileguid, bloburi metadata, extension, and good/bad status
1. transactionrecord - contains the transaction record. this is clean data
1. badfileline - contains the lines of bad files

Other Assumptions:
1. for unknow format, the file will not be stored
1. assumption is made that unknown format does not help in identifying data error
1. log file will store the line in nvarchar(1000), longer than that it would be truncated
1. for those good file, original transaction dates are not required to be stored in db. Instead, the datetime are stored in db column with one single format only.
1. for bad files, original transaction dates format are in-tact so when troubleshooting, the erroneous format can be seen
1. take note that azure function local.settings.json is ignored by git
1. cost of running in cloud is to be kept as another topic, since this project is to be run locally first
1. JWT token issuance is done by v2-api-search, the same api that is used for query. this is against best practice. in order to achieve best practice, a centralized authentication authorization need to be used, 
for example API Gateway with MFA.