using MongoDB.Bson;
using MyBlog.Core.Common;

namespace MyBlog.Core.Entities;

public class Author : MongoDbEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];

    public List<ObjectId> PostIds { get; set; } = new();
}
