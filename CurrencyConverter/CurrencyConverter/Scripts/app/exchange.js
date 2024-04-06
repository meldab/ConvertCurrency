"use strict";
(function () {

    $("#exchange").on("submit", CurrencyExchange);

    $("#currencyFrom").on("change", ChangeCurrency);
    $("#currencyTo").on("change", ChangeCurrency);

    function CurrencyExchange() {
        var data = JSON.stringify({
            'currencyFrom': $("#currencyFrom").val(),
            'currencyTo': $("#currencyTo").val(),
            'amount': $("#currencyAmount").val()
        });

        $.ajax({
            url: '/Home/ExchangeCurrency/',
            type: 'POST',
            data: data,
            contentType: "application/json",
            dataType: 'Json',
        }).done(function (result) {

            $("#currencyResult").text(result.CurrencyResult);

        }).fail(function (error) {
            alert(error.ErrorMessage);
        });

        return false;
    }

    function ChangeCurrency() {
        if ($("#currencyFrom").val() !== $("#currencyTo").val()) {

            $("#rateFrom").show();
            $("#rateTo").show();

            $("#rateFrom").text($("#currencyFrom").find(":selected").data("value").rate);
            $("#rateTo").text($("#currencyTo").find(":selected").data("value").rate);

            $("#currencyResult").text("");
        }
        else {
            $("#rateFrom").hide();
            $("#rateTo").hide();
        }
    }
})();
