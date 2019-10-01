using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.CORE.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.API.Configurations
{
    public static class ConnectionConfiguration
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<StoreContext>(opt =>
                opt.UseSqlServer(conf.GetConnectionString("Store"), b => b.MigrationsAssembly("Store.API"))
               // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
               );

            return services;
        }
    }

}
