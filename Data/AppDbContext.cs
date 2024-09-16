using GameZone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameZone.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>().HasKey(e => new { e.DeviceID, e.GameID });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(CategoryData());
            modelBuilder.Entity<Device>().HasData(DeviceData());
            //modelBuilder.Entity<IdentityRole>().HasData(RoleData());

        }

        //private List<IdentityRole> RoleData()
        //{
        //    return new List<IdentityRole> {
        //    new IdentityRole()
        //    {
        //        Id="1",
        //        Name="Admin"
        //    },
        //    new IdentityRole(){
        //        Id="2",
        //        Name="User"
        //    },

        //    };
        //}

        private List<Device> DeviceData()
        {
            return new List<Device> {
                // this is bootstrap icons
                new Device { ID = 1, Name = "Playstation", Icon = "bi bi-playstation" },
                new Device { ID = 2, Name = "xbox", Icon = "bi bi-xbox" },
                new Device { ID = 3, Name = "Nintendo", Icon = "bi bi-nintendo-switch" },
                new Device { ID = 4, Name = "PC", Icon = "bi bi-pc-display" }
            };
        }

        private List<Category> CategoryData()
        {
            return new List<Category>()
            {
                new Category() { ID = 1, Name = "Sports" },
                new Category() { ID = 2, Name = "Action" },
                new Category() { ID = 3, Name = "Adventure" },
                new Category() { ID = 4, Name = "Racing" },
                new Category() { ID = 5, Name = "Fighting" },
                new Category() { ID = 6, Name = "Film" }
            };
        }
    }
}
