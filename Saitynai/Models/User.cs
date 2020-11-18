using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("lastname")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phonenumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("birthdate")]
        public DateTime BirthDate { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonIgnore]
        public List<Post> posts { get; set; }

        //public ICollection<Post> Posts { get; set; }

        //public ICollection<Comment> Comments { get; set; }
        //public Post Post { get; set; }

    }
}
