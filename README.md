# SimilarWebParser

SimilarWebParser is a library which is used for parsing data from www.similarweb.com/website/<domain_name>/

Currently the library has these 2 options:

1) Parsing already downloaded HTML file.
2) Using your configured WebClient to download HTML document and parse it then. You can connect proxy to this WebClient.

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

You can find my full example codes in this repository.

## Parsing already downloaded HTML file

### Example

```csharp
// ...
string htmlContent = File.ReadAllText(PATH_TO_YOUR_DOWNLOADED_HTML);
SimilarWebInfo info = SimilarWebDataParser.GetWebSiteInfo(htmlContent);
Console.WriteLine(info);
// ...
```

## Downloading and parsing HTML file without proxy

This example will fail after many parsings due to SimilarWeb's security system.

### Example

```csharp
// ...
using (WebClient webClient = new WebClient())
{
    // Here we can connect proxy and set headers to bypass SimilarWeb's security system. 
    SetUpWebClient(webClient);
    
    // Here should be your domain to parse information about.
    string domain = "eharmony.com";
    
    SimilarWebInfo info = SimilarWebDataParser.GetWebSiteInfo(webClient, domain);
    Console.WriteLine(info);
}
// ...
```

## Downloading and parsing HTML file using proxy

### Example

I've used https://www.scraperapi.com/ proxy. This example is based on my free trial plan of scraperapi.com proxy.

```csharp
// ...
// My free trial plan. You should use your credentials here.
const string ProxyUrl =
   "http://scraperapi:3b5c821f62d1595c47a0277c0eedcaec@proxy-server.scraperapi.com:8001";
const string ProxyUserName = "scraperapi";
const string ProxyPassword = "3b5c821f62d1595c47a0277c0eedcaec";

using (WebClient webClient = new WebClient())
{
    // Here we give different headers to our query.
    // You can check my full example to see how it can be implemented.
    SetUpWebClient(webClient);
   
    // Here we set up our scraperapi proxy.
    WebProxy proxy = new WebProxy(ProxyUrl);
    proxy.Credentials = new NetworkCredential(ProxyUserName, ProxyPassword);
    webClient.Proxy = proxy;
   
    // Finally download and parse data about "eharmony.com" using our proxy.
    SimilarWebInfo info = SimilarWebDataParser.GetWebSiteInfo(webClient, "eharmony.com");
    Console.WriteLine(info);
}
// ...
```

Code above can give you HTTP error 500 if proxy couldn't download webpage for 60 seconds.
That's how scraperapi.com proxy works.

You can use this code below to wait until downloading is done after some tries.

```csharp
// ...
while (true)
{
     try
     {
         // YOUR PARSING CODE SHOULD BE HERE. 
         break;
     }
     catch (WebException)
     {
         // ignored.
     }
}
// ...
```

# Things to know

1) SimilarWeb has strong security system. After many loads error 403 can be shown (CAPTCHA). To avoid it, you can try to
   do this:
    1) Use proxy and connect it to the WebClient. Proxy will change your IP and SimilarWeb won't track you;
    2) Lower request rate.
    3) Randomize Headers for WebClient.
2) Web scraping is done by using CSS selectors.