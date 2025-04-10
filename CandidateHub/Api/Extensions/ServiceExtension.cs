using CandidateHub.Data.Contexts;
using CandidateHub.Data.IRepositories;
using CandidateHub.Data.Repositories;
using CandidateHub.Service.IServices;
using CandidateHub.Service.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CandidateHub.Api.Extensions;

public static class ServiceExtension
{
    public static void InitAccessor(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        DatabaseUpdate(scope);
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<ICandidateService, CandidateService>();

    }

    public static void DatabaseUpdate(IServiceScope scope)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();
    }
}