using Microsoft.EntityFrameworkCore;
using CookingBookApi.Models;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CookingBookApi.Database
{
    public class CBDbContext : DbContext
    {
        public CBDbContext(DbContextOptions<CBDbContext> options) : base(options)    
        {    
    
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess){
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess, 
            CancellationToken cancellationToken = default(CancellationToken)
        ){
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
        }

        private void OnBeforeSaving(){
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries){
                // for entities that inherit from BaseEntity,
                // set UpdatedOn / CreatedOn appropriately
                if (entry.Entity is Base trackable){
                    switch (entry.State){
                        case EntityState.Modified:
                            // set the updated date to "now"
                            trackable.UpdatedDate = utcNow;

                            // mark property as "don't touch"
                            // we don't want to update on a Modify operation
                            entry.Property("CreatedDate").IsModified = false;
                            break;

                        case EntityState.Added:
                            // set both updated and created date to "now"
                            trackable.CreatedDate = utcNow;
                            trackable.UpdatedDate = utcNow;
                            break;
                    }
                }
            }
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set;} 
    }
}