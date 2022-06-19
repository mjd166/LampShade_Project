using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;


        [TempData]
        public string LoginMessage { get; set; }
        [TempData]
        public string RegisterMessage { get; set; }
        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login command)
        {
            var res = _accountApplication.Login(command);
            if (res.IsSuccedded)
                return RedirectToPage("/Index");

            LoginMessage = res.Message;
            return RedirectToPage("/Account");
        }

        public IActionResult OnGetLogOut()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostRegister(RegisterAccount registerAccount)
        {
            var result = _accountApplication.Register(registerAccount);
            if(result.IsSuccedded)
            return RedirectToPage("/Account");

            RegisterMessage = result.Message;
            return RedirectToPage("/Account");
        }
    }
}
