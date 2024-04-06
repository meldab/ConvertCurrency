using CurrencyConverter.Models;
using CurrencyConverter.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter.Services.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetCurrencyList();

        Task<decimal> GetLatestCurrencyRate(string currency);

        Task<List<RateHistory>> GetCurrencyRateHistory(string currency);

        Task<List<AmountHistory>> GetCurrencyRateHistoryWithAmount(RateHistoryWithAmount model);
    }
}
