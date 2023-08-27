namespace Logic.Helper_Classes
{
    public class Converter
    {
        public static string ConvertToCurrency(double price) => $"{price}$";
        public static string ConvertToPercentage(double discountPrice) => $"{discountPrice}%";
        public static double ConvertBack(string stringToConvert)
        {
            if (double.TryParse(stringToConvert[..^1], out double converted))
                return converted;

            return 0;
        }
    }
}
