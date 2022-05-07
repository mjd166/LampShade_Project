using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using System.Collections.Generic;

namespace DiscountManagement.Domain.ColleageuDiscountAgg
{
    public interface IColleagueDiscountRepository:IRepository<long,ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueSearchModel searchModel);
    }
}
