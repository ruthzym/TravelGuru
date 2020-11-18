using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Saitynai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Repository
{
    public class PostRepository : IPostRepository
    {
        internal MongoDBContext db = new MongoDBContext();

        public async Task Create(Post post)
        {
            try
            {
                await db.Post.InsertOneAsync(post);
            }
            catch
            {

                throw;
            }
        }

        public async Task<Post> GetPost(string id)
        {
            try
            {
                FilterDefinition<Post> filter = Builders<Post>.Filter.Eq(s => s.Id, new string(id));
                return await db.Post.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            try
            {
                return await db.Post.Find(_ => true).ToListAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task Update(Post post)
        {
            try
            {
                var filter = Builders<Post>
                    .Filter
                    .Eq(s => s.Id, post.Id);
                await db.Post.ReplaceOneAsync(filter, post);
            }
            catch
            {

                throw;
            }
        }
        public async Task UpdateComment(Comment post, string id, string commentId)
        {
            try
            {
                var filter = Builders<Comment>.Filter.Eq(s => s.PostId, id);
                var filter2 = Builders<Comment>.Filter.Eq(s => s.Id, commentId);
                await db.Comment.ReplaceOneAsync(filter & filter2, post);
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
                var filter = Builders<Post>.Filter.Eq(s => s.Id, new string(id));
                await db.Post.DeleteOneAsync(filter);
            }
            catch
            {

                throw;
            }
        }
        public async Task DeleteComment(string id, string commentId)
        {
            try
            {
                var filter = Builders<Comment>.Filter.Eq(s => s.PostId, id);
                var filter2 = Builders<Comment>.Filter.Eq(s => s.Id, commentId);
                await db.Comment.DeleteOneAsync(filter & filter2);
            }
            catch
            {

                throw;
            }
        }
        public async Task<Comment> GetComment(string postId, string id)
        {
            try
            {

                var filter = Builders<Comment>.Filter.Eq(s => s.PostId, postId);
                var filter1 = Builders<Comment>.Filter.Eq(s => s.Id, id);

                return await db.Comment.Find(filter & filter1).FirstOrDefaultAsync();

            }
            catch
            {

                throw;
            }
        }
        public async Task<IEnumerable<Comment>> GetComments(string id)
    {
        try
        {
            FilterDefinition<Comment> filter = Builders<Comment>.Filter.Eq(s => s.PostId, new string(id));
                return await db.Comment.Find(filter).ToListAsync();
        }
        catch
        {

            throw;
        }
    }

        public async Task CreateComment(Comment post, string id)
        {
            try
            {
                FilterDefinition<Post> filter = Builders<Post>.Filter.Eq(s => s.Id, new string(id));
                var update = Builders<Post>.Update.Push(e => e.comments, post);
                await db.Comment.InsertOneAsync(post);
            }
            catch
            {

                throw;
            }
        }


    }
}
