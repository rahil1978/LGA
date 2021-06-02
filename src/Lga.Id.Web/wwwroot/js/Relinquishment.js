
function GetAccountDetailForRelinquishment() {

    var accountCode = $('#RelinquishmentDetail_AccountNumber').val();
    var url = $("#RelinquishmentDetail_AccountNumber").data("url");
    $("#RelinquishmentDetail_AccountNumber").val('');
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
                    alert('Account not found!');                  
                    $("#RelinquishmentDetail_AccountName").val("");
                    $("#RelinquishmentDetail_AccountType").val("");
                    $("#RelinquishmentDetail_InvestmentService").val("");
                }
                else {
                    $("#RelinquishmentDetail_AccountNumber").val(response.AccountCode);
                    $("#RelinquishmentDetail_AccountName").val(response.AccountName);
                    $("#RelinquishmentDetail_AccountType").val(response.AccountType);
                    $("#RelinquishmentDetail_InvestmentService").val(response.InvestmentService);

                    $('#accountErrorAlert').hide();
                    $('#accountErrorAlert').hide();
                    $('#submitButton').removeAttr("disabled");
                    //GetInstrumentDetails();
                }

                ValidateTextField('RelinquishmentDetail_AccountName');
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
}

function GetExistingRelinquishmentForAccount() {

    var accountNumber = $('#RelinquishmentDetail_AccountNumber').val();
    var instrumentCode = $('#RelinquishmentDetail_InstrumentCode').val();
    var exchange = $('#RelinquishmentDetail_Exchange').val();
    
    if (accountNumber.length > 0 && instrumentCode.length > 0 && exchange.length > 0) {

        let result = true; 

        $.ajax({
            beforeSend: function () {
                $("#loader").show();
            },
            complete: function () {
                $("#loader").hide();
            },
            type: "GET",
            async: false,
            cache: false, 
            url: "../api/relinquishment/GetExistingRelinquishmentForAccount",
            data: { "accountNumber": accountNumber, "instrumentCode": instrumentCode, "exchange": exchange  },
            //contentType: "application/json",
            dataType: 'json',
            success: function (response) {

                if (response === null) {
                    alert('No data found');
                    resule =  true;
                }
                else if (response.status === 'Success' && response.alreadyRelinquished === true) {

                    alert(response.message);
                    result =  true;
                }
                else {
                    result = false; 
                }

                ValidateTextField('RelinquishmentDetail_AccountName');
            },
            failure: function (response) {
                alert(response);
            }
        });

        return result; 
    }
}

function ValidateAccountInformationForRelinquishment() {

    if (ValidateTextField('RelinquishmentDetail_AccountNumber') && ValidateTextField('RelinquishmentDetail_AccountName')) {
        return true;
    } else {
        return false;
    }
}

function ValidateAccountInstrumentInfo_Any() {

    if (ValidateTextField('RelinquishmentDetail_InstrumentCode') &&
        ValidateTextField('RelinquishmentDetail_Exchange') &&
        ValidateTextField('RelinquishmentDetail_DateIn') &&        
        ValidateTextField('RelinquishmentDetail_Volume') &&
        ValidateTextField('RelinquishmentDetail_InstrumentName') &&
        ValidateTextField('RelinquishmentDetail_AdminNotes') 
        
    ) {
        return true;
    } else {
        return false;
    }
}

function ValidateAccountInstrumentInfo() {
        var isValid = false;
        var a = ValidateTextField('RelinquishmentDetail_InstrumentCode');
        var b = ValidateTextField('RelinquishmentDetail_Exchange');
        var c = ValidateTextField('RelinquishmentDetail_DateIn');
        var d = ValidateTextField('RelinquishmentDetail_Volume');
        var e = ValidateTextField('RelinquishmentDetail_InstrumentName');
        //var f =ValidateTextField('RelinquishmentDetail_AdminNotes')
 
    if (a && b && c && d && e  ) {
        isValid = true;
    }

    return isValid;
}

function ValidateAllRelinquishmentData() {
    var isValid = false;

    var a = ValidateTextField('RelinquishmentDetail_AccountNumber');
    var b = ValidateTextField('RelinquishmentDetail_AccountType');
    var c = ValidateTextField('RelinquishmentDetail_AccountName');   
    var isRecordavailable = false; 

    var recordCount = $("#tblInstrument TBODY TR").length; 

    if (recordCount > 0) {
        isRecordavailable = true;
    }
    else {
        alert("Please make sure you have selected account along with instrument!");
        
    }


    if (a && b && c && isRecordavailable) {
        isValid = true;
    }

    return isValid;
}

