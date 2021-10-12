using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using SimilarWebParser.Model;
using WebsiteParser;

namespace SimilarWebParser
{
    public class SimilarWebDataParser
    {
        public SimilarWebDataParser()
        {
        }

        public SimilarWebInfo GetWebSiteInfoURL(string url)
        {
            return GetWebSiteInfo(GetHTML(GetFullSimilarWebURL(url)));
        }

        public SimilarWebInfo GetWebSiteInfo(string html)
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

        public string GetFullSimilarWebURL(string url)
        {
            return $"http://www.similarweb.com/website/{url}/";
        }

        private string GetHTML(string url)
        {
            using (WebClient client = new WebClient())
            {
                SetUpWebClient(client);
                string html = client.DownloadString(url);
                // File.WriteAllText("HTML.txt", html);
                // string html = File.ReadAllText("HTML.txt");
                return html;
            }
        }

        private void SetUpWebClient(WebClient client)
        {
            // Headers
            client.Headers["user-agent"] =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:92.0) Gecko/20100101 Firefox/92.0";
            client.Headers["accept"] =
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            client.Headers["cache-control"] = "max-age=0";
            //client.Headers["sec-ch-ua"] =
            //"\"Chromium\";v=\"94\", \"Google Chrome\";v=\"94\", \";Not A Brand\";v=\"99\"";
            client.Headers["sec-ch-ua-mobile"] = "?0";
            client.Headers["sec-ch-ua-platform"] = "\"Windows\"";
            client.Headers["sec-fetch-dest"] = "document";
            client.Headers["sec-fetch-mode"] = "navigate";
            client.Headers["sec-fetch-site"] = "none";
            client.Headers["sec-fetch-user"] = "?1";
            client.Headers["upgrade-insecure-requests"] = "1";
            client.Headers["accept-language"] = "en-EN,en";
            client.Headers["TE"] = "trailers";

