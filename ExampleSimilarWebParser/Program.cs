using System;
using System.IO;
using System.Threading;
using SimilarWebParser;
using SimilarWebParser.Model;

namespace ExampleSimilarWebParser
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            SimilarWebDataParser similarWebDataParser = new SimilarWebDataParser();
            //SimilarWebInfo info = similarWebDataParser.GetWebSiteInfo(File.ReadAllText("HTML2.html"));
            SimilarWebInfo info = similarWebDataParser.GetWebSiteInfoURL("pikabu.ru");
            PrintInfo(info);
        }

        private void StartTimerTest()
        {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            ParserChecker parserChecker = new ParserChecker("eharmony.com");

            // creates a Timer to call CheckStatus() with autoEvent as argument,
            // starting with 1 second delay and calling every 2 seconds.
            var stateTimer = new Timer(parserChecker.TimerTick, autoEvent, 1000, 60000/2);
            autoEvent.WaitOne();
        }

        private static void PrintInfo(SimilarWebInfo info)
        {
            Console.WriteLine(info);
        }
    }
}