using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Helpers;
using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Components.Pages;

public partial class FizzBuzz
{
    private FizzBuzzModel fizzBuzz = new();
    private List<string> fizzBuzzResults = new();

    /// <summary>
    /// OnInitialized : Initializes the component, sets the footer brand, and configures default loan values and the edit context.
    /// </summary>
    /// <remarks>Default loan values are assigned at startup for demonstration purposes.</remarks>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AppState.SetFooterBrand(FooterBrands.MagicSquareCode);
    }

    /// <summary>
    /// Generates FizzBuzz results for numbers from 1 to the specified stop value.
    /// </summary>
    private void GenerateFizzBuzzResults()
    {
        fizzBuzzResults =
            FizzBuzzUtils.GenerateResults(
                fizzBuzz.FizzValue,
                fizzBuzz.BuzzValue,
                fizzBuzz.StopValue);
    }
}
