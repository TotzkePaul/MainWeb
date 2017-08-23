using Main.Data.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Main.Data.Persistence.Entities
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> options) : base(options)
        { }

        public DbSet<FileDescription> FileDescriptions { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FileDescription>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}
