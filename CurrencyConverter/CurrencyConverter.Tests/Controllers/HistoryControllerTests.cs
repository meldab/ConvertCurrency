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


namespace CurrencyConverter.Tests.Controllers
{
    [TestClass]
    public class HistoryControllerTests
    {
        private Mock<ICurrencyRepository> _mockCurrencyRepository;
        private HistoryController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockCurrencyRepository = new Mock<ICurrencyRepository>();
            _controller = new HistoryController(_mockCurrencyRepository.Object);
        }

        [TestMethod]
        public async Task GetCurrencyRateHistroy_ValidCurrency_ReturnsJsonResultWithData()
        {
            // Arrange
            string currency = "USD";
            var expectedData = new List<RateHistory> { new RateHistory { Date = DateTime.Now.ToString(), Rate = "1" } };

            _mockCurrencyRepository.Setup(r => r.GetCurrencyRateHistory(currency)).ReturnsAsync(expectedData);

            // Act
            var result = await _controller.GetCurrencyRateHistroy(currency) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var responseData = result.Data as RateHistoryResult;
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.Success);
            Assert.IsNotNull(responseData.Data);
            CollectionAssert.AreEqual(expectedData, responseData.Data);
        }

        [TestMethod]
        public async Task GetCurrencyRateHistroy_ExceptionThrown_ReturnsJsonResultWithError()
        {
            // Arrange
            string currency = "USD";
            string errorMessage = "Mock exception";

            _mockCurrencyRepository.Setup(r => r.GetCurrencyRateHistory(currency)).ThrowsAsync(new Exception(errorMessage));

            // Act
            var result = await _controller.GetCurrencyRateHistroy(currency) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var responseData = result.Data as RateHistoryResult;
            Assert.IsNotNull(responseData);
            Assert.IsFalse(responseData.Success);
            Assert.AreEqual(errorMessage, responseData.ErrorMessage);
            Assert.IsNull(responseData.Data);
        }

        [TestMethod]
        public async Task GetCurrencyRateHistroyWithAmount_ValidModel_ReturnsJsonResultWithData()
        {
            // Arrange
            var model = new RateHistoryWithAmount {  Amount = 10, Currency = "USD"};
            var expectedResult = new List<AmountHistory> { new AmountHistory { Rate = "2", Date = "2024/01/01", Amount = 20 } };

            _mockCurrencyRepository.Setup(repo => repo.GetCurrencyRateHistoryWithAmount(model)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetCurrencyRateHistroyWithAmount(model) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var responseData = result.Data as AmountHistoryResult;
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.Success);
            Assert.IsNotNull(responseData.Data);
            CollectionAssert.AreEqual(expectedResult, responseData.Data);
        }

        [TestMethod]
        public async Task GetCurrencyRateHistroyWithAmount_ExceptionThrown_ReturnsJsonResultWithError()
        {
            // Arrange
            var model = new RateHistoryWithAmount();
            var errorMessage = "An error occurred.";
            _mockCurrencyRepository.Setup(repo => repo.GetCurrencyRateHistoryWithAmount(model)).ThrowsAsync(new Exception(errorMessage));

            // Act
            var result = await _controller.GetCurrencyRateHistroyWithAmount(model) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var responseData = result.Data as AmountHistoryResult;
            Assert.IsNotNull(responseData);
            Assert.IsFalse(responseData.Success);
            Assert.AreEqual(errorMessage, responseData.ErrorMessage);
            Assert.IsNull(responseData.Data);
        }
    }
}
