using MongoDB.Driver;
using Saitynai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Repository
{
    public class MongoDBContext
    {
        public IMongoDatabase mongodb;
        public MongoClient client;
        public MongoDBContext()
        {
            client = new MongoClient("mongodb+srv://saitynai:saitynai@cluster0.k43li.mongodb.net/SaitynaiLab?retryWrites=true&w=majority");
            mongodb = client.GetDatabase("SaitynaiLab");
        }
        public IMongoCollection<User> User
        {
            get
            {
                return mongodb.GetCollection<User>("Users");
            }
        }

        public IMongoCollection<Post> Post
        {
            get
            {
                return mongodb.GetCollection<Post>("Posts");
            }
        }

        public IMongoCollection<Comment> Comment
        {
            get
            {
                return mongodb.GetCollection<Comment>("Comments");
            }
        }

        public IMongoCollection<UserPost> UserPost
        {
            get
            {
                return mongodb.GetCollection<UserPost>("UserPost");
            }
        }

        public IMongoCollection<PostComment> PostComment
        {
            get
            {
                return mongodb.GetCollection<PostComment>("PostComment");
            }
        }

    }
}
