using _0_Framework.Application;
using System.Collections.Generic;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueSearchModel searchModel);
        OperationResult Remove(long id);
        OperationResult Restore(long id);


    }

}