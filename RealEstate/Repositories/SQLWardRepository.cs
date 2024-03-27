using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public class SQLWardRepository: IWardRepository
    {
        private readonly RealEstateDbContext dbContext;

        public SQLWardRepository(RealEstateDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task<Ward> CreateAsync(Ward ward)
        {
            await dbContext.Wards.AddAsync(ward);
            await dbContext.SaveChangesAsync();
            return ward;
        }

        public async Task<Ward?> DeleteAsync(int id)
        {
            var existingWard = await dbContext.Wards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWard == null)
            {
                return null;
            }

            dbContext.Wards.Remove(existingWard);
            await dbContext.SaveChangesAsync();
            return existingWard;
        }

        

        public async Task<List<Ward>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true)
        {
            var wards = dbContext.Wards.Include("District").AsQueryable();


            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    wards = wards.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    wards = isAscending ? wards.OrderBy(x => x.Name) : wards.OrderByDescending(x => x.Name);
                }
            }

            return await wards.ToListAsync();
        }

        public async Task<Ward?> GetByIdAsync(int id)
        {
            return await dbContext.Wards.Include("District").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Ward?> UpdateAsync(int id, Ward ward)
        {
            var existingWard = await dbContext.Wards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWard == null)
            {
                return null;
            }
            existingWard.Name = ward.Name;
            existingWard.DistrictId = ward.DistrictId;
            await dbContext.SaveChangesAsync();
            return existingWard;
        }
    }
}
