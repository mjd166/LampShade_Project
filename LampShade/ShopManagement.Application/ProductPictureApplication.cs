using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            OperationResult operation = new OperationResult();
            //if (_productPictureRepository.Exist(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
            //    return operation.Failed(ApplicationMessages.DoublicatedRecord);
            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var productslug = product.Slug;
            var productcategoryslug = product.Category.Slug;

            var picturepath = $"{productcategoryslug}/{productslug}";
            var filename = _fileUploader.Upload(command.Picture, picturepath);

            var productpicture = new ProductPicture(command.ProductId, filename, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Create(productpicture);
            _productPictureRepository.Savechanges();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProductPicture Command)
        {
            OperationResult operation = new OperationResult();

            var productpicture = _productPictureRepository.GetWithProductAndCategory(Command.Id);
            if (productpicture == null) return operation.Failed(ApplicationMessages.RecordNotFound);

           
            var productslug = productpicture.Product.Slug;
            var productcategoryslug = productpicture.Product.Category.Slug;

            var picturepath = $"{productcategoryslug}/{productslug}";
            var filename = _fileUploader.Upload(Command.Picture, picturepath);


            productpicture.Edit(Command.ProductId, filename, Command.PictureAlt, Command.PictureTitle);
            _productPictureRepository.Savechanges();
            return operation.Succedded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetials(id);
        }

        public OperationResult Remove(long id)
        {
            var operaton = new OperationResult();
            var productpicture = _productPictureRepository.Get(id);
            if (productpicture == null) return operaton.Failed(ApplicationMessages.RecordNotFound);

            productpicture.Remove();
            _productPictureRepository.Savechanges();
            return operaton.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productpicture = _productPictureRepository.Get(id);
            if (productpicture == null) return operation.Failed(ApplicationMessages.RecordNotFound);
            productpicture.Restore();
            _productPictureRepository.Savechanges();
            return operation.Succedded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
