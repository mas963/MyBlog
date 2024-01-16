using Microsoft.Extensions.DependencyInjection;
using MyBlog.DataAccess.Persistence;
using Microsoft.Extensions.Configuration;
using MyBlog.DataAccess.Repositories;
using MyBlog.DataAccess.Repositories.Impl;

namespace MyBlog.DataAccess;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
            IConfiguration configuration)
    {
        return services.Configure<MongoDbSettings>(options =>
        {
            options.ConnectionString = configuration
                .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
            options.Database = configuration
                .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
        });
    }

    public static void RegisterDataAccessDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAuthorRepository, AuthorRepository>();
        services.AddSingleton<IPostRepository, PostRepository>();
    }
}