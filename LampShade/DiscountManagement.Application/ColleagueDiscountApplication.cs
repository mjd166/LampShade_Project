using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleageuDiscountAgg;
using System.Collections.Generic;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DoublicatedRecord);
            var colleaguediscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(colleaguediscount);
            _colleagueDiscountRepository.Savechanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleaguediscount = _colleagueDiscountRepository.Get(command.Id);
            if (colleaguediscount == null) return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_colleagueDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DoublicatedRecord);
            colleaguediscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Savechanges();
            return operation.Succedded();

        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleague = _colleagueDiscountRepository.Get(id);
            if (colleague == null) return operation.Failed(ApplicationMessages.RecordNotFound);
            colleague.Remove();
            _colleagueDiscountRepository.Savechanges();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleague = _colleagueDiscountRepository.Get(id);
            if (colleague == null) return operation.Failed(ApplicationMessages.RecordNotFound);
            colleague.Restore();
            _colleagueDiscountRepository.Savechanges();
            return operation.Succedded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
