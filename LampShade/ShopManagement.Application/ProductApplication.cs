using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exist(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DoublicatedRecord);


            var slug = command.Slug.Slugify();
            var categoryslug = _productCategoryRepository.GetSlugBy(command.CategoryId);
            var picturepath = $"{categoryslug}//{slug}";
            var filename = _fileUploader.Upload(command.Picture, picturepath);

            var product = new Product(command.Name, command.Code, command.ShortDescription, command.Description,filename
                , command.PictureAlt, command.PictureTitle, command.CategoryId, slug, command.Keywords, command.MetaDescription);

            _productRepository.Create(product);
            _productRepository.Savechanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product == null) return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DoublicatedRecord);

            var slug = command.Slug.Slugify();
            var categoryslug = product.Category.Slug;
            var picturepath = $"{categoryslug}//{slug}";
            var filename = _fileUploader.Upload(command.Picture, picturepath);

            product.Edit(command.Name, command.Code, command.ShortDescription, command.Description,filename
                , command.PictureAlt, command.PictureTitle, command.CategoryId
                , slug, command.Keywords, command.MetaDescription);

            _productRepository.Savechanges();
            return operation.Succedded();

        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

      

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
