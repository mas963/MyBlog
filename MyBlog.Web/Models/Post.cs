using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBlog.Web.Models;

public class Post
{
    [BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> Tags { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId AuthorId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 101)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
