using Microsoft.EntityFrameworkCore;
using RepositoryComponent.Models;

namespace RepositoryComponent
{
    public class ItemsDbContext : DbContext
    {
        public DbSet<ItemModel> ItemsModel { get; set; }

        public ItemsDbContext(DbContextOptions<ItemsDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemModel>().ToTable("Item");
        } 
    }
}
