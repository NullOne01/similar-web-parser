using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SimilarWebParser;
using SimilarWebParser.Model;

namespace SimilarWebParserTests
{
    [TestFixture]
    public class Tests
    {
        /// <summary>
        /// Test for parsing downloaded html files.
        /// </summary>
        [Test]
        [TestCase("HTML1.html")]
        [TestCase("HTML2.html")]
        [TestCase("HTML3.html")]
        public void TestParseLocal(string htmlPath)
        {
            string html = File.ReadAllText(htmlPath);
            Assert.DoesNotThrow(() => SimilarWebDataParser.GetWebSiteInfo(html));
        }

        /// <summary>
        /// Test for downloading HTML and parsing it.
        /// WARNING. Don't use this test too much or you will get banned on SimilarWeb.
        /// </summary>
        [Test]
        [TestCase("eharmony.com")]
        public void TestParseRemote(string domain)
        {
            using (WebClient webClient = new WebClient())
            {
                SetUpWebClient(webClient);
                Assert.DoesNotThrow(() => SimilarWebDataParser.GetWebSiteInfo(webClient, domain));
            }
        }

        /// <summary>
        /// Configure web client to make 403 disappear. 
        /// </summary>
        /// <param name="client"> WebClient to configure. </param>
        private void SetUpWebClient(WebClient client)
        {
            // Headers
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