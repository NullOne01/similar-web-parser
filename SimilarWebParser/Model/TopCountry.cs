using WebsiteParser.Attributes;
using WebsiteParser.Attributes.StartAttributes;

namespace SimilarWebParser.Model
{
    [ListSelector("#geo-countries-accordion", ChildSelector = ".accordion-heading")]
    public class TopCountry
    {
        [Selector(".country-name")]
        public string CountryName { get; set; }
        
        // should be double
        [Selector(".traffic-share-valueNumber")]
        public string Percentage { get; set; }

        public override string ToString()
        {
            return $"CountryName = {CountryName}, Percentage = {Percentage}";
        }
    }
}