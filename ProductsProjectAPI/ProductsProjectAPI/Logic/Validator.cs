using System.Diagnostics;

namespace ProductsProjectAPI.Logic
{
    public class Validator
    {
        public static bool IsValidCategoryCode(string categoryCode)
        {
            if (categoryCode.Length == 6) 
            {
                try
                {
                    string first3 = categoryCode.Substring(0, 3);
                    bool allLetters = first3.All(char.IsLetter);

                    string last3 = categoryCode.Substring(3, 3);
                    bool allNumbers = last3.All(char.IsDigit);

                    return allLetters && allNumbers;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    return false;
                }
            }

            return false;
        }
    }
}
