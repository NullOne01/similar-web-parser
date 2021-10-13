using System;
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
        public static void Main(string[] args)
        {
            Console.WriteLine("Example 1");
            Console.WriteLine("Parsing downloaded html file: ");
            Example1();


            Console.WriteLine("---------------------------------------------");


            Console.WriteLine("Example 2");
            const string domainToParse = "pikabu.ru";
            Console.WriteLine($"Download and parse HTML page for domain {domainToParse}: ");
            Example2(domainToParse);
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
        /// Example 2 to parse downloaded SimilarWeb HTML page for <paramref name="domain"/>.
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
        /// Configure web client to make 403 disappear. 
        /// </summary>
        /// <param name="client"> WebClient to configure. </param>
        private static void SetUpWebClient(WebClient client)
        {
            // Some headers to look like a human.
            client.Headers["user-agent"] =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:92.0) Gecko/20100101 Firefox/92.0";
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