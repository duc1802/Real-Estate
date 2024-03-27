using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public class SQLStreetRepository: IStreetRepository
    {
        private readonly RealEstateDbContext dbContext;

        public SQLStreetRepository(RealEstateDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task<Street> CreateAsync(Street street)
        {
            await dbContext.Streets.AddAsync(street);
            await dbContext.SaveChangesAsync();
            return  street;
        }

        public async Task<Street?> DeleteAsync(int id)
        {
            var existingStreet = await dbContext.Streets.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStreet == null)
            {
                return null;
            }

            dbContext.Streets.Remove(existingStreet);
            await dbContext.SaveChangesAsync();
            return existingStreet;
        }



        public async Task<List<Street>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true)
        {
            var streets = dbContext.Streets.Include("Ward").AsQueryable();


            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    streets = streets.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    streets = isAscending ? streets.OrderBy(x => x.Name) : streets.OrderByDescending(x => x.Name);
                }
            }

            return await streets.ToListAsync();
        }

        public async Task<Street?> GetByIdAsync(int id)
        {
            return await dbContext.Streets.Include("Ward").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Street?> UpdateAsync(int id, Street street)
        {
            var existingStreet = await dbContext.Streets.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStreet == null)
            {
                return null;
            }
            existingStreet.Name = street.Name;
            existingStreet.WardId = street.WardId;
            await dbContext.SaveChangesAsync();
            return existingStreet;
        }
    }

}
