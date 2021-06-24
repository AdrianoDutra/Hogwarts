using Hogwarts.Data.Mapping;
using Hogwarts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<CharacterEntity> Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CharacterEntity>(new CharacterMapping().Configure);

          
        }

    }
}
