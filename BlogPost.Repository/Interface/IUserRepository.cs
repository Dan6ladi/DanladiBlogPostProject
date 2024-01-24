using BlogPost.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Repository.Interface
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(User user);
        Task<User> GetUser(string emailAddress);
        Task<User> GetUserById(string Id);
    }
}
