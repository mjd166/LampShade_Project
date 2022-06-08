using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;


        public ArticleQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public ArticleQueryModel GetArticelDetails(string slug)
        {
            var article = _blogContext.Articles
                   .Where(x => x.PublishDate <= DateTime.Now)
                   .Include(x => x.ArticleCategory)
                   .Select(x => new ArticleQueryModel
                   {
                       Id = x.Id,
                       Title = x.Title,
                       Picture = x.Picture,
                       PictureAlt = x.PictureAlt,
                       PictureTitle = x.PictureTitle,
                       ShortDescription = x.ShortDescription,
                       Description = x.Description,
                       MetaDescription = x.MetaDescription,
                       CategoryId = x.ArticleCategory.Id,
                       CategoryName = x.ArticleCategory.Name,
                       PublishDate = x.PublishDate.ToFarsi(),
                       Slug = x.Slug,
                       Keywords = x.Keywords,
                       CanonicalAddress = x.CanonicalAddress,
                       CategorySlug = x.ArticleCategory.Slug,

                   }).FirstOrDefault(x => x.Slug == slug);
            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split('،').ToList();

            return article;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Include(x => x.ArticleCategory)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Slug = x.Slug,

                }).OrderByDescending(x => x.Id).Take(5).ToList();
        }


    }
}
