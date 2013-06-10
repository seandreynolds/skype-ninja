using System;
using System.Linq;
using System.Text.RegularExpressions;

using NLog;

using eigenein.SkypeNinja.Core.Common.Helpers;

namespace eigenein.SkypeNinja.Core.Common.Extensions
{
    public static class UriExtensions
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Tries to parse the URI string.
        /// </summary>
        public static bool TryParse(string uriString, out Uri uri)
        {
            try
            {
                uri = new Uri(uriString);
                return true;
            }
            catch (UriFormatException ex)
            {
                Logger.Error("Error parsing the URI: {0}", ex.Message);
                uri = null;
                return false;
            }
        }

        /// <summary>
        /// Gets the query parameters. See http://codereview.stackexchange.com/a/1592/12904.
        /// </summary>
        /// <remarks>
        /// <see cref="System.Web.HttpUtility"/> class is not accessible in Client Profile.
        /// </remarks>
        public static QueryParameters ParseQuery(this Uri uri)
        {
            return ParseQuery(uri.Query);
        }

        public static QueryParameters ParseQuery(string query)
        {
            MatchCollection matches = Regex.Matches(
                query, @"[\?&]((?<name>[^&=]+)=(?<value>[^&=#]*))",
                RegexOptions.Compiled);
            return new QueryParameters(matches.Cast<Match>().ToDictionary(
                m => Uri.UnescapeDataString(m.Groups["name"].Value),
                m => Uri.UnescapeDataString(m.Groups["value"].Value)
            ));
        }
    }
}
