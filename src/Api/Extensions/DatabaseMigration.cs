using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class DatabaseMigration
{
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RestauranteDbContext>();
        context.Database.Migrate();
    }
}