using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Store.AUTH.Interfaces;
using Store.AUTH.Services;
using Store.CORE.EF;
using Store.CORE.Repositories;
using Store.DATA.Entities;
using Store.DATA.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.API.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<ICustomerRepository, CustomerRepository>()
                .AddTransient<IItemRepository, ItemRepository>()
                .AddTransient<IOrderRepository, OrderRepository>()
                .AddTransient<IOrderItemRepository, OrderItemRepository>();

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .Build());
            });

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Store", new Info { Title = "Store", Version = "1.0" });
            });
            
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IJwtGenerator, JwtGenerator>()
                .AddTransient<IAuthService, AuthService>();

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Customer, IdentityRole<Guid>>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireLowercase = false;
            })
                 .AddEntityFrameworkStores<StoreContext>();

            return services;
        }

        /*public static IServiceCollection PolicyConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, RoleHandler>();
            services.AddAuthorization(opts => {

                opts.AddPolicy("RoleLimit",
                    policy => policy.Requirements.Add(new RoleRequirement()));
            });
            return services;
        }*/
    }
}
