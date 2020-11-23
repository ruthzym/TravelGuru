using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Models
{
    public class PostComment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("PostId")]
        public string PostId { get; set; }

        [BsonElement("CommentId")]
        public string CommentId { get; set; }
    }
}
