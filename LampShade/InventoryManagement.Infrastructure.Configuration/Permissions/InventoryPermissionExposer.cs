﻿using _0_Framework.Infrastructure;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Infrastructure.Configuration.Permissions
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory",new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.ListInventory,"ListInventory"),
                        new PermissionDto(InventoryPermissions.SearchInventory,"SearchInventory"),
                        new PermissionDto(InventoryPermissions.CreateInventory,"CreateInventory"),
                        new PermissionDto(InventoryPermissions.EditInventory,"EditInventory"),
                        new PermissionDto(InventoryPermissions.IncreaseInventory,"IncreaseInventory"),
                        new PermissionDto(InventoryPermissions.ReduceInventory,"RedcueInventory"),
                        new PermissionDto(InventoryPermissions.OperationLog,"OperationLog"),
                    }
                }
            };
        }
    }
}
