using System.Data.Entity;
using Omu.ProDinner.Core.Model;

namespace Omu.ProDinner.Data
{
    public class Db : DbContext
    {
        public Db()
        {
            Database.SetInitializer<Db>(null);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dinner>().HasMany(r => r.Meals).WithMany(o => o.Dinners).Map(f =>
            {
                f.MapLeftKey("DinnerId");
                f.MapRightKey("MealId");
            });

            modelBuilder.Entity<User>().HasMany(r => r.Roles).WithMany(o => o.Users).Map(f =>
            {
                f.MapLeftKey("UserId");
                f.MapRightKey("RoleId");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}