using System;

namespace _0_Framework.Infrastructure
{
    public class NeedPermissionAttribute:Attribute
    {
        public int Permission { get; set; }

        public NeedPermissionAttribute(int permission)
        {
            Permission = permission;
        }
    }
}
