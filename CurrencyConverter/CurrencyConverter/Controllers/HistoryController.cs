using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using CurrencyConverter.Services.Interfaces;
using CurrencyConverter.Models;

namespace CurrencyConverter.Controllers
{
    public class HistoryController : Controller
    {
        private ICurrencyRepository CurrencyRepository { get; set; }

        public HistoryController(ICurrencyRepository currencyRepository)
        {
            this.CurrencyRepository = currencyRepository;
        }

        [HttpGet]
        [OutputCache(Duration = 60, VaryByParam = "*", CacheProfile = "CacheMinute")]
        public async Task<ActionResult> GetCurrencyRateHistroy(string currency)
        {
            try
            {
                var result = await CurrencyRepository.GetCurrencyRateHistory(currency);

                return Json(new RateHistoryResult { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new RateHistoryResult { Success = false, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetCurrencyRateHistroyWithAmount(RateHistoryWithAmount model)
        {
            try
            {
                var result = await CurrencyRepository.GetCurrencyRateHistoryWithAmount(model);

                return Json(new AmountHistoryResult { Success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new AmountHistoryResult { Success = false, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}