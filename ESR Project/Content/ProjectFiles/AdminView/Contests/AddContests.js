﻿function ValidateContestForm() {
    document.getElementById("BtnContest").disabled = true;
    if ($("#ContestsName").val() != null) {

        var Contest = {
            Name: $("#ContestsName").val(), EntriesCount: $("#ContestsEntries").val(),
            WinnerName: $("#ContestsWinnerName").val(), Reward: $("#ContestsReward").val(), Date: $("#ContestsDate").val()
        };
        var ContestData = JSON.stringify(Contest);
        // Make Ajax request with the contentType = false, and procesDate = false
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "http://localhost:57732/api/contests/PostContest",
            contentType: 'application/json; charset=utf-8',
            processData: false,
            dataType: "json",
            data: ContestData,
            success: function (data, textStatus, xhr) {
                if (data == 0) {
                    document.getElementById("BtnContest").disabled = false;
                    bootbox.alert('Try Again! Later...');
                    $('#Contestform')[0].reset();
                }
                else {
                    document.getElementById("BtnContest").disabled = false;
                    bootbox.alert('Contest Successfully Added!');
                    $('#Contestform')[0].reset();
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.getElementById("BtnContest").disabled = false;
                bootbox.alert('Server is not responding! Try Again Later');
                $('#Contestform')[0].reset();
            }
        });
        return false;
    }
    else {
        bootbox.alert("Make Sure All Fields with * are filled!");
        document.getElementById("BtnContest").disabled = false;
        return false;
    }
}