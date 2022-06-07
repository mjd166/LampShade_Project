using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Domain.ArticleAgg;
using BolgManagement.Application.Contracts.Article;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context):base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                //Picture=x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Keywords = x.Keywords,
                PublishDate = x.PublishDate.ToFarsi()
                 
            }).FirstOrDefault(x => x.Id == id);
        }

        public Article GetWithCategory(long id)
        {
            return _context.Articles.Include(x => x.ArticleCategory).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles
                .Include(x=>x.ArticleCategory)
                .Select(x => new ArticleViewModel
            {
                 Id=x.Id,
                 Title=x.Title,
                 Picture=x.Picture,
                 PublishDate=x.PublishDate.ToFarsi(),
                 ShortDescription=x.ShortDescription.Substring(0,Math.Min(x.ShortDescription.Length,50))+"...",
                 Category=x.ArticleCategory.Name,
                 CategoryId=x.CategoryId
                 
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
