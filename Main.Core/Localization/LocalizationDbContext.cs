using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Main.Core.Localization.Models;

namespace Main.Core.Localization
{
    public class LocalizationDbContext : DbContext
    {
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //https://github.com/aspnet/Localization/issues/66
            optionsBuilder.UseInMemoryDatabase();
        }
    }
}
