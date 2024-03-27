using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public class SQLDistrictRepository : IDistrictRepository
    {
        private readonly RealEstateDbContext dbContext;

        public SQLDistrictRepository(RealEstateDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<District> CreateAsync(District district)
        {
            await dbContext.Districts.AddAsync(district);
            await dbContext.SaveChangesAsync();
            return district;
        }

        public async Task<District?> DeleteAsync(int id)
        {
            var existingDistrict = await dbContext.Districts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDistrict == null)
            {
                return null;
            }

            dbContext.Districts.Remove(existingDistrict);
            await dbContext.SaveChangesAsync();
            return existingDistrict;
        }

        public async Task<List<District>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true)
        {
            var districts = dbContext.Districts.Include("City").AsQueryable();


            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    districts = districts.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    districts = isAscending ? districts.OrderBy(x => x.Name) : districts.OrderByDescending(x => x.Name);
                }
            }

            return await districts.ToListAsync();
            
        }

        public async Task<District?> GetByIdAsync(int id)
        {
            return await dbContext.Districts.Include("City").FirstOrDefaultAsync(x => x.Id == id);
        }

        

        public async Task<District?> UpdateAsync(int id, District district)
        {
            var existingDistrict = await dbContext.Districts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDistrict == null)
            {
                return null;
            }
            existingDistrict.Name = district.Name;
            existingDistrict.CityId = district.CityId;
            await dbContext.SaveChangesAsync();
            return existingDistrict;
        }
    }
}
