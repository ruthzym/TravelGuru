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
    }
}
