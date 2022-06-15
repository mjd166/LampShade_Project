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

        public IndexModel(IAccountApplication accountApplication)
        {
            _AccountApplication = accountApplication;
            
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            var roles = new List<RoleViewModel>();
            roles.Add(new RoleViewModel
            {
                CreationDate = DateTime.Now.ToFarsi(),
                Id = 1,
                Name = "مدیر سیستم"
            });
            roles.Add(new RoleViewModel
            {
                CreationDate = DateTime.Now.ToFarsi(),
                Id = 2,
                Name = "مدیر سایت"
            });
            roles.Add(new RoleViewModel
            {
                CreationDate = DateTime.Now.ToFarsi(),
                Id = 3,
                Name = "اوپراتور"
            });
            Roles = new SelectList(roles, "Id", "Name");
            Accounts = _AccountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var roles = new List<RoleViewModel>();
            roles.Add(new RoleViewModel
            {
                CreationDate = DateTime.Now.ToFarsi(),
                Id = 1,
                Name = "مدیر سیستم"
            });
            roles.Add(new RoleViewModel
            {
                CreationDate = DateTime.Now.ToFarsi(),
                Id = 2,
                Name = "مدیر سایت"
            });
            roles.Add(new RoleViewModel
            {
                CreationDate = DateTime.Now.ToFarsi(),
                Id = 3,
                Name = "اوپراتور"
            });
            var command = new CreateAccount
            {
               Roles=roles
            };


            return Partial("Create", command);
        }

        public JsonResult OnPostCreate(CreateAccount command)
        {
            return new JsonResult(_AccountApplication.Create(command));
        }

        public IActionResult OnGetEdit(long id)
        {
            var roles = new List<RoleViewModel>();
            roles.Add(new RoleViewModel
            {
                CreationDate = DateTime.Now.ToFarsi(),
                Id = 1,
                Name = "مدیر سیستم"
            });
            roles.Add(new RoleViewModel
            {
                CreationDate = DateTime.Now.ToFarsi(),
                Id = 2,
                Name = "مدیر سایت"
            });
            roles.Add(new RoleViewModel
            {
                CreationDate = DateTime.Now.ToFarsi(),
                Id = 3,
                Name = "اوپراتور"
            });
            var account = _AccountApplication.GetDetails(id);
            account.Roles = roles;
            return Partial("Edit", account);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            return new JsonResult(_AccountApplication.Edit(command));
        }


    
    }
}
