using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administrator.Pages.Accounts.Role
{
    public class CreateModel : PageModel
    {
        public CreateRole command;
        private readonly IRoleApplication _roleApplication;

        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
        }
        //public IActionResult OnGetCreate()
        //{

        //    var command = new CreateRole
        //        ();


        //    return Partial("Create", command);
        //}

        public IActionResult OnPost(CreateRole command)
        {
           var result= _roleApplication.Create(command);
            return RedirectToPage("Index");
        }
    }
}
