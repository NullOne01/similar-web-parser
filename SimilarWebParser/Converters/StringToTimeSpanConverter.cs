using System;
using System.Globalization;
using WebsiteParser.Converters.Abstract;

namespace SimilarWebParser.Converters
{
    /// <summary>
    /// Converter for parsed string data into TimeSpan data.
    /// </summary>
    internal class StringToTimeSpanConverter : IConverter
    {
        public object Convert(object input)
        {
            if (!(input is string))
            {
                throw new FormatException("Input value have to be string");
            }

            return TimeSpan.Parse(input.ToString(), CultureInfo.InvariantCulture);
        }
    }
}