using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyBlog.Core.Common;

namespace MyBlog.Core.Entities;

public class Post : MongoDbEntity
{
    public string Title { get; set; }
    public string Content { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string AuthorId { get; set; }
}
