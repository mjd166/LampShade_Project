using _0_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class PaymentRessultModel : PageModel
    {
        public PaymentResult Result;
        public void OnGet(PaymentResult paymentResult)
        {
            Result = paymentResult;
        }
    }
}
