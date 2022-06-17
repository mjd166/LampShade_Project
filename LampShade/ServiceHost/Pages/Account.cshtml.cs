using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;


        [TempData]
        public string Message { get; set; }
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

            Message = res.Message;
            return RedirectToPage("/Login");
        }

        public IActionResult OnGetLogOut()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }
    }
}
