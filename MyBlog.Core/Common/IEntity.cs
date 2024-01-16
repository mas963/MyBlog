namespace MyBlog.Core.Common;

public interface IEntity
{
}

public interface IEntity<out TKey> : IEntity where TKey : IEquatable<TKey>
{
    TKey Id { get; }
    string? CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
}
