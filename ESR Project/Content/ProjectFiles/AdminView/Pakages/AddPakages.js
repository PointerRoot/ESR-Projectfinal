function ValidatePakageForm() {
  
    document.getElementById("BtnPakage").disabled = true;
    if ($("#PakageName").val() != null) {
           var Pakage = {
            Name :$("#PakageName").val(),
            PakageType: $("#PakageType").val(), CostPerMonth: $("#CostPerMonth").val(),
            ServicesIncluded: "Yes"
    
            };
            var PakageData = JSON.stringify(Pakage);
            // Make Ajax request with the contentType = false, and procesDate = false
            var ajaxRequest = $.ajax({
                type: "POST",
                url: "http://localhost:57732/api/pakages/PostPakage",
                contentType: 'application/json; charset=utf-8',
                processData: false,
                dataType: "json",
                data: PakageData,
                success: function (data, textStatus, xhr) {
                    if (data == 0) {
                        document.getElementById("BtnPakage").disabled = false;
                        bootbox.alert('Try Again! Later...');
                        $('#Pakageform')[0].reset();
                    }
                    else {
                        var pack="";
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
