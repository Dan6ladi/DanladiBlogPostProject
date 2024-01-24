using BlogPost.Repository.Interface;
using BlogPost.Repository.Manager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Repository
{
    public static class ServiceCollection
    {
        public static IServiceCollection PersistenceCollections(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            return services;
        }
    }
}
