using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Services.Utils
{
    public class Configuration
    {
        public static string CurrencyData
        {
            get
            {
                var result = ConfigurationManager.AppSettings["CurrencyDataUrl"];

                if (string.IsNullOrWhiteSpace(result))
                {
                    return string.Empty;
                }
                return result;
            }
        }
    }
}
