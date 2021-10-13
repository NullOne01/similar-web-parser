using System.Linq;
using System.Net;
using SimilarWebParser.Model;
using WebsiteParser;

namespace SimilarWebParser
{
    public static class SimilarWebDataParser
    {
        /// <summary>
        /// Get parsed SimilarWeb data about <paramref name="domain"/>. HTML is loaded using <paramref name="webClient"/>
        /// </summary>
        /// <param name="webClient"> WebClient to use. Modify it to avoid 403. </param>
        /// <param name="domain"> Domain to parse information about. </param>
        /// <returns> Parsed SimilarWeb data about given <paramref name="domain"/>. </returns>
        public static SimilarWebInfo GetWebSiteInfo(WebClient webClient, string domain)
        {
            string html = webClient.DownloadString(GetFullSimilarWebURL(domain));
            return GetWebSiteInfo(html);
        }

        /// <summary>
        /// Get parsed SimilarWeb data from <paramref name="html"/> content.
        /// </summary>
        /// <param name="html"> HTML content of a SimilarWeb page. </param>
        /// <returns> Parsed SimilarWeb data of given HTML. </returns>
        public static SimilarWebInfo GetWebSiteInfo(string html)
        {
            SimilarWebInfo similarWebInfo =
                WebContentParser.Parse<SimilarWebInfo>(html);
            similarWebInfo.TopCountries = WebContentParser.ParseList<TopCountry>(html).ToList();
            similarWebInfo.TrafficSources = WebContentParser.ParseList<TrafficSource>(html).ToList();
            similarWebInfo.AlsoVisitedWebsites = WebContentParser.ParseList<AlsoVisitedWebsite>(html)
                .Select(info => info.WebsiteName).ToList();
            similarWebInfo.SimilarSites = WebContentParser.ParseList<SimilarSite>(html)
                .Select(info => info.WebsiteName).ToList();
            similarWebInfo.TopReferringSites = WebContentParser.ParseList<TopReferringSite>(html).ToList();
            return similarWebInfo;
        }

        /// <summary>
        /// Get full SimilarWeb URL from domain name.
        /// </summary>
        /// <param name="domain"> Domain name of the site to parse. </param>
        /// <returns> Full SimilarWeb URL. </returns>
        public static string GetFullSimilarWebURL(string domain)
        {
            return $"http://www.similarweb.com/website/{domain}/";
        }
    }
}