using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommentManagement.Infrastructure.Configuration
{
    public class CommentManagementBootstrapper
    {

        public static void Config(IServiceCollection services , string connectionstring)
        {
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();

            services.AddDbContext<CommentContext>(options =>
            {
                options.UseSqlServer(connectionstring);
            });
        }
    }
}
