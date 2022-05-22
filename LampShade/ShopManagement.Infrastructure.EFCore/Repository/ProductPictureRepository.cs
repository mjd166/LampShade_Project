using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {

        private readonly ShopContext _Context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _Context = context;
        }

        public EditProductPicture GetDetials(long id)
        {
            return _Context.ProductPictures.Select(x => new EditProductPicture
            {
                Id = x.Id,
                //Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public ProductPicture GetWithProductAndCategory(long id)
        {
            return _Context.ProductPictures.Include(x => x.Product).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _Context.ProductPictures.Include(x => x.Product).Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                Product = x.Product.Name,
                CreationDate = x.CreationDate.ToString(),
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved
            }) ;
            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
