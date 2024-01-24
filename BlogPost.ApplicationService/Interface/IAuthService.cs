using BlogPost.SharedKernel.Models;
using BlogPost.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Infrastructure.Model;

namespace BlogPost.ApplicationService.Interface
{
    public interface IAuthService
    {
        Task<ResponseModel> RegisterUser(SignUpRequest request);
        Task<ResponseModel<LoginResponse>> Login(LoginRequest request);
        Task<ResponseModel<ThirdPartyResponse>> ThirdPartyRegister(ThirdPartyRequest request);
    }
}
