using BlogPost.Infrastructure.Model;
using BlogPost.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Infrastructure.ThirdParty
{
    public interface IThirdPartyService
    {
        Task<ResponseModel<ThirdPartyResponse>> FaceBook(FaceBookRequest request);
    }
}
