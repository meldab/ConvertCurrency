using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyConverter.Models
{
    public class RateHistoryWithAmount
    {
        public string Currency { get; set; }

        public decimal Amount { get; set;  }
    }
}