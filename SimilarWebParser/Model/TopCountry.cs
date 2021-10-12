using System.ComponentModel;
using SimilarWebParser.Converters;
using WebsiteParser.Attributes;
using WebsiteParser.Attributes.StartAttributes;

namespace SimilarWebParser.Model
{
    [ListSelector("#geo-countries-accordion", ChildSelector = ".accordion-heading")]
    public class TopCountry
    {
        [Selector(".country-name")]
        public string CountryName { get; set; }
        
        [Selector(".traffic-share-valueNumber")]
        [Regex("(.*)%")]
        [Converter(typeof(StringToDoubleConverter))]
        public double Percentage { get; set; }

        public override string ToString()
        {
            return $"CountryName = {CountryName}, Percentage = {Percentage}%";
        }
    }
}