using SimilarWebParser.Converters;
using WebsiteParser.Attributes;
using WebsiteParser.Attributes.StartAttributes;

namespace SimilarWebParser.Model
{
    /// <summary>
    /// Parsed TrafficSource data.
    /// </summary>
    [ListSelector(".trafficSourcesChart-list", ChildSelector = ".trafficSourcesChart-item")]
    public class TrafficSource
    {
        [Selector(".trafficSourcesChart-title")]
        public string TrafficSourceName { get; set; }
        
        [Selector(".trafficSourcesChart-value")]
        [Regex("(.*)%")]
        [Converter(typeof(StringToDoubleConverter))]
        public double Percentage { get; set; }
        
        public override string ToString()
        {
            return $"TrafficSourceName = {TrafficSourceName}, Percentage = {Percentage}%";
        }
    }
}