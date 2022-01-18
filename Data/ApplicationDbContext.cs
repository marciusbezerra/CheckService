using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CheckService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<GoogleResult> GoogleResults { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes().Where(t => !t.GetTableName().ToLower().StartsWith("aspnet")))
            {
                foreach (var property in entity.GetProperties().Where(p => p.ClrType == typeof(string)))
                {
                    Console.WriteLine($"{entity.GetTableName()}.{property.Name} ... size: 255");
                    property.SetColumnType("nvarchar(255)");
                }
            }

            builder.Entity<GoogleResult>().Property(e => e.Error).HasColumnType("LONGTEXT");
        }
    }
}
