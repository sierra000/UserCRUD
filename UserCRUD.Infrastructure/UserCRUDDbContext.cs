using Microsoft.EntityFrameworkCore;
using System;

namespace UserCRUD.Infrastructure
{
    public class UserCRUDDbContext : DbContext
    {
        public UserCRUDDbContext(DbContextOptions<UserCRUDDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Core.Model.UserCRUD.UserCRUD> UserCRUD { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Core.Model.UserCRUD.UserCRUD>().HasData(
                new Core.Model.UserCRUD.UserCRUD
                {
                    Id = 1,
                    Name = "Usuario 1",
                    Birthdate = DateTime.Now      
                },
                new Core.Model.UserCRUD.UserCRUD
                {
                    Id = 2,
                    Name = "Usuario 2",
                    Birthdate = DateTime.Now
                },
                new Core.Model.UserCRUD.UserCRUD
                {
                    Id = 3,
                    Name = "Usuario 3",
                    Birthdate = DateTime.Now
                },
                new Core.Model.UserCRUD.UserCRUD
                {
                    Id = 4,
                    Name = "Usuario 4",
                    Birthdate = DateTime.Now
                }
            );
            

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
