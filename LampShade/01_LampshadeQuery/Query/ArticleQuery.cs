using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
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
                       CategorySlug = x.ArticleCategory.Slug

                   }).FirstOrDefault(x => x.Slug == slug);
            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split('،').ToList();

            var comments = _commentContext.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,

                    Name = x.Name,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ParentId=x.ParentId,
                   // ParentName = x.Parent.Name

                }
                ).OrderByDescending(x=>x.Id).AsNoTracking().ToList();

            foreach(var comment in comments)
            {
                if (comment.ParentId > 0)
                {
                    comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
                }
            }

            article.Comments = comments;


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
