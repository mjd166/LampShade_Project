using System.Collections.Generic;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administrator.Pages.Accounts.Role
{
    public class IndexModel : PageModel
    {
       
        public List<RoleViewModel> Roles { get; set; }
   
        private readonly IRoleApplication _roleApplication;
        public IndexModel( IRoleApplication roleApplication)
        {
           
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {


            Roles = _roleApplication.List ();
        }

        public IActionResult OnGetCreate()
        {

            var command = new CreateRole
                ();


            return Partial("Create", command);
        }

        public JsonResult OnPostCreate(CreateRole command)
        {
            return new JsonResult(_roleApplication.Create(command));
        }

        public IActionResult OnGetEdit(long id)
        {
          
            var role = _roleApplication.GetDetails(id);
            
            return Partial("Edit", role);
        }

        public JsonResult OnPostEdit(EditRole command)
        {
            return new JsonResult(_roleApplication.Edit(command));
        }



    
    }
}
