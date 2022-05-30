using _0_Framework.Application;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BolgManagement.Application.Contracts.Article;
using System.Collections.Generic;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IFileUploader _fileUploader;
        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operationResult =new OperationResult();
            if (_articleRepository.Exist(x => x.Title == command.Title)) return operationResult.Failed(ApplicationMessages.DoublicatedRecord);

            string categorySlug = _articleCategoryRepository.GetSluBy(command.CategoryId);
            string slug = command.Slug.Slugify();
            string path = $"{categorySlug}/{slug}";
            string filepath = _fileUploader.Upload(command.Picture, path);

            var article = new Article(command.Title, command.CategoryId, command.ShortDescription, command.Description, filepath, command.PictureAlt, command.PictureTitle
                , command.PublishDate.ToGeorgianDateTime(), slug, command.MetaDescription,command.Keywords,command.CanonicalAddress);
            
            _articleRepository.Create(article);
            _articleRepository.Savechanges();
            return operationResult.Succedded();

        }

        public OperationResult Edit(EditArticle command)
        {
            var operationResult = new OperationResult();
            var article = _articleRepository.GetWithCategory(command.Id);
            if (article == null) 
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            if (_articleRepository.Exist(x => x.Title == command.Title && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.DoublicatedRecord);

            var slug = command.Slug.Slugify();
            var categoryslug = article.ArticleCategory.Slug;
            string path = $"{categoryslug}/{slug}";
            var filename = _fileUploader.Upload(command.Picture, path);

            article.Edit(command.Title, command.CategoryId, command.ShortDescription, command.Description, filename, command.PictureAlt, command.PictureTitle,
                command.PublishDate.ToGeorgianDateTime(), slug, command.MetaDescription, command.Keywords, command.CanonicalAddress);

            _articleRepository.Savechanges();
            return operationResult.Succedded();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
