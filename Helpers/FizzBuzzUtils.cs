namespace BlazorCodeChallenge.Helpers;

public static class FizzBuzzUtils
{
    public static List<string> GenerateResults(
        int fizzValue,
        int buzzValue,
        int stopValue)
    {
        var results = new List<string>();

        for (int i = 1; i <= stopValue; i++)
        {
            if (i % fizzValue == 0 && i % buzzValue == 0)
            {
                results.Add("FizzBuzz");
            }
            else if (i % fizzValue == 0)
            {
                results.Add("Fizz");
            }
            else if (i % buzzValue == 0)
            {
                results.Add("Buzz");
            }
            else
            {
                results.Add(i.ToString());
            }
        }

        return results;
    }
}
