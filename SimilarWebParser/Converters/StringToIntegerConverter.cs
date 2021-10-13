using System;
using System.Globalization;
using WebsiteParser.Converters.Abstract;

namespace SimilarWebParser.Converters
{
    /// <summary>
    /// Converter for parsed string data into integer data.
    /// </summary>
    internal class StringToIntegerConverter : IConverter
    {
        public object Convert(object input)
        {
            if (!(input is string))
            {
                throw new FormatException("Input value have to be string");
            }

            return int.Parse(input.ToString(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
        }
    }
}