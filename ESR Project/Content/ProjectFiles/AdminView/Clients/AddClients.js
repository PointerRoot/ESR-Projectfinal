function ValidateClientForm() {

    document.getElementById("BtnClient").disabled = true;
    if ($("#ClientName").val() != null) {
        var files = $("#ResImage").get(0).files;
        var dataValues = new FormData();
        dataValues.append("Image", files[0]);
        var Client = {
            Name: $("#ClientName").val(), Testimonial: $("#ClientTestimonial").val(),



        };
        var ClientData = JSON.stringify(Client);
        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "http://localhost:57732/api/clients/PostClient",
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
                    var dataValues = new FormData();
                    dataValues.append("Id", data);
                    var files = $("#ResImage").get(0).files;
                    dataValues.append("Image", files[0]);
                    alert(files[0]);
                    // Add the uploaded image content to the form data collection
                    if (files[0] != null) {
                        var ajaxRequest = $.ajax({
                            type: "POST",
                            url: "http://localhost:57732/api/clients/postImage",
                            contentType: false,
                            processData: false,
                            data: dataValues,
                            success: function (data, textStatus, xhr) {

                                

                            }
                        });
                    }

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