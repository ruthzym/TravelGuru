using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("body")]
        public string Body { get; set; }
        [BsonElement("picURL")]
        public string PictureURL { get; set; }

    }
}
