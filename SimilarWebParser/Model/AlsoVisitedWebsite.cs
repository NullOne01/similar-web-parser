using WebsiteParser.Attributes;
using WebsiteParser.Attributes.StartAttributes;

namespace SimilarWebParser.Model
{
    /// <summary>
    /// Parsed AlsoVisitedWebsite data.
    /// </summary>
    [ListSelector(".alsoVisitedSection > div:nth-child(2)", ChildSelector = ".websitePage-listUnderline")]
    internal class AlsoVisitedWebsite
    {
        [Selector(".websitePage-listItemLink")]
        public string WebsiteName { get; set; }
    }
}