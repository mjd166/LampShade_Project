using System;
using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administrator.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

       
        public SelectList Roles;

        public AccountSearchModel SearchModel { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
        private readonly IAccountApplication _AccountApplication;
        private readonly IRoleApplication _roleApplication;
        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _AccountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            Roles = new SelectList(_roleApplication.List(), "Id", "Name");
            
            Accounts = _AccountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            
            var command = new RegisterAccount
            {
               Roles=_roleApplication.List()
            };


            return Partial("Create", command);
        }

        public JsonResult OnPostCreate(RegisterAccount command)
        {
            return new JsonResult(_AccountApplication.Register(command));
        }

        public IActionResult OnGetEdit(long id)
        {
          
            var account = _AccountApplication.GetDetails(id);
            account.Roles = _roleApplication.List();
            return Partial("Edit", account);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            return new JsonResult(_AccountApplication.Edit(command));
        }


        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword { Id = id };

            return Partial("ChangePassword", command);

        }


        public JsonResult OnPostChangePassword(ChangePassword changePassword)
        {
            var result = _AccountApplication.ChangePassword(changePassword);
            return new JsonResult(result);
        }

    
    }
}
