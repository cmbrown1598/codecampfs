namespace SampleTestableClasses
{
    public static class ConversionExtensions
    {
        public static string ToPercentageString(this decimal value)
        {
            return string.Format("{0:0.00000000}%", decimal.Round(value * 100.0M, 8));
        }

        public static string ToPercentageString(this decimal? value, decimal? defaultValue = null, string nullString = "")
        {
            return value.HasValue
                ? value.Value.ToPercentageString()
                : defaultValue.ToPercentageString(nullString);
        }

        private static string ToPercentageString(this decimal? value, string defaultPercentage = null)
        {
            return value.HasValue
                ? ToPercentageString(value.Value)
                : defaultPercentage;
        }
    }
}
