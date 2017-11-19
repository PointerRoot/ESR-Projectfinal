function validate(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9]/;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

function PakageUpdate(id) {

    $("#ServiceTable").empty();
    var count = 0;
    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/services/GetAllServices?id=" + count + "", //URI
        dataType: "json",
        success: function (data) {

            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {

                $("#ServiceTable").append(' <tr id="jawad' + mobj.Id + '"> ' +
                    '               <td><input type="checkbox" class="checkbox" name="checkboxforService" value="' + mobj.Id + '"></td>' +
                    '               <td>' + mobj.Name + ' </td>' +

                    '</tr>');

            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });

    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/pakages/GetAllPakagesById?id=" + id + "", //URI
        dataType: "json",
        success: function (data) {
            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {
                document.getElementById("PakageId").value = mobj.Id;
                document.getElementById("Name").value = mobj.Name;
                document.getElementById("Type").value = mobj.PakageType;
               // document.getElementById("Services").value = mobj.ServicesIncluded;
                document.getElementById("Price").value = mobj.CostPerMonth;
            });

        },
        error: function (xhr) {
            alert('Internel Error...');
        }
    });

    
    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/pakages/GetAllPakageServicesById?id=" + id + "", //URI
        dataType: "json",
        success: function (data) {

            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {

                $(":checkbox[value=" + mobj.ServiceId + "]").prop("checked", "true");

            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });
}

function ValidatePakageForm() {

    document.getElementById("BtnPakage").disabled = true;
    if ($("#Name").val() != null) {
        var Pakage = {
            Id: $("#PakageId").val(),
            Name: $("#Name").val(),
            PakageType: $("#Type").val(), CostPerMonth: $("#Price").val(),
            ServicesIncluded: "Yes"

        };
        var PakageData = JSON.stringify(Pakage);
        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "http://localhost:57732/api/pakages/UpdatePakage",
            contentType: 'application/json; charset=utf-8',
            processData: false,
            dataType: "json",
            data: PakageData,
            success: function (data, textStatus, xhr) {
                if (data == 0) {
                    document.getElementById("BtnPakage").disabled = false;
                    bootbox.alert('Try Again! Later...');
                    
                }
                else {
                    var pack = "";
                    $("input:checkbox[name=checkboxforService]:checked").each(function () {

                        pack = data + "," + $(this).val();
                        $.ajax({
                            type: "POST",
                            url: "http://localhost:57732/api/pakages/PostPakageServices?id=" + pack + "",
                            contentType: 'application/json; charset=utf-8',
                            processData: false,
                            dataType: "json",
                            data: PakageData,
                            success: function (data, textStatus, xhr) {

                            },

                        });

                    });
                    document.getElementById("BtnPakage").disabled = false;
                    bootbox.alert('Pakage Successfully Added!');
                    //var count = parseInt(document.getElementById("PakagesCounth3Div").innerHTML);
                    //document.getElementById("PakagesCounth3Div").innerHTML = count + 1;
                    $('#Pakageform')[0].reset();
                    myFunctionSwapView('PakageDetails');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.getElementById("BtnPakage").disabled = false;
                bootbox.alert('Server is not responding! Try Again Later');
                $('#Pakageform')[0].reset();
            }
        });
        return false;
    }
    else {
        bootbox.alert("Make Sure All Fields with * are filled!");
        document.getElementById("BtnPakage").disabled = false;
        return false;
    }
}