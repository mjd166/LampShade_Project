using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory Command)
        {
            var operation = new OperationResult();
            if (productCategoryRepository.Exist(x=>x.Name  == Command.Name))
             return    operation.Failed(ApplicationMessages.DoublicatedRecord);

            var slug = Command.Slug.Slugify();
            var productcategory = new ProductCategory(Command.Name, Command.Description, Command.Picture, Command.PictureAlt, Command.PictureTitle, Command.Keywords,
                Command.Metadescription,slug);

            productCategoryRepository.Create(productcategory);
            productCategoryRepository.Savechanges();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            OperationResult operation = new OperationResult();

            var productcategory = productCategoryRepository.Get(command.Id);
            if (productcategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (productCategoryRepository.Exist(x=>x.Name ==command.Name && x.Id !=command.Id))
                return operation.Failed(ApplicationMessages.DoublicatedRecord);

            var slug = command.Slug.Slugify();
            productcategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords, 
                command.Metadescription, slug);

            productCategoryRepository.Savechanges();
            return operation.Succedded();
        }

        public EditProductCategory GetDetails(long id)
        {
          return   productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return productCategoryRepository.GetProductCategories();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return productCategoryRepository.Search(searchModel);
        }
    }
}
