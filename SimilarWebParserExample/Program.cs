using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using SimilarWebParser;
using SimilarWebParser.Model;

namespace SimilarWebParserExample
{
    internal class Program
    {
        // This is my free trial proxy data.
        private const string ProxyUrl =
            "http://scraperapi:3b5c821f62d1595c47a0277c0eedcaec@proxy-server.scraperapi.com:8001";
        private const string ProxyUserName = "scraperapi";
        private const string ProxyPassword = "3b5c821f62d1595c47a0277c0eedcaec";

        public static void Main(string[] args)
        {
            Console.WriteLine("Example 1");
            Console.WriteLine("Parsing downloaded html file: ");
            try
            {
                Example1();
            }
            catch
            {
                Console.WriteLine("Couldn't parse local copy of SimilarWeb HTML. ");
            }


            Console.WriteLine("---------------------------------------------");


            Console.WriteLine("Example 2");
            const string domainToParse = "eharmony.com";
            Console.WriteLine($"Download (without proxy) and parse HTML page for domain {domainToParse}: ");
            try
            {
                Example2(domainToParse);
            }
            catch
            {
                Console.WriteLine("Couldn't download and parse SimilarWeb without proxy.");
            }


            Console.WriteLine("---------------------------------------------");


            Console.WriteLine("Example 3");
            Console.WriteLine($"We download the same SimilarWeb data about {domainToParse} many times using proxy.");
            Console.WriteLine("Without proxy it would be impossible.");
            Console.WriteLine();
            Example3(domainToParse);
        }

        /// <summary>
        /// Example 1 to parse local copy of SimilarWeb HTML page.
        /// </summary>
        private static void Example1()
        {
            // HTML1.html should be located in the same directory with executable file.
            string htmlContent = File.ReadAllText("HTML1.html");
            SimilarWebInfo info = SimilarWebDataParser.GetWebSiteInfo(htmlContent);
            Console.WriteLine(info);
        }

        /// <summary>
        /// Example 2 to parse downloaded SimilarWeb HTML page for <paramref name="domain"/> without proxy.
        /// </summary>
        /// <param name="domain"> Domain to parse SimilarWeb data about. </param>
        private static void Example2(string domain)
        {
            /* HTML data should be downloaded using WebClient.
             * If no CAPTCHA is shown, then this parsing would be working fine.
             * Otherwise, it will give you an exception.
             * To avoid CAPTCHA (403 code), you can use these methods:
             * 1) Use proxy and connect it to the WebClient. Proxy will change your IP and SimilarWeb won't track you;
             * 2) Lower request rate. Postman had no difficulties with 1 request per hour.
             * 3) Randomize Headers for WebClient. I didn't test it, but it should give some results.
             */
            using (WebClient webClient = new WebClient())
            {
                SetUpWebClient(webClient);
                SimilarWebInfo info = SimilarWebDataParser.GetWebSiteInfo(webClient, domain);
                Console.WriteLine(info);
            }
        }

        /// <summary>
        /// Example 3 to parse downloaded SimilarWeb HTML page from <paramref name="domain"/> with proxy.
        /// </summary>
        /// <param name="domain"> Domain to parse SimilarWeb data about. </param>
        private static void Example3(string domain)
        {
            int parseNum = 0;
            while (parseNum < 500)
            {
                Console.WriteLine($"Download and parse HTML page for domain (PROXY) {domain}: ");
                Console.WriteLine($"Time start: {DateTime.Now}");

                // Trying to parse until proxy error is not given.
                while (true)
                {
                    try
                    {
                        ExampleProxy(domain);
                        break;
                    }
                    catch (WebException)
                    {
                        // scraperapi proxy gives error 500 if it can't download page for 60 seconds.
                        // by using this try/catch we give it extra time (1 more minute on failure).
                        Console.WriteLine($"Timeout on downloading web page! Time: {DateTime.Now}");
                        Console.WriteLine("Giving extra 60 seconds...");
                    }
                }

                parseNum++;
                Console.WriteLine("Webpage was parsed successfully!");
                Console.WriteLine($"Time end: {DateTime.Now}");
                Console.WriteLine($"You've downloaded and parsed \"{domain}\" {parseNum} times. ");

                Console.WriteLine("---------------------------------");
            }
        }

        /// <summary>
        /// Example to parse downloaded SimilarWeb HTML page from <paramref name="domain"/> using scraperapi proxy.
        /// </summary>
        /// <param name="domain"> URL to parse SimilarWeb data from. </param>
        private static void ExampleProxy(string domain)
        {
            using (WebClient webClient = new WebClient())
            {
                // Here we give different headers to our query.
                SetUpWebClient(webClient);

                // Here we set up our scraperapi proxy.
                WebProxy proxy = new WebProxy(ProxyUrl);
                proxy.Credentials = new NetworkCredential(ProxyUserName, ProxyPassword);
                webClient.Proxy = proxy;

                // Finally download and parse using our proxy.
                SimilarWebInfo info = SimilarWebDataParser.GetWebSiteInfo(webClient, domain);
                Console.WriteLine(info);
            }
        }

        /// <summary>
        /// Configure web client to make 403 disappear. 
        /// </summary>
        /// <param name="client"> WebClient to configure. </param>
        private static void SetUpWebClient(WebClient client)
        {
            // Some headers to look like a human. \\
            // Random user agent.
            client.Headers["user-agent"] = RandomUa.RandomUserAgent;
            client.Headers["accept"] =
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            client.Headers["cache-control"] = "max-age=0";
            client.Headers["sec-ch-ua-mobile"] = "?0";
            client.Headers["sec-ch-ua-platform"] = "\"Windows\"";
            client.Headers["sec-fetch-dest"] = "document";
            client.Headers["sec-fetch-mode"] = "navigate";
            client.Headers["sec-fetch-site"] = "none";
            client.Headers["sec-fetch-user"] = "?1";
            client.Headers["upgrade-insecure-requests"] = "1";
            client.Headers["accept-language"] = "en-EN,en";
            client.Headers["TE"] = "trailers";

            // Encoding
            client.Encoding = Encoding.UTF8;
        }
    }
}