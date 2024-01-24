using BlogPost.Domain.Entity;
using BlogPost.Repository.Context;
using BlogPost.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Repository.Manager
{
    public class PostRepository : ManagerBase<Post>, IPostRepository
    {
        private readonly BlogPostContext _blogPostContext;
        public PostRepository(BlogPostContext blogPostContext) : base(blogPostContext)
        {
            _blogPostContext = blogPostContext;
        }


        public async Task<bool> CreatePost(Post post)
        {
            var status = await AddAsync(post);
            return status;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var status = await UpdateAsync(post);
            return status;
        }

        public async Task<List<Post>> GetPost(string userId)
        {
            var post = GetAllAsync(x => x.UserId == userId).Result.OrderByDescending(x=> x.TimeStamp).ToList();
            return post;
        }

        public async Task<Post> GetPostById(string postId)
        {
            var post = await GetAsync(x => x.Id == postId);
            return post;
        }

        public async Task<bool> DeletePost(Post post)
        {
            var status = await DeleteAsync(post);
            return status;
        }
    }
}
