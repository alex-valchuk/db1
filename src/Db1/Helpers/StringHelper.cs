namespace Db1.Helpers
{
    public static class StringHelper
    {
        public static string GetStringOfSigns(int size, char sign)
        {
            var result = new char[size];
            for (var i = 0; i < size; i++)
            {
                result[i] = sign;
            }

            return result.ToString();
        }
    }
}