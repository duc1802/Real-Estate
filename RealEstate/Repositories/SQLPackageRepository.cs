using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public class SQLPackageRepository: IPackageRepository
    {
        private readonly RealEstateDbContext dbContext;

        public SQLPackageRepository(RealEstateDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task<Package> CreateAsync(Package package)
        {
            await dbContext.Packages.AddAsync(package);
            await dbContext.SaveChangesAsync();
            return package;
        }

        public async Task<Package?> DeleteAsync(int id)
        {
            var exitstingPackage = await dbContext.Packages.FirstOrDefaultAsync(x => x.Id == id);
            if (exitstingPackage == null)
            {
                return null;
            }

            dbContext.Packages.Remove(exitstingPackage);
            await dbContext.SaveChangesAsync();
            return exitstingPackage;
        }



        public async Task<List<Package>> GetAllAsync()
        {
            return await dbContext.Packages.ToListAsync();
        }

        public async Task<Package?> GetByIdAsync(int id)
        {
            return await dbContext.Packages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Package?> UpdateAsync(int id, Package package)
        {
            var exitstingPackage = await dbContext.Packages.FirstOrDefaultAsync(x => x.Id == id);
            if (exitstingPackage == null)
            {
                return null;
            }
            exitstingPackage.Name = package.Name;
            exitstingPackage.Description = package.Description;
            exitstingPackage.Price = package.Price;
            exitstingPackage.DurationInDays = package.DurationInDays;
            await dbContext.SaveChangesAsync();
            return exitstingPackage;
        }
    }
}
