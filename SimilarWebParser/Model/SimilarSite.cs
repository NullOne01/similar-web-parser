using WebsiteParser.Attributes;
using WebsiteParser.Attributes.StartAttributes;

namespace SimilarWebParser.Model
{
    /// <summary>
    /// Parsed SimilarSite data.
    /// </summary>
    [ListSelector(".similarSitesList.similarity", ChildSelector = ".similarSitesList-item")]
    internal class SimilarSite
    {
        [Selector(".similarSitesList-title", Attribute = "href")]
        [Regex("/website/(.*)")]
        public string WebsiteName { get; set; }   
    }
}