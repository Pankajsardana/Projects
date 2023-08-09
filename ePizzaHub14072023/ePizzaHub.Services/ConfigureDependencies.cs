using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Respository.Implementation;
using ePizzaHub.Respository.Interface;
using ePizzaHub.Services.Implementations;
using ePizzaHub.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services
{
    public class ConfigureDependencies
    {
        public static void RegisterServices(IConfiguration configuration,IServiceCollection services) 
        {
            //Database
            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString("DBConnection"));
            });
            //Respository
            services.AddScoped<IUserRespository, UserRespository>();
            services.AddScoped<IRespository<Item>,Respository<Item>>();
            services.AddScoped<IRespository<Cart>,Respository<Cart>>();
            services.AddScoped<IRespository<CartItem>, Respository<CartItem>>();
            services.AddScoped<ICartRespository, CartRespository>();

            //Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IService<Item>,Service<Item>>();
            
            
            //services.AddScoped<IService<Cart>, Service<Cart>>();
            //services.AddScoped<ICartRespository, CartRespository>();
        }
    }
}
