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
            catch (Exception ex)
            {
                Logger.ErrorException("Error parsing the URI.", ex);
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
        public static QueryParameters GetQueryParameters(this Uri uri)
        {
            MatchCollection matches = Regex.Matches(
                uri.Query, @"[\?&]((?<name>[^&=]+)=(?<value>[^&=#]*))",
                RegexOptions.Compiled);
            return new QueryParameters(matches.Cast<Match>().ToDictionary(
                m => Uri.UnescapeDataString(m.Groups["name"].Value),
                m => Uri.UnescapeDataString(m.Groups["value"].Value)
            ));
        }
    }
}
