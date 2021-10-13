# SimilarWebParser

SimilarWebParser is a library which is used for parsing data from www.similarweb.com/website/<domain_name>/

Currently the library has these 2 options:

1) Parsing already downloaded HTML file.
2) Using your configured WebClient to download HTML document and parse it then.

# SimilarWebInfo
SimilarWebInfo is a result class, which contains all parsed information that you need.\
It has such properties:

int GlobalRank; \
string TopCountry; \
int TopCountryRank; \
string Category; \
int CategoryRank; \
string TotalVisits (is not int, because idk which format SimilarWeb uses); \
TimeSpan AverageVisitDuration; \
double PagesPerVisit; \
double BounceRate; \
List< TopCountry > TopCountries; // string CountryName, double Percentage \
List< TrafficSource > TrafficSources; // string TrafficSourceName, double Percentage \
List< string > AlsoVisitedWebsites; \
List< string > SimilarSites; \
List< TopReferringSite > TopReferringSites; // string Hostname, double Percentage

# Usage
## Parsing already downloaded HTML file
### Example
```csharp
// ...
string htmlContent = File.ReadAllText(PATH_TO_YOUR_DOWNLOADED_HTML);
SimilarWebInfo info = SimilarWebDataParser.GetWebSiteInfo(htmlContent);
Console.WriteLine(info);
// ...
```

## Downloading and parsing HTML file
### Example
```csharp
// ...
using (WebClient webClient = new WebClient())
{
    // Here we can connect proxy and set headers to avoid SimilarWeb's security system. 
    SetUpWebClient(webClient);
    
    // Here should be your domain to parse information about.
    string domain = "eharmony.com";
    
    SimilarWebInfo info = SimilarWebDataParser.GetWebSiteInfo(webClient, domain);
    Console.WriteLine(info);
}
// ...
```

# Things to know
1) SimilarWeb has strong security system. 
After many loads error 403 can be shown (CAPTCHA).
To avoid it, you can try to do this:
    1) Use proxy and connect it to the WebClient. Proxy will change your IP and SimilarWeb won't track you;
    2) Lower request rate. Postman had no difficulties with 1 request per hour.
    3) Randomize Headers for WebClient. I didn't test it, but it should give some results.
2) Web scraping is done by using CSS selectors.