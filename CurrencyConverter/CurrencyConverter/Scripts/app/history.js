"use strict";
(function () {

    $("#historyCurrency").on("change", GetCurrencyRateHistory);

    $("#historyButton").on("click", GetCurrencyRateHistoryWithAmount);


    function GetCurrencyRateHistory() {
        var currency = $("#historyCurrency").val();
        $("#target").empty();
        $("#historyAmount").val(null);

        if (currency == "EUR")
            return;
  
        $.ajax({
            url: '/History/GetCurrencyRateHistroy?currency=' + currency,
            type: 'GET',
            contentType: "application/json",
            dataType: 'Json',
        }).done(function (result) {
            if (result.Success) {

                var $table = $('<table class="border" style="width:250px;"></table>');

                var $trHeader = $('<tr class="border"></tr>');
                var $tdRateHeader = $('<td class="border header">Rate</td>');
                var $tdDateHeader = $('<td class="border header">Date</td>');

                $trHeader.append($tdRateHeader);
                $trHeader.append($tdDateHeader);

                $table.append($trHeader);

                for (let i = 0; i < result.Data.length; i++) {
                    var $tr = $('<tr class="border"></tr>');
                    var $tdRate = $('<td class="border"></td>');
                    var $tdDate = $('<td class="border"></td>');
                    $tdRate.append(result.Data[i].Rate);
                    $tdDate.append(result.Data[i].Date);

                    $tr.append($tdRate);
                    $tr.append($tdDate);

                    $table.append($tr);
                }

                $("#target").empty().append($table);
            }
            else {
                $("#currency_error").text(error.responseText);
            }
        }).fail(function (error) {
            alert(error);
        });

        return false;
    }

    function GetCurrencyRateHistoryWithAmount() {
        var currency = $("#historyCurrency").val();
        $("#target").empty();
        if (currency == "EUR")
            return;

        var data = JSON.stringify({
            'currency': currency,
            'amount': $("#historyAmount").val()
        });

        $.ajax({
            url: '/History/GetCurrencyRateHistroyWithAmount/',
            type: 'POST',
            data: data,
            contentType: "application/json",
            dataType: 'Json',
        }).done(function (result) {
            if (result.Success) {

                var $table = $('<table class="border" style="width:250px;"></table>');

                var $trHeader = $('<tr class="border"></tr>');
                var $tdRateHeader = $('<td class="border header">Rate</td>');
                var $tdDateHeader = $('<td class="border header">Date</td>');
                var $tdAmountHeader = $('<td class="border header">Amount</td>');

                $trHeader.append($tdRateHeader);
                $trHeader.append($tdDateHeader);
                $trHeader.append($tdAmountHeader);

                $table.append($trHeader);

                for (let i = 0; i < result.Data.length; i++) {
                    var $tr = $('<tr class="border"></tr>');
                    var $tdRate = $('<td class="border"></td>');
                    var $tdDate = $('<td class="border"></td>');
                    var $tdAmount = $('<td class="border"></td>');

                    $tdRate.append(result.Data[i].Rate);
                    $tdDate.append(result.Data[i].Date);
                    $tdAmount.append(result.Data[i].Amount);

                    $tr.append($tdRate);
                    $tr.append($tdDate);
                    $tr.append($tdAmount);

                    $table.append($tr);
                }

                $("#target").empty().append($table);
            }
            else {
                $("#currency_error").text(error.responseText);
            }
        }).fail(function (error) {
            alert(error);
        });

        return false;
    }


})();
