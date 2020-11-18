using Saitynai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Repository
{
    interface IPostRepository
    {
        Task Create(Post post);
        Task Update(Post post);
        Task Delete(string id);
        Task<Post> GetPost(string id);
        Task<IEnumerable<Post>> GetPosts();
        Task<IEnumerable<Comment>> GetComments(string id);
        Task CreateComment(Comment post, string id);
        Task<Comment> GetComment(string postId, string id);
        Task DeleteComment(string id, string commentId);
        Task UpdateComment(Comment post, string id, string commentId);
    }
}
