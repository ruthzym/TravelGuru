using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saitynai.Models;

namespace Saitynai.Repository
{
    interface ICommentRepository
    {
        Task Create(Comment comment);
        Task Update(Comment comment);
        Task Delete(string id);
        Task<Comment> GetComment(string id);
        Task<IEnumerable<Comment>> GetComments();
    }
}
