namespace BunnySurfers.API.Utilities
{
    public static class EnumUtilities
    {
        public static string DescribeValidValues<TEnum>() where TEnum : struct, Enum
        {
            var names = Enum.GetNames<TEnum>();
            var values = (int[])Enum.GetValuesAsUnderlyingType<TEnum>();
            var numValues = names.Length;
            string[] valueStrings = new string[numValues];
            for (int i = 0; i < numValues; i++)
                valueStrings[i] = $"{names[i]} ({values[i]})";
            return string.Join("; ", valueStrings);
        }

        public static Dictionary<int, string> ConvertToDict<TEnum>() where TEnum : struct, Enum
        {
            var names = Enum.GetNames<TEnum>();
            var values = (int[])Enum.GetValuesAsUnderlyingType<TEnum>();
            var numValues = names.Length;
            Dictionary<int, string> enumAsDict = [];
            for (int i = 0; i < numValues; i++)
                enumAsDict[values[i]] = names[i];
            return enumAsDict;
        }
    }
}
