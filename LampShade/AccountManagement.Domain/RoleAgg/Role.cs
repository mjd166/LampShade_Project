﻿using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; }
        public List<Permission> Permissions { get; private set; }

        protected Role()
        {
            this.Permissions = new List<Permission>();
            this.Accounts = new List<Account>();
        }

        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            this.Permissions = permissions;
            Accounts = new List<Account>();
        }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            this.Permissions = permissions;
        }
    }
}
