using CurrencyConverter.Services.Models;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CurrencyConverter.Services.Utils
{
    public class XmlReader
    {
        public static async Task<HttpResponseMessage> ReadCurrencyListFromXml()
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                var path = Configuration.CurrencyData;
                response = await client.GetAsync(path);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            return response;
        }

        public static async Task<EcbEnvelope> DeserializedXml(HttpResponseMessage httpResponseMessage)
        {
            var model = new EcbEnvelope();
            var result = await httpResponseMessage.Content.ReadAsStringAsync();

            var serializer = new XmlSerializer(typeof(EcbEnvelope));
            using (var streamReader = new StringReader(result))
            {
                model = serializer.Deserialize(streamReader) as EcbEnvelope;
            }

            return model;
        }
    }
}

