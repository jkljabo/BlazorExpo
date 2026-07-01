using BlazorCodeChallenge.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorCodeChallenge.Components.UI
{
    public partial class PaymentSchedule
    {
        [Parameter, EditorRequired]
        public List<LoanPayment> Payments { get; set; } = new();
    }
}
