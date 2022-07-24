using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InventoryManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryApplication _inventoryApplication;

        public InventoryController(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }


        [HttpGet("{id}")]
        public List<InventoryOperationViewModel> GetOperationsBy(long Id)
        {
           return _inventoryApplication.GetOperationLog(Id);
        }
    }
}
