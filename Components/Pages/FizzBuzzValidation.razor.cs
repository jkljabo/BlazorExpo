using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Helpers;
using BlazorCodeChallenge.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorCodeChallenge.Components.Pages;

public partial class FizzBuzzValidation
{
    private FizzBuzzModel fizzBuzz = new();
    private List<string> fizzBuzzResults = new();
    private EditContext editContext = null!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        editContext = new EditContext(fizzBuzz);

        AppState.SetFooterBrand(FooterBrands.MagicSquareCode);
    }

    /// <summary>
    /// Dispose : Releases resources used by the instance and resets the FooterBrandService.
    /// </summary>
    public void Dispose()
    {
        // nothing (preferred long-term)
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
