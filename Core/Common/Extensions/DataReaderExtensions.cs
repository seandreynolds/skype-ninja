using System;
using System.Data;

namespace eigenein.SkypeNinja.Core.Common.Extensions
{
    /// <summary>
    /// <see cref="IDataReader"/> extensions that allow to read values by
    /// their names instead of ordinals.
    /// </summary>
    internal static class DataReaderExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();

        public static string GetString(this IDataReader dataReader, string name)
        {
            int ordinal = dataReader.GetOrdinal(name);
            return dataReader.GetString(ordinal);
        }

        public static long GetInt64(this IDataReader dataReader, string name)
        {
            int ordinal = dataReader.GetOrdinal(name);
            return dataReader.GetInt64(ordinal);
        }

        public static TEnum GetEnum<TEnum>(this IDataReader dataReader, string name)
            where TEnum : struct, IConvertible
        {
            int ordinal = dataReader.GetOrdinal(name);
            object o = dataReader.GetValue(ordinal);
            return (TEnum)Enum.ToObject(typeof(TEnum), o);
        }

        public static bool TryGetEnum<TEnum>(
            this IDataReader dataReader,
            string name,
            out TEnum value)
            where TEnum : struct, IConvertible
        {
            int ordinal = dataReader.GetOrdinal(name);
            object o = dataReader.GetValue(ordinal);

            if (o != null && o != DBNull.Value)
            {
                value = (TEnum)Enum.ToObject(typeof(TEnum), o);
                return true;
            }

            value = default(TEnum);
            return false;
        }

        public static DateTime GetDateTime(this IDataReader dataReader, string name)
        {
            int ordinal = dataReader.GetOrdinal(name);
            int seconds = dataReader.GetInt32(ordinal);
            return Epoch.AddSeconds(seconds);
        }
    }
}
