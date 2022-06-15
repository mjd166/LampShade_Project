using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
    public  class AccountManagementBootstrapper
    {
        public static void Config(IServiceCollection services,string connectionstring)
        {

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionstring));
        }
    }
}
