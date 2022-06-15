using AccountManagement.Application.Contracts.Role;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount :CreateAccount
    {
        public long Id { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }


    
}
