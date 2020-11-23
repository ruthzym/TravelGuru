using MongoDB.Bson;
using MongoDB.Driver;
using Saitynai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Repository
{
    public class UserRepository : IUserRepository
    {
        internal MongoDBContext db = new MongoDBContext();
        private IMongoCollection<User> collection;
        public UserRepository()
        {
            collection = db.mongodb.GetCollection<User>("Users");
        }
        public async Task Create(User user)
        {
            try
            {
                await db.User.InsertOneAsync(user);
            }
            catch
            {

                throw;
            }
        }

        public async Task<User> GetUser(string id)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq(s => s.Id, new string(id));
                return await db.User.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                return await db.User.Find(_ => true).ToListAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task Update(User user)
        {
            try
            {
                var filter = Builders<User>
                    .Filter
                    .Eq(s => s.Id, user.Id);
                await db.User.ReplaceOneAsync(filter, user);
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
                var filter = Builders<User>.Filter.Eq(s => s.Id, new string(id));
                await db.User.DeleteOneAsync(filter);
            }
            catch
            {

                throw;
            }
        }

        public async Task<IEnumerable<Post>> GetPosts(string userId)
        {
            try
            {
                var fil = Builders<UserPost>.Filter.Eq(x => x.UserId, userId);

                var rez = db.UserPost.Find(fil).ToList();
                var builder = Builders<Post>.Filter;
                FilterDefinition<Post>[] filtered = new FilterDefinition<Post>[rez.Count];
                for (int i = 0; i < rez.Count; i++)
                {
                    filtered[i] = builder.Eq(u => u.Id, rez[i].PostId);

                }
                var newFil = builder.Or(filtered);

                return await db.Post.Find(newFil).ToListAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task CreatePost(Post post, string userId)
        {
            try
            {
                var postCom = new UserPost();
                await db.Post.InsertOneAsync(post);

                postCom.UserId = userId;
                postCom.PostId = post.Id;
                await db.UserPost.InsertOneAsync(postCom);
            }
            catch
            {

                throw;
            }
        }

        public async Task<Post> GetPost(string userId, string postId)
        {
            try
            {
                var builder = Builders<Post>.Filter;
                var builder0 = Builders<UserPost>.Filter;
                var filteredPosts = builder0.Eq(x => x.UserId, userId);
                var posts = db.UserPost.Find(filteredPosts).ToList();
                FilterDefinition<Post> filtered = null;
                foreach (var item in posts)
                {
                    if (item.PostId == postId)
                    {
                        filtered = builder.Eq(x => x.Id, postId);
                    }
                }

                return await db.Post.Find(filtered).FirstOrDefaultAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task DeletePost(string userId, string postId)
        {            

            
            try
            {
                var builder = Builders<Post>.Filter;
                var builder0 = Builders<UserPost>.Filter;
                var filteredPosts = builder0.Eq(x => x.UserId, userId);
                var posts = db.UserPost.Find(filteredPosts).ToList();
                FilterDefinition<Post> filtered = null;
                foreach (var item in posts)
                {
                    if (item.PostId == postId)
                    {
                        filtered = builder.Eq(x => x.Id, postId);
                    }
                }
                await db.Post.DeleteOneAsync(filtered);
            }
            catch
            {

                throw;
            }
        }

        public async Task UpdatePost(Post post, string userId, string postId)
        {
            try
            {
                var builder = Builders<Post>.Filter;
                var builder0 = Builders<UserPost>.Filter;
                var filteredPosts = builder0.Eq(x => x.UserId, userId);
                var posts = db.UserPost.Find(filteredPosts).ToList();
                FilterDefinition<Post> filtered = null;
                foreach (var item in posts)
                {
                    if (item.PostId == postId)
                    {
                        filtered = builder.Eq(x => x.Id, postId);
                    }
                }

                await db.Post.ReplaceOneAsync(filtered, post);
            }
            catch 
            {

                throw;
            }
        }

        public async Task<IEnumerable<Comment>> GetPostComments(string userId, string postId)
        {
            try
            {
                var builder = Builders<Post>.Filter;
                var builder0 = Builders<UserPost>.Filter;
                var builderpc = Builders<PostComment>.Filter;
                var bulderc = Builders<Comment>.Filter;
                //filter users
                var filteredPosts = builder0.Eq(x => x.UserId, userId);
                var posts = db.UserPost.Find(filteredPosts).ToList();
                FilterDefinition<Post> filtered = null;
                

                foreach (var item in posts)
                {
                    if (item.PostId == postId)
                    {
                        filtered = builder.Eq(x => x.Id, postId);
                    }
                }
                var post = db.Post.Find(filtered).ToList();

                var filPosts = builderpc.Eq(x => x.PostId, post[0].Id);
                var comms = db.PostComment.Find(filPosts).ToList();
                FilterDefinition<Comment>[] filtered2 = new FilterDefinition<Comment>[comms.Count];
                for (int i = 0; i < comms.Count; i++)
                {
                    if (comms[i].PostId == postId)
                    {
                        filtered2[i] = bulderc.Eq(x=> x.Id, comms[i].CommentId);
                    }
                }

                var final = bulderc.Or(filtered2);

                return await db.Comment.Find(final).ToListAsync();

            }
            catch 
            {

                throw;
            }
        }

        public async Task CreatePostComment(Comment comment, string userId, string postId)
        {
            try
            {
                var userPost = new UserPost();
                await db.Comment.InsertOneAsync(comment);

                userPost.UserId = userId;
                userPost.PostId = postId;
                await db.UserPost.InsertOneAsync(userPost);

                var postCom = new PostComment();               
                postCom.PostId = postId;
                postCom.CommentId = comment.Id;
                await db.PostComment.InsertOneAsync(postCom);

            }
            catch
            {

                throw;
            }
        }

        public async Task<Comment> GetPostComment(string userId, string postId, string commentId)
        {
            try
            {
                var builder = Builders<Post>.Filter;
                var builderCom = Builders<Comment>.Filter;
                var builderPC = Builders<PostComment>.Filter;
                var builderUP = Builders<UserPost>.Filter;

                var filterUser = builderUP.Eq(x => x.UserId, userId);
                var users = db.UserPost.Find(filterUser).ToList();
                FilterDefinition<Post>[] filtered = new FilterDefinition<Post>[users.Count];
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].UserId == userId)
                    {
                        filtered[i] = builder.Eq(x => x.Id, postId);
                    }
                }
                var filter = builder.Or(filtered);

                var posts = db.Post.Find(filter).ToList();
                FilterDefinition<PostComment>[] filtered0 = new FilterDefinition<PostComment>[posts.Count];
                for (int i = 0; i < posts.Count; i++)
                {
                    filtered0[i] = builderPC.Eq(x => x.PostId, posts[i].Id);

                }
                var postFilter = builderPC.Or(filtered0);

                var comms = db.PostComment.Find(postFilter).ToList();

                FilterDefinition<Comment>[] filteredC = new FilterDefinition<Comment>[comms.Count];
                for (int i = 0; i < comms.Count; i++)
                {
                    if (comms[i].PostId == postId)
                    {
                        filteredC[i] = builderCom.Eq(x => x.Id, commentId);
                    }
                }

                var filterFin = builderCom.Or(filteredC);

                return await db.Comment.Find(filterFin).FirstOrDefaultAsync();


            }
            catch 
            {

                throw;
            }
        }

        public async Task DeletePostComment(string userId, string postId, string commentId)
        {
            try
            {
                var builder = Builders<Post>.Filter;
                var builderCom = Builders<Comment>.Filter;
                var builderPC = Builders<PostComment>.Filter;
                var builderUP = Builders<UserPost>.Filter;

                var filterUser = builderUP.Eq(x => x.UserId, userId);
                var users = db.UserPost.Find(filterUser).ToList();
                FilterDefinition<Post>[] filtered = new FilterDefinition<Post>[users.Count];
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].UserId == userId)
                    {
                        filtered[i] = builder.Eq(x => x.Id, postId);
                    }
                }
                var filter = builder.Or(filtered);

                var posts = db.Post.Find(filter).ToList();
                FilterDefinition<PostComment>[] filtered0 = new FilterDefinition<PostComment>[posts.Count];
                for (int i = 0; i < posts.Count; i++)
                {
                    filtered0[i] = builderPC.Eq(x => x.PostId, posts[i].Id);

                }
                var postFilter = builderPC.Or(filtered0);

                var comms = db.PostComment.Find(postFilter).ToList();

                FilterDefinition<Comment>[] filteredC = new FilterDefinition<Comment>[comms.Count];
                for (int i = 0; i < comms.Count; i++)
                {
                    if (comms[i].PostId == postId)
                    {
                        filteredC[i] = builderCom.Eq(x => x.Id, commentId);
                    }
                }

                var filterFin = builderCom.Or(filteredC);

                await db.Comment.DeleteOneAsync(filterFin);
            }
            catch
            {

                throw;
            }
        }

        public async Task UpdatePostComment(Comment post, string userId, string postId, string commentId)
        {
            try
            {
                var builder = Builders<Post>.Filter;
                var builderCom = Builders<Comment>.Filter;
                var builderPC = Builders<PostComment>.Filter;
                var builderUP = Builders<UserPost>.Filter;

                var filterUser = builderUP.Eq(x => x.UserId, userId);
                var users = db.UserPost.Find(filterUser).ToList();
                FilterDefinition<Post>[] filtered = new FilterDefinition<Post>[users.Count];
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].UserId == userId)
                    {
                        filtered[i] = builder.Eq(x => x.Id, postId);
                    }
                }
                var filter = builder.Or(filtered);

                var posts = db.Post.Find(filter).ToList();
                FilterDefinition<PostComment>[] filtered0 = new FilterDefinition<PostComment>[posts.Count];
                for (int i = 0; i < posts.Count; i++)
                {
                    filtered0[i] = builderPC.Eq(x => x.PostId, posts[i].Id);

                }
                var postFilter = builderPC.Or(filtered0);

                var comms = db.PostComment.Find(postFilter).ToList();

                FilterDefinition<Comment>[] filteredC = new FilterDefinition<Comment>[comms.Count];
                for (int i = 0; i < comms.Count; i++)
                {
                    if (comms[i].PostId == postId)
                    {
                        filteredC[i] = builderCom.Eq(x => x.Id, commentId);
                    }
                }

                var filterFin = builderCom.Or(filteredC);

                await db.Comment.ReplaceOneAsync(filterFin, post);
            }
            catch
            {

                throw;
            }
        }
    }
}
