using MongoDB.Bson;

namespace MyBlog.Web.Models;

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
