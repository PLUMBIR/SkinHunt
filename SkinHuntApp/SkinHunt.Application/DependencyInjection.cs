using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkinHunt.Application.Common.Interfaces;
using SkinHunt.Application.Services;
using System.Reflection;
using SkinHunt.Application.Common.Entities;

namespace SkinHunt.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add Database connection
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DbContext>(options => options.UseSqlServer(connection));

            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;  
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequireUppercase = false;  
                options.Password.RequireLowercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DbContext>()
                .AddDefaultTokenProviders();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IDbInitializer, DbInitializer>();

            return services;
        }
    }
}
