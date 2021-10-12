using System;
using System.Collections.Generic;
using SimilarWebParser.Converters;
using WebsiteParser.Attributes;
using WebsiteParser.Attributes.StartAttributes;
using WebsiteParser.Converters;

namespace SimilarWebParser.Model
{
    public class SimilarWebInfo
    {
        [Selector("li.websiteRanks-item:nth-child(1) > div:nth-child(2)")]
        [Converter(typeof(StringToIntegerConverter))]
        public int GlobalRank { get; set; }

        [Selector("li.websiteRanks-item:nth-child(2) > div:nth-child(1) > div:nth-child(2) > a:nth-child(1)")]
        public string TopCountry { get; set; }
        
        [Selector("li.websiteRanks-item:nth-child(2) > div:nth-child(2)")]
        [Converter(typeof(StringToIntegerConverter))]
        public int TopCountryRank { get; set; }

        [Selector("li.websiteRanks-item:nth-child(3) > div:nth-child(1) > div:nth-child(2) > a:nth-child(1)")]
        public string Category { get; set; }

        [Selector("li.websiteRanks-item:nth-child(3) > div:nth-child(2)")]
        [Converter(typeof(StringToIntegerConverter))]
        public int CategoryRank { get; set; }

        // Should be int?
        [Selector(".engagementInfo-value--large > span:nth-child(1)")]
        public string TotalVisits { get; set; }

        [Selector("div.engagementInfo-line:nth-child(3) > div:nth-child(1) > span:nth-child(2) > span:nth-child(1)")]
        [Converter(typeof(StringToTimeSpanConverter))]
        public TimeSpan AverageVisitDuration { get; set; }

        [Selector("div.engagementInfo-line:nth-child(4) > div:nth-child(1) > span:nth-child(2) > span:nth-child(1)")]
        [Converter(typeof(StringToDoubleConverter))]
        public double PagesPerVisit { get; set; }

        [Selector("div.engagementInfo-line:nth-child(5) > div:nth-child(1) > span:nth-child(2) > span:nth-child(1)")]
        [Regex("(.*)%")]
        [Converter(typeof(StringToDoubleConverter))]
        public double BounceRate { get; set; }

        public List<TopCountry> TopCountries { get; set; }
        public List<TrafficSource> TrafficSources { get; set; }
        public List<string> AlsoVisitedWebsites { get; set; }
        public List<string> SimilarSites { get; set; }
        public List<TopReferringSite> TopReferringSites { get; set; }

        public override string ToString()
        {
            var res = $"GlobalRank: {GlobalRank} \n" +
                      $"TopCountry: {TopCountry} \n" +
                      $"TopCountryRank: {TopCountryRank} \n" +
                      $"Category: {Category} \n" +
                      $"CategoryRank: {CategoryRank} \n" +
                      $"TotalVisits: {TotalVisits} \n" +
                      $"AverageVisitDuration: {AverageVisitDuration} \n" +
                      $"PagesPerVisit: {PagesPerVisit} \n" +
                      $"BounceRate: {BounceRate}% \n";
            res += "TopCountries(List): \n";
            for (var i = 0; i < TopCountries.Count; i++)
            {
                res += $"\t {i}. {TopCountries[i]} \n";
            }

            res += "TrafficSources(List): \n";
            for (var i = 0; i < TrafficSources.Count; i++)
            {
                res += $"\t {i}. {TrafficSources[i]} \n";
            }

            res += "AlsoVisitedWebsites(List): \n";
            for (var i = 0; i < AlsoVisitedWebsites.Count; i++)
            {
                res += $"\t {i}. {AlsoVisitedWebsites[i]} \n";
            }
            
            res += "SimilarSites(List): \n";
            for (var i = 0; i < SimilarSites.Count; i++)
            {
                res += $"\t {i}. {SimilarSites[i]} \n";
            }

            res += "TopReferringSites(List): \n";
            for (var i = 0; i < TopReferringSites.Count; i++)
            {
                res += $"\t {i}. {TopReferringSites[i]} \n";
            }

            return res;
        }
    }
}