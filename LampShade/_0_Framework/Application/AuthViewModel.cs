namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long AccountId { get; set; }
        public long RoleId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }

        public AuthViewModel(long accountId, long roleId, string fullname, string username)
        {
            AccountId = accountId;
            RoleId = roleId;
            Fullname = fullname;
            Username = username;
        }
    }
}