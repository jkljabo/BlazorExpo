using BlazorCodeChallenge.Services;
using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorCodeChallenge.Components.Pages;

public partial class FizzBuzzValidation
{
    private FizzBuzzModel fizzBuzz = new();
    private List<string> fizzBuzzResults = new();
    private EditContext editContext;

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
        fizzBuzzResults.Clear();

        // Loop through all the numbers between 1 and the stop value.
        for (int i = 1; i <= fizzBuzz.StopValue; i++)
        {
            // Check to see if FizzValue and BuzzValue are both divisable by i first.
            if (i % fizzBuzz.FizzValue == 0 && i % fizzBuzz.BuzzValue == 0)
            {
                fizzBuzzResults.Add("FizzBuzz");

            }
            // Check to see if FizzValue is divisable by i second.
            else if (i % fizzBuzz.FizzValue == 0) 
            {
                fizzBuzzResults.Add("Fizz");
            }
            // Check to see if BuzzValue is divisable by i third.
            else if (i % fizzBuzz.BuzzValue == 0)
            {
                fizzBuzzResults.Add("Buzz");
            }
            // After the first 3 checks, i is not FizzBuzz compatable.
            else
            {
                fizzBuzzResults.Add(i.ToString());
            }
        }
    }
}
