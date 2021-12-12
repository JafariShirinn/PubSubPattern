using System.IO;
using System.Xml.Linq;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain
{
    public class XmlHelper
    {
        private readonly string _xmlPath = $"{System.AppDomain.CurrentDomain.BaseDirectory}\\ForecastResults.xml";

        public void UpdateForecast(string media, WeatherForecastModel weatherForecastModel)
        {
            XDocument document;

            if (!File.Exists(_xmlPath))
                CreateXml();


            document = XDocument.Load(_xmlPath);

            var root = document.Element("Forecast");

            var currentElement = document.Element(media);

            if (currentElement == null)
            {
                root?.Add(new XElement(media, new XElement("Summary", weatherForecastModel.Summary)));
            }
            else
            {
                currentElement.Add(new XElement("Summary", weatherForecastModel.Summary));
            }

            document.Save(_xmlPath);
        }

        public string ReadForecast()
        {
            if (!File.Exists(_xmlPath))
                CreateXml();

            var document = XDocument.Load(_xmlPath);

            return JsonConvert.SerializeXNode(document);
        }

        public void DeleteForecastXmlFile()
        {
            if (File.Exists(_xmlPath))
                File.Delete(_xmlPath);
        }

        private void CreateXml()
        {
            var document = new XDocument(new XElement("Forecast", ""));
            document.Save(_xmlPath);
        }
    }
}
