using System.Web.Http;

namespace CurrencyConverter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            config.MapHttpAttributeRoutes();

        }
    }
}
