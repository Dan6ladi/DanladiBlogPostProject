using BlogPost.ApplicationService.Authentication;
using BlogPost.ApplicationService.Interface;
using BlogPost.ApplicationService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPost.ApplicationService
{
    public static class ServiceCollection
    {
        public static IServiceCollection ApplicationServiceCollections(this IServiceCollection services)
        {
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAuthService, AuthenticationService>();
            return services;
        }
    }
}
