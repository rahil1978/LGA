

function ValidateInputForRegistryHoldings() {

    var isValid = false;

    var pageMode = $('#hfSavingHolding').val();

    var a = ValidateTextField('RegistryHoldingDetails_InstrumentCode');
    var b = ValidateTextField('RegistryHoldingDetails_Exchange');
    var c = ValidateTextField('RegistryHoldingDetails_InstrumentName');

    //for certificate
    var d = ValidateTextField('RegistryHoldingDetails_RegistryTaxGroup');
    var e = ValidateDropdownList('RegistryHoldingDetails_AccountType');   

    //for csn      
    var f = ValidateDropdownList('RegistryHoldingDetails_CSN');   
    var g = ValidateTextField('RegistryHoldingDetails_SeAccountType');  

    var h = ValidateTextField('RegistryHoldingDetails_Volume');
   
    
    if (pageMode.toUpperCase() === 'TRUE') {
        if (a && b && c && d && e && h ) {
            isValid = true;
        }
    }
    else if (pageMode.toUpperCase() === 'FALSE') {
        if (a && b && c  && f && g && h ) {
            isValid = true;
        }

    }

    return isValid;
}

function GetAccountTypeForSelectedCSN() {

    var selectedCSN = $('#RegistryHoldingDetails_CSN').val();
    var url = $("#RegistryHoldingDetails_CSN").data("url");    
    if (selectedCSN.length > 2) {
        $.ajax({
            beforeSend: function () {
                $("#loader").show();
            },
            complete: function () {
                $("#loader").hide();
            },
            type: "GET",
            url: '../api/registry/GetAccountTypeByCsnNumber',
            data: { "csn": selectedCSN },
            //contentType: "application/json",
            dataType: 'json',
            success: function (response) {

                if (response === null || response.accountType === "" || response.accountType === null) {                   
                    alert("CSN not found in mapping table");
                    $("#RegistryHoldingDetails_SeAccountType").val("");                    
                }
                else {
                    $("#RegistryHoldingDetails_SeAccountType").val(response.accountType);                    
                    
                    $('#submitButton').removeAttr("disabled");                    
                }

                ValidateTextField('RegistryHoldingDetails_SeAccountType');
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
}

var onStartSuccess = function (result) {
    if (result.status === "Success") {
        iziToast.success({
            title: 'Submitted Successfully!',
            message: 'Registry Holding has been added to the Register!'
        });
        $('#certErrorAlert').hide();
        $("#hfEditPageUrl").val(result.editUrl)

        //disabling the controls
        $(":button").prop('disabled', true);
        $('#RegistryHoldingDetails_InstrumentCode').attr('readonly', true);
        $('#RegistryHoldingDetails_RegistryTaxGroup').attr('readonly', true);
        $('#RegistryHoldingDetails_Volume').attr('readonly', true);
        $('#RegistryHoldingDetails_AdminNotes').attr('readonly', true);

        $('#RegistryHoldingDetails_RegistryTaxGroup').attr('readonly', true);
        $('#RegistryHoldingDetails_AccountType').attr('readonly', true);
        $('#RegistryHoldingDetails_CSN').attr('readonly', true);
        $('#RegistryHoldingDetails_SeAccountType').attr('readonly', true);

        //$('#RegistryHoldingDetails_SeAccountType').attr('readonly', true);

        $('#rbCertificate').attr('disabled', true);
        $('#rbCSN').attr('disabled', true);
             

    }
    else if (result.status === "CodeError") {
        iziToast.error({
            title: 'An error occurred while adding the registry holding!',
            message: 'It has been sent to <a href="mailto: development@forsythbarr.co.nz">Development</a> for review. Please contact <a href="mailto: development@forsythbarr.co.nz">Development</a> should you wish to discuss.'
        });
    }
    else {
        $("#certErrorAlertText").html(result.message);
        $('#certErrorAlert').show();
    }
}

var onStartSuccessForEdit = function (result) {
    if (result.status === "Success") {
        iziToast.success({
            title: 'Updated Successfully!',
            message: 'Registry Holding record has been Updated!'
        });
        $('#certErrorAlert').hide();
        $("#hfEditPageUrl").val(result.editUrl)

    }
    else if (result.status === "CodeError") {
        iziToast.error({
            title: 'An error occurred while updating Registry holding!',
            message: 'It has been sent to <a href="mailto: development@forsythbarr.co.nz">Development</a> for review. Please contact <a href="mailto: development@forsythbarr.co.nz">Development</a> should you wish to discuss.'
        });
    }
    else {
        $("#certErrorAlertText").html(result.message);
        $('#certErrorAlert').show();
    }
}

var onInsertComplete = function () {
    $("#loader").hide();
    $('#divSumbit').hide();
    $('#divEdit').show();
    $('#editButton').attr('disabled', false);;


}

var onUpdateComplete= function () {   
    $("#loader").hide();
    $('#divSumbit').hide();
    $('#divEdit').show();
    $('#editButton').attr('disabled', false);

}


function DeleteRecord() {

    return confirm("Do you really want to delete this record?");

}
