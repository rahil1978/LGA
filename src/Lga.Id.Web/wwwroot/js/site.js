// Write your Javascript code.

$(document).ready(function () {
    //$.fn.dataTable.moment('DD/MM/YYYY');
    SetDefaults();  
    

    //$(function () {
    //    $(document).tooltip();
    //});
});

function SetDefaults() {

    //Certificate Section---------------------
    $("#CertificateDetails_InstrumentCode").keyup(function () {
        $('#CertificateDetails_InstrumentCode').val($('#CertificateDetails_InstrumentCode').val().toUpperCase());
        $('#CertificateDetails_Exchange').val("");
        $('#CertificateDetails_InstrumentName').val("");
    });    
       
    $("#CertificateDetails_InstrumentCode").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/api/instrument/searchinstrument',               
                //data: "{ 'searchInstrumentTerm': '" + request.searchInstrumentTerm + "'}",
                data: "searchInstrumentTerm=" + request.term,
                dataType: "json",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
					
					//alert(data.length );
					if (data.length > 0)
					{
						response($.map(data, function (item) {
							//return item.Code + "." + item.Exchange + " (" + item.Name + ")"; 

							return {
								label: item.Code + "." + item.Exchange + " (" + item.Name + ")",
								value: item.Code
							};
						}))
					}
                    else {
                        alert(request.term + " is not found!");
						$("#CertificateDetails_InstrumentCode").val("");
						$("#CertificateDetails_Exchange").val("");
						$("#CertificateDetails_InstrumentName").val("");

						response(''); 
						
					}
					
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        minLength: 1,
        select: function (e, i) {

            var val = i.item.label.replace(" ", "").replace(".", "?").replace("(", "?").replace(")", "?");

            //var instrumentCode = val.substring(0, val.indexOf("."));
            //var exchange = val.substr(val.indexOf(".") + 1, 3);
            //var instrumentName = val.substr(val.indexOf("(")+ 4, val.indexOf(")") + -1);

            var instrumentCode = val.split("?")[0];
            var exchange = val.split("?")[1];
            var instrumentName = val.split("?")[2];

            $("#CertificateDetails_InstrumentCode").val(instrumentCode);
            $("#CertificateDetails_Exchange").val(exchange);
            $("#CertificateDetails_InstrumentName").val(instrumentName);
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
			
        }
    });  
    
    $("#CertificateDetails_AccountNumber").focusout(function () {
        GetAccountDetails();
        ValidateAccountInformation();
    });
    
    $("#CertificateDetails_MaturityDate").datepicker({ dateFormat: 'dd-mm-yy' });

    $("#MaturityDate").datepicker({ dateFormat: 'dd-mm-yy' });
    
    SetTodayDate('CertificateDetails_DateIn');

    SetTodayDate('CertificateDetails_UpdatedDate');    

    
    $("#CertificateStatus").val("1");

    $('#divEdit').hide();
    $('#divReset').hide(); 
    $('#divCertHistory').hide();     

    $.fn.dataTable.moment('D/MM/YYYY h:mm:ss A');
    $('#tblSearchedCertificate').DataTable({
        pageLength: 100,
        columnDefs: [{
            target: 9,
            type: 'datetime-moment'

        }]
    });


    //Relinquishment Section----------------------
    SetTodayDate('CertificateDetails_UpdatedDate');
    
    $("#divAddNew").hide();

    $("#RelinquishmentDetail_AccountNumber").focusout(function () {
        GetAccountDetailForRelinquishment();
        //ValidateAccountInformationForRelinquishment();
    });

    $("#RelinquishmentDetail_InstrumentCode").keyup(function () {
        $('#RelinquishmentDetail_InstrumentCode').val($('#RelinquishmentDetail_InstrumentCode').val().toUpperCase());
    });

    $("#RelinquishmentDetail_InstrumentCode").autocomplete({
                

        source: function (request, response) {
            $.ajax({
                  url: '../api/instrument/searchinstrument',
                //data: "{ 'searchInstrumentTerm': '" + request.searchInstrumentTerm + "'}",
                //data: "searchInstrumentTerm=" + request.term,
                data: {
                        searchInstrumentTerm: request.term,
                    includeInactiveInstruments: $("#IsInactiveInstrumentChecked")[0].checked
                        },
                dataType: "json",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //alert(data.length );
					if (data.length > 0)
					{
						response($.map(data, function (item) {
							//return item.Code + "." + item.Exchange + " (" + item.Name + ")"; 

							return {
								label: item.Code + "." + item.Exchange + " (" + item.Name + ")",
                                value: item.Code,
                                exchange: item.Exchange,
                                intrumentName: item.Name,
                                custodian: item.Custodian
							};
						}))
					}
					else{
						$("#RelinquishmentDetail_InstrumentCode").val('');
						$("#RelinquishmentDetail_Exchange").val('');
                        $("#RelinquishmentDetail_InstrumentName").val('');
                        $("#RelinquishmentDetail_Custodian").val('');
                        
						response(''); 
						
					}
                },
                search: function () { $(this).addClass('working'); },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        minLength: 1,
        select: function (e, i) {

            var val = i.item.label.replace(" ", "").replace(".", "?").replace("(", "?").replace(")", "?");
            var custodian = i.item.custodian;
           

            var instrumentCode = val.split("?")[0];
            var exchange = val.split("?")[1];
            var instrumentName = val.split("?")[2];

            $("#RelinquishmentDetail_InstrumentCode").val(instrumentCode);
            $("#RelinquishmentDetail_Exchange").val(exchange);
            $("#RelinquishmentDetail_InstrumentName").val(instrumentName);
            $("#RelinquishmentDetail_Custodian").val(custodian);
          
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });   



    
    //$.fn.dataTable.moment('DD/MM/YYYY');
    $('#tblRelinquishment').DataTable({
        pageLength: 100,
        columnDefs: [{
            target: 6,
            type: 'datetime-moment'
            
        }]
     
    });
    
    //Registry-Holding Section---------------------

    var pageMode = $('#hfSavingHolding').val();
    if (pageMode === 'True') {
        $('#divCertificate').show();
        $('#divCsn').hide();
    }

    else if (pageMode === 'False') {
        $('#divCsn').show();
        $('#divCertificate').hide();
    }  

    $('#rbCertificate').on('change', function (e) {
        if (e.target.value === "true") {           
            $('#divCertificate').show(); 
            $('#divCsn').hide();  
            $('#hfSavingHolding').val("true");
          
        } 
    });

    $('#rbCSN').on('change', function (e) {
        if (e.target.value === "false") {
            $('#divCsn').show();
            $('#divCertificate').hide(); 
            $('#hfSavingHolding').val("false");

            
            //$('#divCertificate').removeClass(".visible");
            //$('#divCertificate').addClass(".invisible");             
            //$('#divCsn').removeClass(".invisible");
            //$('#divCsn').addClass(".visible");
        }
    });

    //RegistryHoldingDetails_InstrumentCode
    $("#RegistryHoldingDetails_InstrumentCode").autocomplete({
        source: function (request, response) {
            $.ajax({
                  url: '../api/instrument/searchinstrument',
                //data: "{ 'searchInstrumentTerm': '" + request.searchInstrumentTerm + "'}",
                data: "searchInstrumentTerm=" + request.term,
                dataType: "json",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.length > 0)
					{
						response($.map(data, function (item) {
							return {
								label: item.Code + "." + item.Exchange + " (" + item.Name + ")",
								value: item.Code
							};
						}))
					}
					else{
						$("#RegistryHoldingDetails_InstrumentCode").val('');
						$("#RegistryHoldingDetails_Exchange").val('');
						$("#RegistryHoldingDetails_InstrumentName").val('');
						response(''); 
						
					}
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        minLength: 1,
        select: function (e, i) {

            var val = i.item.label.replace(" ", "").replace(".", "?").replace("(", "?").replace(")", "?");

            //var instrumentCode = val.substring(0, val.indexOf("."));
            //var exchange = val.substr(val.indexOf(".") + 1, 3);
            //var instrumentName = val.substr(val.indexOf("(")+ 4, val.indexOf(")") + -1);

            var instrumentCode = val.split("?")[0];
            var exchange = val.split("?")[1];
            var instrumentName = val.split("?")[2];

            $("#RegistryHoldingDetails_InstrumentCode").val(instrumentCode);
            $("#RegistryHoldingDetails_Exchange").val(exchange);
            $("#RegistryHoldingDetails_InstrumentName").val(instrumentName);
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    }); 

    $("#RegistryHoldingDetails_CSN").change(function () {
        GetAccountTypeForSelectedCSN();
        
    });
         
    //Feater button on Grid
    feather.replace()

    $(".decimal-with-comma").inputmask({
        'alias': 'decimal',
         rightAlign: false,
        'groupSeparator': '.',
        'autoGroup': true
    });

    $('#tblRegistryHolding').DataTable({
        pageLength: 100,
        columnDefs: [{
            target: 6,
            type: 'datetime-moment'

        }]
    });
}

function ValidateDropdownList(fieldId) {
    var isValid = false;
    if ($('#' + fieldId).val().length > 0) {
        isValid = true;
        ValidField(fieldId);
    } else {
        HighlightInvalidField(fieldId);
    }

    
    if ($('#' + fieldId).children("option:selected").val() === "") {
        isValid = false;
        HighlightInvalidField(fieldId);
    }
    else {
        isValid = true;
        ValidField(fieldId);
    }
    return isValid;
}

function ValidateTextField(fieldId) {
    var isValid = false;
    if ($('#' + fieldId).val().length > 0) {
        isValid = true;
        ValidField(fieldId);
    } else {
        HighlightInvalidField(fieldId);
    }

    return isValid;
}

function ValidField(fieldId) {
    $('#' + fieldId).removeClass("error-alert");
}

function HighlightInvalidField(fieldId) {
    $('#' + fieldId).addClass("error-alert");
}

function SetReadonlyField(fieldId) {
    $('#' + fieldId).attr('readonly', true);
    $('#' + fieldId).addClass('input-disabled');
    //$('#' + fieldId).css('cursor', 'not-allowed');
    $('#' + fieldId).on("focus", function () {
        //$('#' + fieldId).blur();
    });
}

function RemoveReadonlyField(fieldId) {
    $('#' + fieldId).attr('readonly', false);
    $('#' + fieldId).removeClass('input-disabled');
    ////$('#' + fieldId).css('cursor', 'not-allowed');
    $('#' + fieldId).on("focus", function () {
        //$('#' + fieldId).off("blur");
    });
}

function SetTodayDate(fieldId) {
    var today = new Date();
    var tomorrow = new Date(new Date().getTime() + 24 * 60 * 60 * 1000);
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    var tomday = tomorrow.getDate();
    var tommonth = tomorrow.getMonth() + 1;
    var tomyear = tomorrow.getFullYear();
    if (dd < 10) { dd = '0' + dd } if (mm < 10) { mm = '0' + mm } today = dd + '/' + mm + '/' + yyyy;
    if (tomday < 10) { tomday = '0' + tomday } if (tommonth < 10) { tommonth = '0' + tommonth } tomorrow = tommonth + '/' + tomday + '/' + tomyear;    
    $('#' + fieldId).attr('value', today);
   
}

function ValidateSelectCombo(fieldId) {

    var divName = 'div' + fieldId;
   // if ($("select[name='" + fieldId + "']")[0].selectedIndex === 0)
    
    if ($("#" + fieldId)[0].selectedIndex === 0)
    {
        isValid = false;
        HighlightInvalidField(divName);
    } else {
        isValid = true;
        ValidField(divName);
    }

    return isValid;

}

var onBegin = function () {
    $("#loader").show();
    $(".messageBox").html("");
};

var onComplete = function () {
    $("#loader").hide();
    $('#divSumbit').hide();
    $('#divEdit').show();
    $('#divReset').show();

    
}

var onFailed = function () {
    $('#certErrorAlert').html(
        "An error occurred while processing your order. Please try again.");    
};

function RedirectToEdit() {     
    var url = $("#hfEditPageUrl").val();
    window.location = url;
}

function GetTodayDate() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;
    document.write(today);
}



