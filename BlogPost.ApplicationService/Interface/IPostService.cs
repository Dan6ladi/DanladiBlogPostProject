using BlogPost.SharedKernel.Models;
using BlogPost.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Domain.Entity;

namespace BlogPost.ApplicationService.Interface
{
    public interface IPostService
    {
        Task<ResponseModel> CreatePost(PostRequest request, string userId, string author);
        Task<ResponseModel<Post>> ReadPost(string userId, int pageNumber, int MaxItem);
        Task<ResponseModel> UpdatePost(string postId, string content, string title);
        Task<ResponseModel> DeletePost(string postId);
    }
}
