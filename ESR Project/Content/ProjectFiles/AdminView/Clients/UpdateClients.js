function ClientUpdate(id) {

    $("#PakageTable").empty();
    var count2 = 0;

    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/pakages/GetAllPakages?id=" + count2 + "", //URI
        dataType: "json",
        success: function (data) {

            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {

                $("#PakageTable").append(' <tr id="jawad' + mobj.Id + '"> ' +
                    '               <td><input type="checkbox" class="checkbox" name="checkboxforPakage" value="' + mobj.Id + '"></td>' +
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
        url: "http://localhost:57732/api/clients/GetAllClientsById?id=" + id + "", //URI
        dataType: "json",
        success: function (data) {
            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {
                document.getElementById("ClientId").value = mobj.Id;
                document.getElementById("ClientName").value = mobj.Name;
                document.getElementById("ClientTestimonial").value = mobj.Testimonial;

            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });

    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/clients/GetAllClientsPakageById?id=" + id + "", //URI
        dataType: "json",
        success: function (data) {
            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {
             
                $(":checkbox[value=" + mobj.PakageId + "]").prop("checked", "true");

            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });
}
function UpdateClientForm() {
    document.getElementById("BtnClient").disabled = true;
    if ($("#ClientName").val() != null) {

        var Client = {
            Id: $("#ClientId").val(),
            Name: $("#ClientName").val(), Testimonial: $("#ClientTestimonial").val(),

        };
        var ClientData = JSON.stringify(Client);
        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "http://localhost:57732/api/clients/UpdateClient",
            contentType: 'application/json; charset=utf-8',
            processData: false,
            dataType: "json",
            data: ClientData,
            success: function (data, textStatus, xhr) {
                if (data == 0) {
                    document.getElementById("BtnClient").disabled = false;
                    bootbox.alert('Try Again! Later...');
                    $('#Clientform')[0].reset();
                }
                else {

                    var pack = "";
                    $("input:checkbox[name=checkboxforPakage]:checked").each(function () {

                        pack = data + "," + $(this).val();
                        $.ajax({
                            type: "POST",
                            url: "http://localhost:57732/api/clients/PostClientPakage?id=" + pack + "",
                            contentType: 'application/json; charset=utf-8',
                            processData: false,
                            dataType: "json",
                            data: ClientData,
                            success: function (data, textStatus, xhr) {

                            },

                        });

                    });
                    document.getElementById("BtnClient").disabled = false;
                    bootbox.alert('Client Successfully Added!');
                    var count = parseInt(document.getElementById("ClientsCounth3Div").innerHTML);
                    document.getElementById("ClientsCounth3Div").innerHTML = count + 1;
                    $('#Clientform')[0].reset();
                    myFunctionSwapView('ClientDetails');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.getElementById("BtnClient").disabled = false;
                bootbox.alert('Server is not responding! Try Again Later');
                $('#Clientform')[0].reset();
            }
        });
        return false;
    }
    else {
        bootbox.alert("Make Sure All Fields with * are filled!");
        document.getElementById("BtnClient").disabled = false;
        return false;
    }
}