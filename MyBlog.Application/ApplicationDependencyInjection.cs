using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Application.MappingProfiles;
using MyBlog.Application.Services;
using MyBlog.Application.Services.Impl;

namespace MyBlog.Application;

public static class ApplicationDependencyInjection
{
    public static void RegisterApplicationDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles));

        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IPostService, PostService>();
    }
}