            //User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:92.0) Gecko/20100101 Firefox/92.0
            //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
            //Accept-Language: ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3
            //Accept-Encoding: gzip, deflate, br
            //Connection: keep-alive
            //Cookie: locale=ru-ru; sgID=a94b46f3-328b-09f7-7bc6-8babdc851d97; loyal-user={%22date%22:%222021-09-29T12:17:20.004Z%22%2C%22isLoyal%22:true}; _pk_id.1.fd33=88c2f1a2803c0f5e.1632917840.4.1633442311.1633442307.; _gcl_au=1.1.2014629650.1632917841; _wingify_pc_uuid=a61789b1bb4f42ae945b808fe99cec36; _ga=GA1.2.176435971.1632917841; _vwo_uuid_v2=D3789510EAFFE24C7134B1496F55E83FE|042a2d4164c6fef207ff472812daa040; _pxvid=334af8b6-211f-11ec-8f38-4341516a4a46; _vis_opt_s=4%7C; _vwo_uuid=D3789510EAFFE24C7134B1496F55E83FE; _vwo_ds=3%241632917843%3A2.3010558%3A%3A; _fbp=fb.1.1632917842163.153953667; _hjid=682841dc-57d9-432e-8b77-5ac7b5c6cb92; __qca=P0-1830224500-1632917842026; wingify_donot_track_actions=0; _sn_m={"r":{"n":0}}; _sn_a={"a":{"s":1633442311691,"e":1633442311688},"v":"957901c8-e800-4b36-9580-7fde69ccb0d1"}; visitor_id597341=750496173; visitor_id597341-hash=34e91d6158f02e5bde73dce7b7074ff8483254f38f5ab75d93c312f8a4ba63fdc9dd427508327fad6f82c9d6f9da4950c7840b07; intercom-id-e74067abd037cecbecb0662854f02aee12139f95=4f9e4995-1434-48f4-87a2-ee69b0b8d6c3; intercom-session-e74067abd037cecbecb0662854f02aee12139f95=; _gid=GA1.2.1254641407.1633382378; _px3=1dd529328677a75251c4180c81f6c0075a2ba86abf1bf09dfb6725f5bbe09a25:czMvYwl1QKhUqSbTDTRxgW2qeq8Gz32q8mDhUhK8YeaWH4rlXadZ/ZUqrbSSUnbpL8U7ulqkkAnyNI88CKFYWQ==:1000:Rdn9kRpJZeWIoibjbSbzwqqTh7DOeM3o1d/wFy4Ohiw5FDuPVmmXQZDyz59LFE2OrhuAIYcoMchof2EI8CGAdRPhb/5a8jafZf2MaQYPfPvrm2qSF0shrHVsvvcMrzJsCFIg1FDPnehyjtKEPYL/WBw4ysidxBJc7s9ZJAH8vfL5snP/+vYkF46NUZaL+FLcLBIM5nt3nE8dB/imklopYA==; pxcts=4a474a41-25e4-11ec-a059-afbf3e594b4f; _pxff_rf=1; _pxff_fp=1; .AspNetCore.Antiforgery.xd9Q-ZnrZJo=CfDJ8MZK45L5wIRGm3Bn15aGrTxoIY_2PUgmcucIkSB3lKYH0VhPeXMesKa9TEYZ8D_kSVq95GCGhwGS7Z5ToHazE2eGNrPAVnzrGMFStkoHXCu7sVAOIrpvqM4SZ2zJUAYqOeke1wOUbQ4AxfVXocsWAs8; _pk_ses.1.fd33=*; fsrndid=false; mp_7ccb86f5c2939026a4b5de83b5971ed9_mixpanel=%7B%22distinct_id%22%3A%20%2217c317ce43b175-03a39cf904f41d8-4c3e2778-1fa400-17c317ce43c7f1%22%2C%22%24device_id%22%3A%20%2217c317ce43b175-03a39cf904f41d8-4c3e2778-1fa400-17c317ce43c7f1%22%2C%22sgId%22%3A%20%22a94b46f3-328b-09f7-7bc6-8babdc851d97%22%2C%22site_type%22%3A%20%22Lite%22%2C%22%24initial_referrer%22%3A%20%22%24direct%22%2C%22%24initial_referring_domain%22%3A%20%22%24direct%22%2C%22session_id%22%3A%20%22281953f1-3419-4e49-a740-36429bbfa17f%22%2C%22session_first_event_time%22%3A%20%222021-09-29T12%3A17%3A22.333Z%22%2C%22url%22%3A%20%22https%3A%2F%2Fwww.similarweb.com%2Fru%2Fwebsite%2Feharmony.com%2F%22%2C%22is_sw_user%22%3A%20false%2C%22language%22%3A%20%22ru-ru%22%2C%22section%22%3A%20%22website%22%2C%22sub_section%22%3A%20%22%22%2C%22sub_sub_section%22%3A%20%22%22%2C%22page_number%22%3A%20%221%22%2C%22first_time_visitor%22%3A%20false%2C%22last_event_time%22%3A%201633442310962%2C%22entity_name%22%3A%20%22eharmony.com%22%2C%22entity_id%22%3A%20%22eharmony.com%22%2C%22main_category%22%3A%20%22Community_and_Society%22%2C%22sub_category%22%3A%20%22Romance_and_Relationships%22%7D; sc_is_visitor_unique=rx8617147.1633442308.8509910BD3074F571FCA24A24D71A445.4.4.4.4.4.4.3.2.1; _uetsid=c72aa620255811eca8230d56644e5df7; _uetvid=321d7960211f11ecacf5e5ebaef36bf8; _gat_gtag_UA_42469261_1=1; _vis_opt_test_cookie=1; _vwo_sn=524466%3A1; _hjIncludedInSessionSample=0; _hjAbsoluteSessionInProgress=0; SNS=1; _sn_n={"a":{"i":"77988a56-8434-49c3-bf36-87664734fd7b"}}
            //Upgrade-Insecure-Requests: 1
            //Sec-Fetch-Dest: document
            //Sec-Fetch-Mode: navigate
            //Sec-Fetch-Site: none
            //Sec-Fetch-User: ?1
            //Cache-Control: max-age=0
            //TE: trailers


            // Encoding
            client.Encoding = Encoding.UTF8;

            //client.Proxy = new WebProxy("http://13.232.139.182:80/", true);
        }
    }
}