function AddNewRow() {

    if (ValidateAccountInstrumentInfo()) {

        //Check if the instrument is already 
        var alreadyAdded = false; 
        $("#tblInstrument TBODY TR").each(function () {
            var row = $(this);                        
            var instrumentCode = row.find("TD").eq(0).html();            
            var instrumentExchange = row.find("TD").eq(2).html();
            var selectedInstrumentCode = $("#RelinquishmentDetail_InstrumentCode").val(); 
            var selectedExchange = $("#RelinquishmentDetail_Exchange").val();
            if (instrumentCode === selectedInstrumentCode && instrumentExchange === selectedExchange) {
                alert("Selected instrument has already been added!");
                return alreadyAdded = true; 
            }            
        });  

        if (alreadyAdded) {
            return false; 
        }
        
       

        if (GetExistingRelinquishmentForAccount()) {
            return false;
        }
        //Reference the Name and Country TextBoxes.
        var txtInstrumentCode = $("#RelinquishmentDetail_InstrumentCode");
        var txtInstrumentName = $("#RelinquishmentDetail_InstrumentName");
        var txtExchange = $("#RelinquishmentDetail_Exchange");
        var txtVolume = $("#RelinquishmentDetail_Volume");
        var txtDateIn = $("#RelinquishmentDetail_DateIn");
        var txtNotes = $("#RelinquishmentDetail_AdminNotes");
        var txtCustodian = $("#RelinquishmentDetail_Custodian"); 
        

        //Get the reference of the Table's TBODY element.
        var tBody = $("#tblInstrument > TBODY")[0];

        //Add Row.
        var row = tBody.insertRow(-1);

        //Add Name cell.
        var cell = $(row.insertCell(-1));
        cell.html(txtInstrumentCode.val());

        //Add Country cell.
        cell = $(row.insertCell(-1));
        cell.html(txtInstrumentName.val());

      
        cell = $(row.insertCell(-1));
        cell.html(txtExchange.val());

       
        cell = $(row.insertCell(-1));
        cell.html(txtVolume.val());

        
        cell = $(row.insertCell(-1));
        cell.html(txtDateIn.val());

        
        cell = $(row.insertCell(-1));
        cell.html(txtNotes.val());

        cell = $(row.insertCell(-1));
        cell.html(txtCustodian.val());        

        //Add Button cell.
        cell = $(row.insertCell(-1));
        var btnRemove = $("<input />");
        btnRemove.attr("type", "button");
        btnRemove.addClass("btnRemove");
        btnRemove.attr("onclick", "Remove(this);");
        btnRemove.val("Remove");
        cell.append(btnRemove);

        //Clear the TextBoxes.
        txtInstrumentCode.val("");
        txtExchange.val("");
        txtVolume.val("");  
        txtInstrumentName.val("");
        txtNotes.val("");
        txtCustodian.val("");
    }
}

function Remove(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    var name = $("TD", row).eq(0).html();
    if (confirm("Do you want to delete: " + name)) {
        //Get the reference of the Table.
        var table = $("#tblInstrument")[0];

        //Delete the Table row using it's Index.
        table.deleteRow(row[0].rowIndex);
    }
}

function SaveAllRelinquishment() {

    var isValid = ValidateAllRelinquishmentData();
    if (!isValid) return false; 

    var txtAccountNumber = $("#RelinquishmentDetail_AccountNumber");
    var txtAccountType = $("#RelinquishmentDetail_AccountType");
    var txtAccountName = $("#RelinquishmentDetail_AccountName");
    var txtInvestmentService = $("#RelinquishmentDetail_InvestmentService");
    var txtUpdatedName = $("#RelinquishmentDetail_UpdatedName");

    //Loop through the Table rows and build a JSON array.
    var relinquishments = new Array();
    $("#tblInstrument TBODY TR").each(function () {
        var row = $(this);
        var instrument = {};
        instrument.AccountNumber = txtAccountNumber.val();
        instrument.AccountName = txtAccountName.val();
        instrument.AccountType = txtAccountType.val();
        instrument.InvestmentService = txtInvestmentService.val(); 
        instrument.RelinquishmentDate = new Date(); //row.find("TD").eq(4).html(); //this line is making trouble
        instrument.InstrumentCode = row.find("TD").eq(0).html();
        instrument.InstrumentName = row.find("TD").eq(1).html();
        instrument.Exchange = row.find("TD").eq(2).html();
        instrument.Volume = row.find("TD").eq(3).html();        
        instrument.AdminNotes = row.find("TD").eq(5).html();
        instrument.Custodian = row.find("TD").eq(6).html();
        instrument.CreatedOn = new Date();
        instrument.CreatedBy = txtUpdatedName.val();
        instrument.UpdatedOn = new Date();
        instrument.UpdatedBy = txtUpdatedName.val();
        relinquishments.push(instrument);
    });       
    
   
    $.ajax({
        beforeSend: function () {
            $("#loader").show();

        },
        complete: function () {
            $("#loader").hide();
        },
        type: "POST",        
        url: "../api/relinquishment/SaveAllRelinquishments",
        data: JSON.stringify(relinquishments),
        traditional: true, 
        contentType: "application/json",
        dataType: 'json',
        success: function (result) {         

            if (result.status === "Success") {
                iziToast.success({
                    title: 'Submitted Successfully!',
                    message: 'Client relinquishment(s) have been added to the relinquishment register!'
                });
                $('#certErrorAlert').hide();
                $("#hfEditPageUrl").val(result.editUrl)
                //$('form input').hide();
                $(":button").prop('disabled', true);
                $('#RelinquishmentDetail_AccountNumber').attr('readonly', true);
                $("#divInstrumentSection").hide(1000);

                //button divs
                $("#divSubmitButton").hide();
                $("#divAddNew").show(1000);              

               
            }
            else if (result.status === "CodeError") {
                iziToast.error({
                    title: 'An error occurred while submitting your Order!',
                    message: 'It has been sent to <a href="mailto: development@forsythbarr.co.nz">Development</a> for review. Please contact <a href="mailto: development@forsythbarr.co.nz">Development</a> should you wish to discuss.'
                });
            }
            else {
                $("#certErrorAlertText").html(result.message);
                $('#certErrorAlert').show();
            }
           
        },
        failure: function (response) {
            alert('Error');
        }
      
    });
}

