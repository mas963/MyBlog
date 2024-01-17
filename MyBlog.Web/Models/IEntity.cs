namespace MyBlog.Web.Models;

public interface IEntity
{
}

public interface IEntity<out TKey> : IEntity where TKey : IEquatable<TKey>
{
    TKey Id { get; }
    DateTime CreatedAt { get; set; }
}
