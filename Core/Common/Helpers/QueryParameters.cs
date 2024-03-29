﻿using System;
using System.Collections.Generic;

namespace eigenein.SkypeNinja.Core.Common.Helpers
{
    public class QueryParameters : Dictionary<string, string>
    {
        /// <summary>
        /// Initializes an instance from the given dictionary.
        /// </summary>
        public QueryParameters(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            // Do nothing.
        }
    }
}
