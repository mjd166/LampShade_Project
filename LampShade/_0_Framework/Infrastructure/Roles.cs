namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string ContentUploader = "2";
        public const string SystemUser = "3";


        public static string GetRoleBy(long roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "مدیر سیستم";

                case 2:
                    return "محتوا گزار";
                case 3:
                    return "کاربر سیستم";

                default:
                    return "";
                   

            }
        }
    }
}
