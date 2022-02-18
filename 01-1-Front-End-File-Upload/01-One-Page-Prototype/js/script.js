$(function(){

    $("#transactionCsvXml").change(function(){
        let theFile = this.files[0];
        ValidateFile(theFile);
    });
});

function ValidateFile(theFile){
    let form = new FormData();
    let fileName = theFile.name;
    let fileExt = fileName.split('.').pop();
    form.append("FileUpload", theFile);
    form.append("FileName", fileName.split(fileExt)[0]);
    form.append("FileExtension", fileExt);

    var settings = {
    "url": "https://localhost:44314/api/v1/Validation/validateFile",
    "method": "POST",
    "timeout": 0,
    "headers": {
        "accept": "*/*"
    },
    "processData": false,
    "mimeType": "multipart/form-data",    
    "cache": false,
    "contentType": false,
    "data": form
    };

    $.ajax(settings).done(function (response) {
        console.log(response);
    })
    .fail(function(response){
        console.log("fail", response);
    });
}
