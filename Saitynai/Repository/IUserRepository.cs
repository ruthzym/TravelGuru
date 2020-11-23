using Saitynai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Repository
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task Update(User user);
        Task Delete(string id);
        Task<User> GetUser(string id);
        Task<IEnumerable<User>> GetUsers();
        //hierarchiniai
        Task<IEnumerable<Post>> GetPosts(string userId);
        Task CreatePost(Post post, string userId);
        Task<Post> GetPost(string userId, string postId);
        Task DeletePost(string userId, string postId);
        Task UpdatePost(Post post, string userId, string postId);
        // ------3---hierarchiniia
        Task<IEnumerable<Comment>> GetPostComments(string userId, string postId);
        Task CreatePostComment(Comment post, string userId, string postId);
        Task<Comment> GetPostComment(string userId, string postId, string commentId);
        Task DeletePostComment(string userId, string postId, string commentId);
        Task UpdatePostComment(Comment post, string userId, string postId, string commentId);
    }
}
