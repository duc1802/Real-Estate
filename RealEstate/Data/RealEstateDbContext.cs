using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models.Domain;

namespace RealEstate.Data
{
    public class RealEstateDbContext: IdentityDbContext<ApplicationUser>
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<Nation> Nations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Package> Packages { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var realEstateTypes = new List<RealEstateType>()
            {
                new RealEstateType()
                {
                    Id = 1,
                    Name = "Apartment"
                },
                new RealEstateType()
                {
                    Id = 2,
                    Name = "Apartment"
                },
                new RealEstateType()
                {
                    Id = 3,
                    Name = "Agricultural land"
                },
                new RealEstateType()
                {
                    Id = 4,
                    Name = "House"
                },
                new RealEstateType()
                {
                    Id = 5,
                    Name = "Villa"
                },
                new RealEstateType()
                {
                    Id = 6,
                    Name = "Warehouse"
                },
                new RealEstateType()
                {
                    Id = 7,
                    Name = "Farm"
                },
                new RealEstateType()
                {
                    Id= 8,
                    Name = "Other real estate"
                },
            };

            // Seed difficulties to the database
            modelBuilder.Entity<RealEstateType>().HasData(realEstateTypes);

            var postTypes = new List<PostType>()
            {
                new PostType()
                {
                    Id = 1,
                    Name = "Buy"
                },
                new PostType()
                {
                    Id = 2,
                    Name = "Sell"
                },
                new PostType()
                {
                    Id = 3,
                    Name = "Rent"
                },
                
            };

            // Seed difficulties to the database
            modelBuilder.Entity<PostType>().HasData(postTypes);


            var houseDirections = new List<HouseDirection>()
            {
                new HouseDirection()
                {
                    Id = 1,
                    Name = "East"
                },
                new HouseDirection()
                {
                    Id = 2,
                    Name = "West"
                },
                new HouseDirection()
                {
                    Id = 3,
                    Name = "South"
                },
                new HouseDirection()
                {
                    Id = 4,
                    Name = "North"
                },
                new HouseDirection()
                {
                    Id = 5,
                    Name = "South-east"
                },
                new HouseDirection()
                {
                    Id = 6,
                    Name = "North-east"
                },
                new HouseDirection()
                {
                    Id = 7,
                    Name = "South-west"
                },
                new HouseDirection()
                {
                    Id= 8,
                    Name = "North-west"
                },
            };

            // Seed difficulties to the database
            modelBuilder.Entity<HouseDirection>().HasData(houseDirections);

            var statuses = new List<Status>()
            {
                new Status()
                {
                    Id = 1,
                    Name = "Unapproved"
                },
                new Status()
                {
                    Id = 2,
                    Name = "Unverified"
                },
                new Status()
                {
                    Id = 3,
                    Name = "Approved"
                },

            };

            modelBuilder.Entity<Status>().HasData(statuses);

            modelBuilder.Entity<Package>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
        }

    }
}
