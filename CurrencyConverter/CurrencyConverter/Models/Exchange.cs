using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyConverter.Models
{
    public class Exchange
    {
        public string CurrencyFrom { get; set; }

        public string CurrencyTo { get; set; }

        public decimal Amount { get; set; }
    }
}