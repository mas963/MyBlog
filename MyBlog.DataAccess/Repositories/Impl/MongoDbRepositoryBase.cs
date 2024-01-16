using System.Linq.Expressions;
using MongoDB.Driver;
using MyBlog.Core.Common;
using MyBlog.DataAccess.Persistence;
using Microsoft.Extensions.Options;

namespace MyBlog.DataAccess.Repositories.Impl;

public abstract class MongoDbRepositoryBase<T> : IRepository<T, string> where T : MongoDbEntity, new()
{
    protected readonly IMongoCollection<T> collection;
    private readonly MongoDbSettings settings;

    protected MongoDbRepositoryBase(IOptions<MongoDbSettings> options)
    {
        this.settings = options.Value;
        var client = new MongoClient(this.settings.ConnectionString);
        var db = client.GetDatabase(this.settings.Database);
        this.collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        var options = new InsertOneOptions { BypassDocumentValidation = false };
        await collection.InsertOneAsync(entity, options);
        return entity;
    }

    public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
        return (await collection.BulkWriteAsync((IEnumerable<WriteModel<T>>)entities, options)).IsAcknowledged;

    }

    public virtual async Task<T> DeleteAsync(T entity)
    {
        return await collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
    }

    public virtual async Task<T> DeleteAsync(string id)
    {
        return await collection.FindOneAndDeleteAsync(x => x.Id == id);
    }

    public virtual async Task<T> DeleteAsync(Expression<Func<T, bool>> filter)
    {
        return await collection.FindOneAndDeleteAsync(filter);
    }

    public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
    {
        return predicate == null ? collection.AsQueryable() : collection.AsQueryable().Where(predicate);
    }

    public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return collection.Find(predicate).FirstOrDefaultAsync();
    }

    public virtual Task<T> GetByIdAsync(string id)
    {
        return collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public virtual async Task<T> UpdateAsync(string id, T entity)
    {
        return await collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
    }

    public virtual async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
    {
        return await collection.FindOneAndReplaceAsync(predicate, entity);
    }
}
