using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace se_training.Data
{
    public class SeContext : DbContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public SeContext(DbContextOptions<SeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Material>().HasMany(m => m.Comments).WithOne(c => c.Material);
            modelBuilder.Entity<Material>().HasMany(m => m.Likes).WithOne(l => l.Material);
            modelBuilder.Entity<Material>().HasMany(m => m.Tags).WithMany(t => t.Materials);
            modelBuilder.Entity<Comment>().HasMany(m => m.Children).WithOne(c => c.Parent);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {

            foreach (var entry in ChangeTracker.Entries<IBaseModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["DeletedAt"] = null;
                        entry.CurrentValues["CreatedAt"] = DateTime.Now;
                        entry.CurrentValues["UpdatedAt"] = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues["UpdatedAt"] = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DeletedAt"] = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}