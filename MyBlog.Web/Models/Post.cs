using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBlog.Web.Models;

public class Post : MongoDbEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> Tags { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string AuthorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
