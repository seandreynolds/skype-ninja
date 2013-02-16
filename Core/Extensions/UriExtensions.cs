using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using eigenein.SkypeNinja.Core.Helpers;

namespace eigenein.SkypeNinja.Core.Extensions
{
    internal static class UriExtensions
    {
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
