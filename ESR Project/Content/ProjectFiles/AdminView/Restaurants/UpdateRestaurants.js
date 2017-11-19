function RestaurantUpdate(id) {
    $("#TagTable").empty();
    var count1 = 0;

    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/tags/GetAllTags?id=" + count1 + "", //URI
        dataType: "json",
        success: function (data) {

            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {

                $("#TagTable").append(' <tr id="jawad' + mobj.Id + '"> ' +
                    '               <td><input type="checkbox" class="checkbox" name="checkboxforTag" value="' + mobj.Id + '"></td>' +
                    '               <td>' + mobj.Description + ' </td>' +

                    '</tr>');

            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });

    $("#DealTable").empty();
    var count3 = 0;

    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/deals/GetAllDeals?id=" + count3 + "", //URI
        dataType: "json",
        success: function (data) {

            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {

                $("#DealTable").append(' <tr id="jawad' + mobj.Id + '"> ' +
                    '               <td><input type="checkbox" class="checkbox" name="checkboxforDeal" value="' + mobj.Id + '"></td>' +
                    '               <td>' + mobj.Description + ' </td>' +

                    '</tr>');

            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });

    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/restaurants/GetAllRestaurantsById?id=" + id + "", //URI
        dataType: "json",
        success: function (data) {
            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {
                document.getElementById("RestaurantId").value = mobj.Id;
                document.getElementById("RestaurantName").value = mobj.Name;
                document.getElementById("RestaurantArea").value = mobj.Area;
                document.getElementById("RestaurantDiscount").value = mobj.Discount;
               
            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });

    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/restaurants/GetAllRestTagsById?id=" + id + "", //URI
        dataType: "json",
        success: function (data) {

            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {
               
                $(":checkbox[value=" + mobj.TagId + "]").prop("checked", "true");

            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });

    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/restaurants/GetAllRestDealsById?id=" + id + "", //URI
        dataType: "json",
        success: function (data) {

            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {

                $(":checkbox[value=" + mobj.DealId + "]").prop("checked", "true");

            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });


}

function UpdateRestaurantForm() {
    document.getElementById("BtnRestaurant").disabled = true;
    if ($("#RestaurantName").val() != null) {

        var Restaurant = {
            Id: $("#RestaurantId").val(),
            Name: $("#RestaurantName").val(), Discount: $("#RestaurantDiscount").val(),
            Area: $("#RestaurantArea").val()
        };
        var CarreerData = JSON.stringify(Restaurant);
        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "http://localhost:57732/api/restaurants/UpdateRestaurant",
            contentType: 'application/json; charset=utf-8',
            processData: false,
            dataType: "json",
            data: CarreerData,
            success: function (data, textStatus, xhr) {
                if (data == 0) {
                    document.getElementById("BtnRestaurant").disabled = false;
                    bootbox.alert('Try Again! Later...');
                   
                }
                else {
                    var pack = "";
                    $("input:checkbox[name=checkboxforTag]:checked").each(function () {

                        pack = data + "," + $(this).val();

                        $.ajax({
                            type: "POST",
                            url: "http://localhost:57732/api/restaurants/PostRestaurantTags?id=" + pack + "",
                            contentType: 'application/json; charset=utf-8',
                            processData: false,
                            dataType: "json",
                            data: CarreerData,
                            success: function (data, textStatus, xhr) {

                            },

                        });

                    });
                    var pack2 = "";
                    $("input:checkbox[name=checkboxforDeal]:checked").each(function () {

                        pack2 = data + "," + $(this).val();

                        $.ajax({
                            type: "POST",
                            url: "http://localhost:57732/api/restaurants/PostRestaurantDeals?id=" + pack2 + "",
                            contentType: 'application/json; charset=utf-8',
                            processData: false,
                            dataType: "json",
                            data: CarreerData,
                            success: function (data, textStatus, xhr) {

                            },

                        });

                    });
                    document.getElementById("BtnRestaurant").disabled = false;
                    bootbox.alert('Restaurant Successfully Added!');
                    $('#Restaurantform')[0].reset();
                    myFunctionSwapView('RestaurantDetails');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.getElementById("BtnRestaurant").disabled = false;
                bootbox.alert('Server is not responding! Try Again Later');
                $('#Restaurantform')[0].reset();
            }
        });
        return false;
    }
    else {
        bootbox.alert("Make Sure All Fields with * are filled!");
        document.getElementById("BtnRestaurant").disabled = false;
        return false;
    }
}