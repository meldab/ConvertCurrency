using CurrencyConverter.Services.Models;
using System.Collections.Generic;

namespace CurrencyConverter.Models
{
    public class RateHistoryResult
    {
        public bool Success { get; set; }
        public List<RateHistory> Data { get; set; }
        public string ErrorMessage { get; set; }

    }
}