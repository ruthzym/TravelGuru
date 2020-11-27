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

                return null;
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

                return null;
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
                var builder = Builders<Comment>.Filter;
                var builder0 = Builders<PostComment>.Filter;
                var filteredPosts = builder0.Eq(x => x.PostId, id);
                var posts = db.PostComment.Find(filteredPosts).ToList();
                FilterDefinition<Comment> filtered = null;
                foreach (var item in posts)
                {
                    if (item.CommentId == commentId)
                    {
                        filtered = builder.Eq(x => x.Id, commentId);
                    }
                }

                await db.Comment.ReplaceOneAsync(filtered, post);
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
                var builder = Builders<Comment>.Filter;
                var builder0 = Builders<PostComment>.Filter;
                var filteredPosts = builder0.Eq(x => x.PostId, id);
                var posts = db.PostComment.Find(filteredPosts).ToList();
                FilterDefinition<Comment> filtered = null;
                foreach (var item in posts)
                {
                    if (item.CommentId == commentId)
                    {
                        filtered = builder.Eq(x => x.Id, commentId);
                    }                    
                }

                await db.Comment.DeleteOneAsync(filtered);
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
                var builder = Builders<Comment>.Filter;
                var builder0 = Builders<PostComment>.Filter;
                var filteredPosts = builder0.Eq(x => x.PostId, postId);
                var posts = db.PostComment.Find(filteredPosts).ToList();
                FilterDefinition<Comment> filtered = null;
                foreach (var item in posts)
                {
                    if (item.CommentId == id)
                    {
                        filtered = builder.Eq(x => x.Id, id);
                    }
                }

                return await db.Comment.Find(filtered).FirstOrDefaultAsync();

            }
            catch
            {

                return null;
            }
        }
        public async Task<IEnumerable<Comment>> GetComments(string id)
    {
        try
        {
                var fil = Builders<PostComment>.Filter.Eq(x => x.PostId, id);

                var rez = db.PostComment.Find(fil).ToList();
                string[] ids = new string[rez.Count];
                var builder = Builders<Comment>.Filter;
                FilterDefinition<Comment>[] filtered = new FilterDefinition<Comment>[rez.Count];
                for (int i = 0; i < rez.Count; i++)
                {
                    ids[i] = rez[i].CommentId;
                    filtered[i] = builder.Eq(u => u.Id, ids[i]);

                }
                var newFil = builder.Or(filtered);

                return await  db.Comment.Find(newFil).ToListAsync();

            }
        catch
        {

                return null;
        }
    }

        public async Task CreateComment(Comment post, string id)
        {
            try
            {
                FilterDefinition<Post> filter = Builders<Post>.Filter.Eq(s => s.Id, id);
                var postCom = new PostComment();                
                await db.Comment.InsertOneAsync(post);

                postCom.PostId = id;
                postCom.CommentId = post.Id;
                await db.PostComment.InsertOneAsync(postCom);                
            }
            catch
            {

                throw;
            }
        }


    }
}
