using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using SimilarWebParser;
using SimilarWebParser.Model;

namespace ExampleSimilarWebParser
{
    public class ParserChecker
    {
        private SimilarWebDataParser _similarWebDataParser;
        private int _counter;
        private string _url;
        
        public ParserChecker(string url)
        {
            _similarWebDataParser = new SimilarWebDataParser();
            _url = url;
        }

        public void TimerTick(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            try
            {
                SimilarWebInfo info = _similarWebDataParser.GetWebSiteInfo(File.ReadAllText("HTML.html"));
                _counter++;
                Console.WriteLine($"Try: {_counter}. Works fine!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Task failed after: {_counter} tries.");
                Console.WriteLine(e);
                autoEvent.Set();
            }
        }
    }
}