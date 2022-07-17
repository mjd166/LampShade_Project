using System.Collections.Generic;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Orders
{
    //[Authorize(Roles ="1,2")]
    public class IndexModel : PageModel
    {

        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;

        public OrderSearchModel SearchModel;
        public List<OrderViewModel> orders;
        public SelectList Accounts;

        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }

     
        public void OnGet(OrderSearchModel searchModel)
        {
            Accounts = new SelectList(_accountApplication.GetAccounts(), "Id", "FullName");

            orders = _orderApplication.Search(searchModel);
        }
       
       

       
    }
}
