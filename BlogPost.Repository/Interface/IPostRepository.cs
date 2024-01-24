using BlogPost.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Repository.Interface
{
    public interface IPostRepository
    {
        Task<bool> CreatePost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<List<Post>> GetPost(string userId);
        Task<bool> DeletePost(Post post);
        Task<Post> GetPostById(string postId);
    }
}
