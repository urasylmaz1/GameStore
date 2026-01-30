
using System;
using GameStore.Api.Models;
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

    // Configures the GameStore database context with SQLite and seeds initial data.
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connString = "Data Source=GameStore.db";
        builder.Services.AddSqlite<GameStoreContext>(
        connString,
        optionsAction: options => options.UseSeeding((context, _)=> // Seed initial data
        {
            if (!context.Set<Genre>().Any()) // Seed genres if none exist
            {
                context.Set<Genre>().AddRange( 
                    new Genre {Name = "Fighting"},
                    new Genre {Name = "RPG"},
                    new Genre {Name = "Platformer"},
                    new Genre {Name = "Racing"},
                    new Genre {Name = "Sports"}
                );

                context.SaveChanges(); // Save genres 
            }
        })
);
    }
}
