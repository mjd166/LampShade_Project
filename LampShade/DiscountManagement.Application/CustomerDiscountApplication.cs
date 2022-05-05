using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using System.Collections.Generic;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exist(x => x.DiscountRate == command.DiscountRate && x.ProductId == command.ProductId)) return operation.Failed(ApplicationMessages.DoublicatedRecord);
            
            var customerdiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Reason);
            _customerDiscountRepository.Create(customerdiscount);
            _customerDiscountRepository.Savechanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerdiscount = _customerDiscountRepository.Get(command.Id);
            if (customerdiscount == null) return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_customerDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DoublicatedRecord);


            customerdiscount.Edit(command.ProductId, command.DiscountRate, command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Reason);

            _customerDiscountRepository.Savechanges();
            return operation.Succedded();

        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}
