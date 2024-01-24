using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Domain.Entity
{
    public class Post
    {
        public string Id { get; private set; }
        public string Author { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string TimeStamp { get; private set; }
        public string UserId { get; private set; }

        public Post() { }

        public Post(Guid id,string author, string title, string content, string timeStamp, string userId)
        {
            Id = id.ToString();
            Author = author;
            Title = title;
            Content = content;
            TimeStamp = timeStamp;
            UserId = userId;
        }

        public static Post CreatePost(string author, string title, string content, string timestamp, string userId)
        {
            return new(Guid.NewGuid(),author,title,content,timestamp,userId);
        }

        public static Post EditPost(Post post, string title, string content, string timeStamp)
        {
            post.Title = title;
            post.Content = content;
            post.TimeStamp = timeStamp;
            return post;
        }
    }
}
