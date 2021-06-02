
function SearchInstrument_delete() {
    $("#CertificateDetails_InstrumentCode")({
        source: function (request, response) {
            $.ajax({
                url: '/api/instrument/searchinstrument',
                data: "{ 'searchInstrumentTerm': '" + request.term + "'}",
                dataType: "json",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#CertificateDetails_InstrumentCode").val(i.item.val);
        },
        minLength: 1
    });
}

function GetAccountDetails() {

    var accountCode = $('#CertificateDetails_AccountNumber').val();
    var url = $("#CertificateDetails_AccountNumber").data("url");
    $("#CertificateDetails_AccountNumber").val('');
    if (accountCode.length > 2) {
        $.ajax({
            beforeSend: function () {
                $("#loader").show();

            },
            complete: function () {
                $("#loader").hide();
            },
            type: "GET",
            url: url,
            data: { "accountNumber": accountCode },
            //contentType: "application/json",
            dataType: 'json',
            success: function (response) {

                if (response === null || response.AccountName === "" || response.AccountName === null) {
                    //$('#accountErrorAlert').show();
                    //$('#accountErrorAlertText').text('Invalid Account!').show();
                    //$("#submitButton").attr("disabled", true);
                    alert("Account not found");
                    $("#CertificateDetails_AccountName").val("");
                    $("#CertificateDetails_AccountType").val("");
                    $("#CertificateDetails_InvestmentService").val("");
                }
                else {
                    $("#CertificateDetails_AccountNumber").val(response.AccountCode);
                    $("#CertificateDetails_AccountName").val(response.AccountName);
                    $("#CertificateDetails_AccountType").val(response.AccountType);
                    $("#CertificateDetails_InvestmentService").val(response.InvestmentService);

                    $('#accountErrorAlert').hide();
                    $('#accountErrorAlert').hide();
                    $('#submitButton').removeAttr("disabled");
                    //GetInstrumentDetails();
                }

                ValidateTextField('CertificateDetails_AccountName');
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
}

function ValidateInstrumentInformation() {

    if (ValidateTextField('CertificateDetails_InstrumentCode') && ValidateTextField('CertificateDetails_InstrumentName') && ValidateTextField('CertificateDetails_Exchange')) {
        return true;
    } else {
        return false;
    }
}

function ValidateAccountInformation() {

    if (ValidateTextField('CertificateDetails_AccountName') && ValidateTextField('CertificateDetails_AccountName')) {
        return true;
    } else {
        return false;
    }
}

function ValidateInput() {

    var isValid = false;
    var isInstrumentInfoValid = false;
    var isAccountInfoValid = false;

    var a = ValidateTextField('CertificateDetails_InstrumentCode');
    var b = ValidateTextField('CertificateDetails_InstrumentCode');
    var c = ValidateTextField('CertificateDetails_Exchange');
    var d = ValidateTextField('CertificateDetails_InstrumentName');
    var e = ValidateTextField('CertificateDetails_AccountNumber');
    var f = ValidateTextField('CertificateDetails_AccountName');
    var g = ValidateTextField('CertificateDetails_InvestorHolderNumber');
    var h = ValidateTextField('CertificateDetails_CerificateNumber');
    var i = ValidateTextField('CertificateDetails_Volume');
    //var j = ValidateTextField('CertificateDetails_AccountNumberRecordedOnCertificate');
    //var k = ValidateTextFieldIsNumeric('CertificateDetails_Volume');
    var k = ValidateTextField('CertificateDetails_Volume');
    var l = ValidateSelectCombo('CertificateDetails_InterestInstruction');
    //ValidateTextField('CertificateDetails_MaturityDate');
    //ValidateTextField('CertificateDetails_AccountType');
    //ValidateTextField('CertificateDetails_InterestInstruction');
    //ValidateTextField('CertificateDetails_Status');
    //ValidateTextField('CertificateDetails_InterestRate');
    //ValidateTextField('CertificateDetails_Comments');  

    if (a && b && c && d && e && f && g && h && i && k && l) {
        isValid = true;
    }


    return isValid;
}

function ValidateTextFieldIsNumeric(fieldId) {
    var isValid = false;
    if ($.isNumeric($("#CertificateDetails_Volume1").val())) {
        isValid = true;
        ValidField(fieldId);
    } else {
        HighlightInvalidField(fieldId);
    }

    return isValid;
}

var onStartSuccess = function (result) {
    if (result.status === "Success") {
        iziToast.success({
            title: 'Submitted Successfully!',
            message: 'Certificate has been added to the Register!'
        });
        $('#certErrorAlert').hide();
        $("#hfEditPageUrl").val(result.editUrl)

    }
    else if (result.status === "CodeError") {
        iziToast.error({
            title: 'An error occurred while adding certificate !',
            message: 'It has been sent to <a href="mailto: development@forsythbarr.co.nz">Development</a> for review. Please contact <a href="mailto: development@forsythbarr.co.nz">Development</a> should you wish to discuss.'
        });
    }
    else {
        $("#certErrorAlertText").html(result.message);
        $('#certErrorAlert').show();
    }
}

var onStartSuccessUpdated = function (result) {
    if (result.status === "Success") {
        iziToast.success({
            title: 'Updated Successfully!',
            message: 'Certificate has been updated successfully!'
        });
        $('#certErrorAlert').hide();
        $("#hfEditPageUrl").val(result.editUrl)

    }
    else if (result.status === "CodeError") {
        iziToast.error({
            title: 'An error occurred while updating certificate !',
            message: 'It has been sent to <a href="mailto: development@forsythbarr.co.nz">Development</a> for review. Please contact <a href="mailto: development@forsythbarr.co.nz">Development</a> should you wish to discuss.'
        });
    }
    else {
        $("#certErrorAlertText").html(result.message);
        $('#certErrorAlert').show();
    }
}

function DeleteRecord() {

    return confirm("Do you really want to delete this record?");
   
}

function fetchHistory(certificateIdd) {
    ////alert("Hello " + certificateIdd);
    $("#divCertHistory").hide(500);
    $("#tabHistory tbody tr").remove(); 
    $("#divCertHistory").show(500);    

    $.ajax({
        url: 'api/certificate/gethistory',
        type: 'POST',
        dataType: 'json',
        data: { certificateId:  certificateIdd  },
        success: function (list) {
            if (list == "fail") {
                alert('error');
            }
            else {
                if (list.length) {
                   // $("#titreAppend").append("<h3 style='margin-top:55px'>Object Appel d'Offre  : [ <u style='color:#f43030'><b> " + list[0].Objet + "</b></u> ] </h3>");
                }
                $.each(list, function (i) {
                    
                    $("#tabHistory tbody").append("<tr>" +                       
                        "<td>" + list[i].InstrumentCode + "</td>" +
                        "<td>" + list[i].InstrumentName + "</td>" +
                        "<td>" + list[i].AccountNumber + "</td>" +
                        "<td>" + list[i].CerificateNumber + "</td>" +
                        "<td><span class='decimal-with-comma'>" + list[i].Volume + "</span></td>" +
                        "<td>" + moment(list[i].MaturityDate).format("DD/MM/YYYY") + "</td>" +
                        "<td>" + moment(list[i].UpdatedDate).format("DD/MM/YYYY LTS") + "</td>" +
                        "</tr>");
                })
            }
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    })

    return false;
}

function HideHistory(certificateIdd) { 
    $("#divCertHistory").hide(500);
    return false;
}
















