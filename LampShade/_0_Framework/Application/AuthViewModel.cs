using System.Collections.Generic;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long AccountId { get; set; }
        public long RoleId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<int>  Permissions { get; set; }

        public AuthViewModel(long accountId, long roleId, string fullname, string username,List<int>permissions)
        {
            AccountId = accountId;
            RoleId = roleId;
            Fullname = fullname;
            Username = username;
            Permissions = permissions;
        }

        public AuthViewModel()
        {
        }
    }
}