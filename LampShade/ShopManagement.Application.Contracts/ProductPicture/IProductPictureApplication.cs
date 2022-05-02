using _0_Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture Command);

        EditProductPicture GetDetails(long id);

        OperationResult Remove(long id);

        OperationResult Restore(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
