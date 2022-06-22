using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administrator.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;
        public EditRole Command;
        public List<SelectListItem> Permissions = new List<SelectListItem>();
        private readonly IEnumerable<IPermissionExposer> _exposers;

        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication = roleApplication;
            _exposers = exposers;
        }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);
            var permissions = new List<PermissionDto>();
            foreach (var expos in _exposers)
            {
                var exposedpermission = expos.Expose();
                foreach (var (Key,Value) in exposedpermission)
                {
                    permissions.AddRange(Value);
                    var groups = new SelectListGroup()
                    {
                        Name = Key
                    };
                    foreach (var permiss in Value)
                    {
                        var item = new SelectListItem(permiss.Name, permiss.Code.ToString())
                        {
                            Group = groups
                        };
                        if (Command.Permissions.Any(x => x.Code == permiss.Code))
                        {
                            item.Selected = true;
                        }
                        Permissions.Add(item);
                    }
                }
            }
        }



        public IActionResult OnPost(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            return RedirectToPage("Index");
        }

    }


}
