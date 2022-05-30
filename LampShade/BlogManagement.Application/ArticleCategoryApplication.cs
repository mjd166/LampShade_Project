using _0_Framework.Application;
using BlogManagement.Domain.ArticleCategoryAgg;
using BolgManagement.Application.Contracts.ArticleCategory;
using System.Collections.Generic;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult CreateArticleCategory(CreateArticleCategory command)
        {
            var operationResult = new OperationResult();
            if (_articleCategoryRepository.Exist(x => x.Name == command.Name)) return operationResult.Failed(ApplicationMessages.DoublicatedRecord);

            var slug = command.Slug.Slugify();
            var filepath = _fileUploader.Upload(command.Picture, slug);
            var articleCategory = new ArticleCategory(command.Name,filepath,command.PictureAlt,command.PictureTitle, command.Description, command.ShowOrder, slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.Savechanges();
            return operationResult.Succedded();
        }

        public OperationResult EditArticleCategory(EditArticleCategory command)
        {
            var operationResult = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(command.Id);
            if (articleCategory == null) return operationResult.Failed(ApplicationMessages.RecordNotFound);
            if (_articleCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id)) return operationResult.Failed(ApplicationMessages.DoublicatedRecord);

            var slug = command.Slug.Slugify();
            string filename = _fileUploader.Upload(command.Picture, slug);
            articleCategory.Edit(command.Name, filename,command.PictureAlt,command.PictureTitle, command.Description, command.ShowOrder, slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);
            _articleCategoryRepository.Savechanges();
            return operationResult.Succedded();
        }

        public EditArticleCategory GetDetials(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}
