using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            OperationResult operation = new OperationResult();
            if (_productPictureRepository.Exist(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DoublicatedRecord);
            var productpicture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Create(productpicture);
            _productPictureRepository.Savechanges();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProductPicture Command)
        {
            OperationResult operation = new OperationResult();

            var productpicture = _productPictureRepository.Get(Command.Id);
            if (productpicture == null) return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productPictureRepository.Exist(x => x.Picture == Command.Picture && x.ProductId == Command.ProductId && x.Id != Command.Id))
                return operation.Failed(ApplicationMessages.DoublicatedRecord);


            productpicture.Edit(Command.ProductId, Command.Picture, Command.PictureAlt, Command.PictureTitle);
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
