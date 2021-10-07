using WebsiteParser.Attributes;
using WebsiteParser.Attributes.StartAttributes;

namespace SimilarWebParser.Model
{
    [ListSelector(".trafficSourcesChart-list", ChildSelector = ".trafficSourcesChart-item")]
    public class TrafficSource
    {
        [Selector(".trafficSourcesChart-title")]
        public string TrafficSourceName { get; set; }
        
        // should be double
        [Selector(".trafficSourcesChart-value")]
        public string Percentage { get; set; }
        
        public override string ToString()
        {
            return $"TrafficSourceName = {TrafficSourceName}, Percentage = {Percentage}";
        }
    }
}