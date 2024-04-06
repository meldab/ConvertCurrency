using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Services.Models
{
    public class AmountHistory
    {
        public string Rate { get; set; }

        public string Date { get; set; }

        public decimal Amount { get; set; }
    }
}
