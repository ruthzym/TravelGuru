using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Models
{
    public class UserPost
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }
        public Post post { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public User user { get; set; }


    }
}
