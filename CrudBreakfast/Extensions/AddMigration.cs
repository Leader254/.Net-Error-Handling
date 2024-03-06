using CrudBreakfast.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudBreakfast.Extensions
{
    // The purpose of a static class is to act as container for static members, such as constants, static fields and static methods.
    public static class AddMigration
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            return app;
        }
    }
}