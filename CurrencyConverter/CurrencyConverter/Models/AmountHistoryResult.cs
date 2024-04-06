using CurrencyConverter.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyConverter.Models
{
    public class AmountHistoryResult
    {
        public bool Success { get; set; }
        public List<AmountHistory> Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}