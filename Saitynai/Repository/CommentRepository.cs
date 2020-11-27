using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Saitynai.Models;


namespace Saitynai.Repository
{
    public class CommentRepository : ICommentRepository
    {
        internal MongoDBContext db = new MongoDBContext();

        public async Task Create(Comment comment)
        {
            try
            {
                await db.Comment.InsertOneAsync(comment);
            }
            catch
            {

                throw;
            }
        }

        public async Task<Comment> GetComment(string id)
        {
            try
            {
                var filter = Builders<Comment>.Filter.Eq(s => s.Id, new string(id));
                return await db.Comment.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                return null;
            }
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            try
            {
                return await db.Comment.Find(_ => true).ToListAsync();
            }
            catch
            {

                return null;
            }
        }

        public async Task Update(Comment comment)
        {
            try
            {
                var filter = Builders<Comment>
                    .Filter
                    .Eq(s => s.Id, comment.Id);
                await db.Comment.ReplaceOneAsync(filter, comment);
            }
            catch
            {

                throw;
            }
        }
        public async Task Delete(string id)
        {
            try
            {
                var filter = Builders<Comment>.Filter.Eq(s => s.Id, new string(id));
                await db.Comment.DeleteOneAsync(filter);
            }
            catch
            {

                throw;
            }
        }
    }
}

