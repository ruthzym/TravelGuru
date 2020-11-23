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
        public string Id { get; set; }       

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("PostId")]
        public string PostId { get; set; }


    }
}
