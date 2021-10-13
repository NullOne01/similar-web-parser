using SimilarWebParser.Converters;
using WebsiteParser.Attributes;
using WebsiteParser.Attributes.StartAttributes;

namespace SimilarWebParser.Model
{
    /// <summary>
    /// Parsed TopReferringSite data.
    /// </summary>
    [ListSelector(".websitePage-list", ChildSelector = ".websitePage-listItem")]
    public class TopReferringSite
    {
        [Selector(".websitePage-listItemLink")]
        public string Hostname { get; set; }

        [Selector(".websitePage-trafficShare")]
        [Regex("(.*)%")]
        [Converter(typeof(StringToDoubleConverter))]
        public double Percentage { get; set; }

        public override string ToString()
        {
            return $"Hostname = {Hostname}, Percentage = {Percentage}%";
        }
    }
}