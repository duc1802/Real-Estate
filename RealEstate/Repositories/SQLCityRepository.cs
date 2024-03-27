using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public class SQLCityRepository : ICityRespository
    {
        private readonly RealEstateDbContext dbContext;

        public SQLCityRepository(RealEstateDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<City> CreateAsync(City city)
        {
            await dbContext.Cities.AddAsync(city);  
            await dbContext.SaveChangesAsync();
            return city;
        }

        public async Task<City?> DeleteAsync(int id)
        {
            var existingCity = await dbContext.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCity == null)
            {
                return null;
            }

            dbContext.Cities.Remove(existingCity);
            await dbContext.SaveChangesAsync();
            return existingCity;
        }

        public async Task<List<City>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true)
        {
            var cities = dbContext.Cities.Include("Nation").AsQueryable();


            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    cities = cities.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    cities = isAscending ? cities.OrderBy(x => x.Name) : cities.OrderByDescending(x => x.Name);
                }
            }

            return await cities.ToListAsync();
            /*return await dbContext.Cities.Include("Nation").ToListAsync();*/
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            return await dbContext.Cities.Include("Nation").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<City?> UpdateAsync(int id ,City city)
        {
            var exitingCity = await dbContext.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (exitingCity == null) 
            { 
                return null;
            }
            exitingCity.Name = city.Name;
            exitingCity.NationId = city.NationId;
            await dbContext.SaveChangesAsync();
            return exitingCity;
        }

        
    }
}
