using BlazorCodeChallenge.Components.Services;
using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Helpers;
using BlazorCodeChallenge.Models;
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

    /// <summary>
    /// Dispose : Releases resources used by the instance and resets the FooterBrandService.
    /// </summary>
    public void Dispose()
    {
        // nothing (preferred long-term)
    }

    private void HandleSubmit()
    {
        //loan.Payment = LoanUtils.CalculatePayment(loan.PurchaseAmount, loan.Rate, loan.Term);
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

}
