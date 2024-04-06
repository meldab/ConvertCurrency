using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using CurrencyConverter.Models;
using CurrencyConverter.Services.Interfaces;

namespace CurrencyConverter.Controllers
{
    public class HomeController : Controller
    {
        private ICurrencyRepository _currencyRepository { get; set; }

        public HomeController(ICurrencyRepository currencyRepository)
        {
           this._currencyRepository = currencyRepository;
        }

        [HttpGet]
        [OutputCache(Duration = 60, VaryByParam = "*", CacheProfile = "CacheMinute")]
        public async Task<ActionResult> Index()
        {
            ViewBag.CurrencyList =  await _currencyRepository.GetCurrencyList();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ExchangeCurrency(Exchange exchange)
        {
            try
            {
                var fromRate = await _currencyRepository.GetLatestCurrencyRate(exchange.CurrencyFrom);
                var toRate = await _currencyRepository.GetLatestCurrencyRate(exchange.CurrencyTo);

                var currencyResult = (exchange.Amount / fromRate * toRate).ToString("0.00");

                var exchangeResult = new ExchangeResult { CurrencyResult = currencyResult };

                return Json(exchangeResult);
            }
            catch (Exception ex)
            {
                return Json(new ExchangeResult { ErrorMessage = ex.Message });
            }
        }
    }
}