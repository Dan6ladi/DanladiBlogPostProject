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
    public class UserRepository : ManagerBase<User>, IUserRepository
    {
        private readonly BlogPostContext _blogPostContext;
        public UserRepository(BlogPostContext blogPostContext) : base(blogPostContext)
        {
            _blogPostContext = blogPostContext;
        }

        public async Task<bool> CreateUser(User user)
        {
            var status = await AddAsync(user);
            return status;
        }

        public async Task<User> GetUser(string emailAddress)
        {
            var user = await GetAsync(x => x.EmailAddress == emailAddress);
            return user;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await GetAsync(x => x.Id == id);
            return user;
        }
    }
}