//Relinquishment Edit page
function ValidateEditRelinquishmentData() {
    var isValid = false;

    var a = ValidateTextField('RelinquishmentDetail_AccountNumber');
    var b = ValidateTextField('RelinquishmentDetail_AccountType');
    var c = ValidateTextField('RelinquishmentDetail_AccountName');  
    var d = ValidateTextField('RelinquishmentDetail_InstrumentCode');
    var e = ValidateTextField('RelinquishmentDetail_InstrumentName');
    var f = ValidateTextField('RelinquishmentDetail_Exchange');
    var g = ValidateTextField('RelinquishmentDetail_Volume'); 
    var h = ValidateTextField('RelinquishmentDetail_DateIn');       

    if (a && b && c && e && f && g &&  h ) {
        isValid = true;
    }


    $("#hdnHandlerType").val("update");

    return isValid;
}

function UpdateRelinquishment() {

    var isValid = ValidateEditRelinquishmentData();
    if (!isValid) return false;

    var txtAccountNumber = $("#RelinquishmentDetail_AccountNumber");
    var txtAccountType = $("#RelinquishmentDetail_AccountType");
    var txtAccountName = $("#RelinquishmentDetail_AccountName");
    var txtUpdatedName = $("#RelinquishmentDetail_UpdatedName");    
    


    $.ajax({
        beforeSend: function () {
            $("#loader").show();

        },
        complete: function () {
            $("#loader").hide();
        },
        type: "POST",
        url: "/api/relinquishment/SaveAllRelinquishments",
        data: JSON.stringify(relinquishments),
        traditional: true,
        contentType: "application/json",
        dataType: 'json',
        success: function (result) {

            if (result.status === "Success") {
                iziToast.success({
                    title: 'Submitted Successfully!',
                    message: 'Client relinquishment(s) have been added to the relinquishment register!'
                });
                $('#certErrorAlert').hide();
                $("#hfEditPageUrl").val(result.editUrl)
                //$('form input').hide();
                $(":button").prop('disabled', true);
                $('#RelinquishmentDetail_AccountNumber').attr('readonly', true);
                $("#divInstrumentSection").hide(1000);


            }
            else if (result.status === "CodeError") {
                iziToast.error({
                    title: 'An error occurred while submitting your Order!',
                    message: 'It has been sent to <a href="mailto: development@forsythbarr.co.nz">Development</a> for review. Please contact <a href="mailto: development@forsythbarr.co.nz">Development</a> should you wish to discuss.'
                });
            }
            else {
                $("#certErrorAlertText").html(result.message);
                $('#certErrorAlert').show();
            }

        },
        failure: function (response) {
            alert('failed');
        }

    });
}

var onStartUpdateRelinquishment = function (result) {
    if (result.status === "Success") {
        iziToast.success({
            title: 'Submitted Successfully!',
            message: 'Client Relinquishment has been updated successfully!'
        });
    }
    else if (result.status === "CodeError") {
        iziToast.error({
            title: 'An error occurred while updating client relinquishment!',
            message: 'It has been sent to <a href="mailto: development@forsythbarr.co.nz">Development</a> for review. Please contact <a href="mailto: development@forsythbarr.co.nz">Development</a> should you wish to discuss.'
        });
    }
    else {
        $("#certErrorAlertText").html(result.message);
        $('#certErrorAlert').show();
    }
}

function DeleteRecord() {  
    $("#hdnHandlerType").val("delete");   
    var userResponse = confirm("Do you really want to delete this record?");  
    if (userResponse) {
        $("#deleteButton").prop('disabled', true);
        $("#submitAllButton").prop('disabled', true);
        return true; 
    }

    return false;
}



