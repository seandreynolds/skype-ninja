using System;
using System.Collections;
using System.Collections.Generic;

using eigenein.SkypeNinja.Core.Enums;

namespace eigenein.SkypeNinja.Core.Common
{
    /// <summary>
    /// Represents the copying process statistics.
    /// </summary>
    public class Statistics : IEnumerable<KeyValuePair<StatisticsType, int>>
    {
        private readonly IDictionary<StatisticsType, int> values =
            new Dictionary<StatisticsType, int>();

        /// <summary>
        /// Gets the statistics value.
        /// </summary>
        public int this[StatisticsType statisticsType]
        {
            get
            {
                int value;
                if (values.TryGetValue(statisticsType, out value))
                {
                    return value;
                }
                // The statistics value does not exist thus has the initial value.
                return 0;
            }
            set
            {
                values[statisticsType] = value;
            }
        }

        public IEnumerator<KeyValuePair<StatisticsType, int>> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
