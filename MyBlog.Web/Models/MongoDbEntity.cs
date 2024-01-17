using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBlog.Web.Models;

public abstract class MongoDbEntity : IEntity<string>
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    [BsonElement(Order = 1)]
    public string Id { get; } = ObjectId.GenerateNewId().ToString();

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 101)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
