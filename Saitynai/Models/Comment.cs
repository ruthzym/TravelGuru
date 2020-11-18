using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Models
{
    public class Comment
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("postId")]
        public string PostId { get; set; }


    }
}
