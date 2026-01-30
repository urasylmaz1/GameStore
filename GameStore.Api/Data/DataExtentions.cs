
using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtentions
{
    // Applies any pending migrations for the context to the database.
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext= scope.ServiceProvider
                            .GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}
