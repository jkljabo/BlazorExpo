using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Helpers;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Models.UI;
using BlazorCodeChallenge.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorCodeChallenge.Components.Pages;

public partial class MortgageCalculator
{
    private Loan loan = new Loan();
    private EditContext? editContext;
    private bool showSchedule = false;
    private string buttonText = "Show Schedule";

    /// <summary>
    /// OnInitialized : Initializes the component, sets the footer brand, and configures default loan values and the edit context.
    /// </summary>
    /// <remarks>Default loan values are assigned at startup for demonstration purposes.</remarks>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AppState.SetFooterBrand(FooterBrands.MagicSquareCode);

        editContext = new EditContext(loan);

        // Default values at system startup. (remove later)
        loan.PurchaseAmount = 25000;
        loan.Term = 5;
        loan.Rate = 5.0;
    }

    private void HandleSubmit()
    {
        loan = LoanUtils.GetPayments(loan);
    }

    private void ShowScheduleToggle()
    {
        if (showSchedule)
        {
            showSchedule = false;
            buttonText = "Show Schedule";
        }
        else
        {
            showSchedule = true;
            buttonText = "Hide Schedule";
        }
    }

    /// <summary>
    /// Technologies showcased by the Mortgage Calculator page.
    /// </summary>
    private static readonly IReadOnlyList<TechStackItem> Technologies =
    [
        new()
        {
            Name = "Blazor",
            IconClass = "devicon-blazor-original colored"
        },

        new()
        {
            Name = "C#",
            IconClass = "devicon-csharp-plain colored"
        },

        new()
        {
            Name = "Bootstrap",
            IconClass = "devicon-bootstrap-plain colored"
        },

        new()
        {
            Name = "JavaScript",
            IconClass = "devicon-javascript-plain colored"
        },

        new()
        {
            Name = "HTML5",
            IconClass = "devicon-html5-plain colored"
        },

        new()
        {
            Name = "CSS3",
            IconClass = "devicon-css3-plain colored"
        }
    ];
}
