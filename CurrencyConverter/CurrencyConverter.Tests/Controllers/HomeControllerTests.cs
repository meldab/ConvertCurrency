using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using CurrencyConverter.Services.Interfaces;
using CurrencyConverter.Controllers;
using CurrencyConverter.Services.Models;
using System.Web.Mvc;
using CurrencyConverter.Models;
using System;

namespace CurrencyConverter.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<ICurrencyRepository> _mockCurrencyRepository;
        private HomeController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockCurrencyRepository = new Mock<ICurrencyRepository>();
            _controller = new HomeController(_mockCurrencyRepository.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsViewWithCurrencyListInViewBag()
        {
            // Arrange
            var fakeCurrencyList = new List<Currency>() {
                new Currency {
                    Name = "USD",
                    Rate = 1 },
                new Currency
                {
                    Name = "GBP",
                    Rate = 2
                }
            };

            _mockCurrencyRepository.Setup(r => r.GetCurrencyList()).ReturnsAsync(fakeCurrencyList);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewBag.CurrencyList);
            CollectionAssert.AreEqual(fakeCurrencyList, (List<Currency>)result.ViewBag.CurrencyList);
        }

        [TestMethod]
        public async Task ExchangeCurrency_ValidExchange_ReturnsJsonResult()
        {
            // Arrange
            var exchange = new Exchange
            {
                Amount = 100,
                CurrencyFrom = "USD",
                CurrencyTo = "EUR"
            };

            _mockCurrencyRepository.Setup(r => r.GetLatestCurrencyRate("USD")).ReturnsAsync(1);
            _mockCurrencyRepository.Setup(r => r.GetLatestCurrencyRate("EUR")).ReturnsAsync(2);

            // Act
            var result = await _controller.ExchangeCurrency(exchange) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var exchangeResult = result.Data as ExchangeResult;
            Assert.IsNotNull(exchangeResult);
            Assert.AreEqual("200.00", exchangeResult.CurrencyResult);
        }

        [TestMethod]
        public async Task ExchangeCurrency_ExceptionThrown_ReturnsJsonResultWithError()
        {
            // Arrange
            var exchange = new Exchange
            {
                Amount = 100,
                CurrencyFrom = "USD",
                CurrencyTo = "EUR"
            };

            _mockCurrencyRepository.Setup(r => r.GetLatestCurrencyRate("USD")).ThrowsAsync(new Exception("Mock exception"));

            // Act
            var result = await _controller.ExchangeCurrency(exchange) as JsonResult;

            Assert.IsNotNull(result);
            var exchangeResult = result.Data as ExchangeResult;
            Assert.IsNotNull(exchangeResult);
            Assert.IsNotNull(exchangeResult.ErrorMessage);
            Assert.AreEqual("Mock exception", exchangeResult.ErrorMessage);
        }
    }
}