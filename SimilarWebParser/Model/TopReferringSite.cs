using WebsiteParser.Attributes;
using WebsiteParser.Attributes.StartAttributes;

namespace SimilarWebParser.Model
{
    [ListSelector(".websitePage-list", ChildSelector = ".websitePage-listItem")]
    public class TopReferringSite
    {
        [Selector(".websitePage-listItemLink")]
        public string Hostname { get; set; }

        // should be double
        [Selector(".websitePage-trafficShare")]
        public string Percentage { get; set; }

        public override string ToString()
        {
            return $"Hostname = {Hostname}, Percentage = {Percentage}";
        }
    }
}