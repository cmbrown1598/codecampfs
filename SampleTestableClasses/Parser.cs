using System;
using System.Globalization;

namespace SampleTestableClasses
{

    public class DateTimeZoneAwareParser
    {
        //    "ddd MMM d HH:mm:ss yyyy", 
        //    "ddd MMM d HH:mm:ss EST yyyy",
        //    "ddd MMM d HH:mm:ss EDT yyyy", 
        //    "ddd MMM d HH:mm:ss PST yyyy", 
        //    "ddd MMM d HH:mm:ss PDT yyyy"

        /// <summary>
        /// This method ignores any time zone short code (EST, PST, etc) in a DateTime formatted string.  Such short-codes
        /// are non-standard because these abbreviations are not unique.
        /// </summary>
        /// <param name="source">The string to convert in this format: ddd MMM d HH:mm:ss EST yyyy</param>
        /// <param name="offset">The DateTime value if the source could be parsed; otherwise DateTime.MinValue.</param>
        /// <returns>True of the source string could be converted to a DateTime; otherwise false.</returns>
        public static bool TryParseOffset(string source, out DateTime offset)
        {
            if (!string.IsNullOrWhiteSpace(source))
            {
                //  EXAMPLE:   "ddd MMM d HH:mm:ss EST yyyy"
                // prepare source
                var pre = source.Substring(0, source.Length - " EST yyyy".Length);
                var post = source.Substring(source.Length - 4);
                var toParse = string.Join(" ", pre, post);
                if (DateTime.TryParseExact(toParse, "ddd MMM d HH:mm:ss yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AllowInnerWhite, out offset))
                {
                    return true;
                }
            }
            offset = default(DateTime);
            return false;
        }
    }
}
