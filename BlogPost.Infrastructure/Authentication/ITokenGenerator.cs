using BlogPost.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.ApplicationService.Authentication
{
    public interface ITokenGenerator
    {
        string GenerateAccessToken(User user);

        string GenerateRefreshToken();

        bool ValidateRefreshoken(string refreshToken);

        ClaimsPrincipal ValidateAccessTokenWithoutLifetime(string accessToken);
    }
}
