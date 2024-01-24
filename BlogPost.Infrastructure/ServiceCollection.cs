using BlogPost.ApplicationService.Authentication;
using BlogPost.Infrastructure.Email;
using BlogPost.Infrastructure.ThirdParty;
using BlogPost.SharedKernel;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPost.Infrastructure
{
    public static class ServiceCollection
    {
        public static IServiceCollection InfrastructureServiceCollections(this IServiceCollection services)
        {
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IThirdPartyService, ThirdPartyService>();
            return services;
        }
    }
}
