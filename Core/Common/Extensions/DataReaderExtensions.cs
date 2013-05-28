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
        public static string GetString(this IDataReader dataReader, string name)
        {
            int ordinal = dataReader.GetOrdinal(name);
            return dataReader.GetString(ordinal);
        }

        public static bool TryGetEnum<TEnum>(this IDataReader dataReader, string name, out TEnum value)
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
    }
}
