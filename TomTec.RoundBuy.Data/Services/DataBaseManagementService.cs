using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Data
{
    public static class DataBaseManagementService
    {
        public static void MigrationInitialization(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var serviceDB = serviceScope.ServiceProvider.GetService<RoundBuyDbContext>();
                serviceDB.Database.Migrate();
            }
        }
    }
}
