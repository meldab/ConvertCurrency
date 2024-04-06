using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Models;
using CurrencyConverter.Services.Interfaces;
using CurrencyConverter.Services.Models;
using CurrencyConverter.Services.Utils;

namespace CurrencyConverter.Services.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        public async Task<List<Currency>> GetCurrencyList()
        {
            var result = await GetCurrencyListFromXml();

            var currencyList = CurrencyList(result);

            return currencyList;
        }

        public async Task<decimal> GetLatestCurrencyRate(string currency)
        {
            var currencyList = await GetCurrencyList();
            var rate = currencyList.Where(x => x.Name == currency).Select(x => x.Rate).FirstOrDefault();

            return rate;
        }

        public async Task<List<RateHistory>> GetCurrencyRateHistory(string currency)
        {
            var result = await GetCurrencyListFromXml();

            var historyList = new List<RateHistory>();
           
            foreach (var item in result.CubeRootEl)
            {
                var history = new RateHistory();
                history.Rate = item.CubeItems.Where(x => x.Currency == currency).Select(x => x.Rate).FirstOrDefault().ToString("0.00");
                history.Date = Convert.ToDateTime(item.Time).ToShortDateString();

                historyList.Add(history);
            }

            return historyList;
        }

        public async Task<List<AmountHistory>> GetCurrencyRateHistoryWithAmount(RateHistoryWithAmount model)
        {
            var result = await GetCurrencyListFromXml();

            var historyList = new List<AmountHistory>();

            foreach (var item in result.CubeRootEl)
            {
                var history = new AmountHistory();
                history.Rate = item.CubeItems.Where(x => x.Currency == model.Currency).Select(x => x.Rate).FirstOrDefault().ToString("0.00");
                history.Date = Convert.ToDateTime(item.Time).ToShortDateString();
                history.Amount = Convert.ToDecimal(history.Rate) * model.Amount;

                historyList.Add(history);
            }

            return historyList;
        }


        #region private methods

        private async Task<EcbEnvelope> GetCurrencyListFromXml()
        {
            var response = await XmlReader.ReadCurrencyListFromXml();

            var result = XmlReader.DeserializedXml(response).Result;

            return result;
        }

        private List<Currency> CurrencyList(EcbEnvelope ecbEnvelope)
        {
            var cubeItems = ecbEnvelope.CubeRootEl.OrderByDescending(x => x.Time).FirstOrDefault().CubeItems;

            var currencyList = new List<Currency>();
            currencyList.Add(new Currency { Name = "EUR", Rate = 1 });

            foreach (var item in cubeItems)
            {
                var currency = new Currency { Name = item.Currency, Rate = item.Rate };
                currencyList.Add(currency);
            }

            return currencyList;
        }

        #endregion
    }
}
