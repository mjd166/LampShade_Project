using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public  interface IProductPictureRepository:IRepository<long,ProductPicture>
    {
        EditProductPicture GetDetials(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }

}
