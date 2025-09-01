using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MHR_V02.Data
{
    public class MigrationService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ApplyMigrations()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
