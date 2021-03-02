using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.DAL
{
    public static  class ProviderService
    {
        public static void AddDbContextExtension(this IServiceCollection services) 
        {
            services.AddDbContext<ETicaretContext>(options => options.UseSqlServer("Server=.;Database=ETicaretDB;Trusted_Connection=True;"));
        }
        public static ServiceProvider GetProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContextExtension();
            return services.BuildServiceProvider();
        }
        public static ETicaretContext GetContext()
        {
            return (ETicaretContext)GetProvider().GetService(typeof(ETicaretContext));
        }
    }
}
