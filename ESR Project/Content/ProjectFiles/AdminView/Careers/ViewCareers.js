﻿function CareerDetailsOpenFunction(id) {
    $("#DesiMasalaDiv1").hide();
    $("#DesiMasalaDiv2").hide();
    var funcName = "CareerUpdate";
    var count = 0;
    $("#CareerDetailsDataBody").empty();
    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/careers/GetAllCareers?id="+ id +"", //URI
        dataType: "json",
        success: function (data) {
            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {
                $("#CareerDetailsDataBody").append('<tr id="Row' + mobj.Id + '"> ' +
                    '               <td><input type="checkbox" class="checkbox" name="checkboxDelete" value="' + mobj.Id + '"></td>' +
                    '               <td>' + mobj.PositionTitle + ' </td>' +
                    '               <td>' + mobj.Responsibilities + ' </td>' +
                    '               <td>' + mobj.Requirments + ' </td>' +
                    '               <td><button type="button" class="btn btn-default btn-sm" onclick="myFunctionSwapView2(' + mobj.Id + ',\'' + funcName + '\');">Edit</button> </td> ' +
                    '</tr>');

                
                    if (mobj.count == 1 || mobj.count == 0)
                        document.getElementById('NumberOfCareers').innerHTML = mobj.Count + "  Career";
                    else
                        document.getElementById('NumberOfCareers').innerHTML = mobj.Count + "  Careers";
                    count++;
                
            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });
};

function CareerCheakingCheckboxes1() {
    var select_all = document.getElementById("select_all1"); //select all checkbox
    var checkboxes = document.getElementsByClassName("checkbox"); //checkbox items

    //select all checkboxes
    select_all.addEventListener("change", function (e) {
        for (i = 0; i < checkboxes.length; i++) {
            checkboxes[i].checked = select_all.checked;
        }
    });


    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].addEventListener('change', function (e) { //".checkbox" change
            //uncheck "select all", if one of the listed checkbox item is unchecked
            if (this.checked == false) {
                select_all.checked = false;
            }
            //check "select all" if all checkbox items are checked
            if (document.querySelectorAll('.checkbox:checked').length == checkboxes.length) {
                select_all.checked = true;
            }
        });
    }
    return false;

}
function CareerCheakingCheckboxes2() {
    var select_all = document.getElementById("select_all2"); //select all checkbox
    var checkboxes = document.getElementsByClassName("checkbox"); //checkbox items

    //select all checkboxes
    select_all.addEventListener("change", function (e) {
        for (i = 0; i < checkboxes.length; i++) {
            checkboxes[i].checked = select_all.checked;
        }
    });


    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].addEventListener('change', function (e) { //".checkbox" change
            //uncheck "select all", if one of the listed checkbox item is unchecked
            if (this.checked == false) {
                select_all.checked = false;
            }
            //check "select all" if all checkbox items are checked
            if (document.querySelectorAll('.checkbox:checked').length == checkboxes.length) {
                select_all.checked = true;
            }
        });
    }
    return false;
}
function CareerDeleteFunction() { 
    var id = 0;
    $("input:checkbox[name=checkboxDelete]:checked").each(function () {
        id++;
    });
    if (id == 0)
        bootbox.alert("To Delete Select the desired record!")
    else {
        bootbox.confirm({
            title: "Delete Confirmation",
            message: "It Will be Permanently Deleted",
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm'
                }
            },
            callback: function (result) {
                if (result) {
                    $("input:checkbox[name=checkboxDelete]:checked").each(function () {
                        var rowId = $(this).val();
                        $.ajax({
                            type: 'DELETE',
                            url: "http://localhost:57732/api/careers/DeleteCareer?id=" + $(this).val() + "", //URI
                            dataType: "json",
                            contentType: "application/json",
                            success: function (data, textStatus, xhr) {
                                var row = document.getElementById("Row" + rowId);
                                row.parentNode.removeChild(row);
                                $("#DesiMasalaDiv1").hide();
                                $("#DesiMasalaDiv2").hide();
                                var count = parseInt(document.getElementById('NumberOfCareers').innerHTML);
                                if(count == 2)
                                    document.getElementById('NumberOfCareers').innerHTML = count - 1 + "  Career";
                                else
                                    document.getElementById('NumberOfCareers').innerHTML = count - 1 + "  Careers";

                            },
                            error: function (xhr) {
                                bootbox.alert('Error in Operation Try Again Later');
                            }

                        });
                    });
                    bootbox.alert('Records Successfully Deleted :)');
                }
            }
        });
    }
}

function GetSingleCareerByName()
{
    document.getElementById('NumberOfCareers').innerHTML = 0 + "  Career";
    $("#DesiMasalaDiv1").hide();
    $("#DesiMasalaDiv2").hide();
    var funcName = "CareerUpdate";
    var count = 0;
    $("#CareerDetailsDataBody").empty();
    $.ajax({
        type: "GET",
        url: "http://localhost:57732/api/careers/GetAllCareersByName?id=" + document.getElementById("CareerSearch").value + "", //URI
        dataType: "json",
        success: function (data) {
            debugger;
            var datavalue = data;
            var myJsonObject = datavalue;
            contentType: "application/json";
            $.each(myJsonObject, function (i, mobj) {
                $("#CareerDetailsDataBody").append('<tr id="Row' + mobj.Id + '"> ' +
                    '               <td><input type="checkbox" class="checkbox" name="checkboxDelete" value="' + mobj.Id + '"></td>' +
                    '               <td>' + mobj.PositionTitle + ' </td>' +
                    '               <td>' + mobj.Responsibilities + ' </td>' +
                    '               <td>' + mobj.Requirments + ' </td>' +
                    '               <td><button type="button" class="btn btn-default btn-sm" onclick="myFunctionSwapView2(' + mobj.Id + ',\'' + funcName + '\');">Edit</button> </td> ' +
                    '</tr>');

                if (count == 0) {
                    $("#DesiMasalaDiv1").show();
                    $("#DesiMasalaDiv2").show();
                    document.getElementById("DesiMasalaCareer1").innerHTML = mobj.NumberOfShowing;
                    document.getElementById("DesiMasalaCareer2").innerHTML = mobj.NumberOfShowing;
                    document.getElementById("PrevButtonOfCareer1").value = mobj.Prev;
                    document.getElementById("PrevButtonOfCareer2").value = mobj.Prev;
                    document.getElementById("NextButtonOfCareer1").value = mobj.Next;
                    document.getElementById("NextButtonOfCareer2").value = mobj.Next;
                    if (mobj.count == 1 || mobj.count == 0)
                        document.getElementById('NumberOfCareers').innerHTML = mobj.Count + "  Career";
                    else
                        document.getElementById('NumberOfCareers').innerHTML = mobj.Count + "  Careers";
                    count++;
                }
            });

        },
        error: function (xhr) {
            bootbox.alert('Internel Error...');
        }
    });
    document.getElementById("CareerSearch").value = "";
    return false;
}