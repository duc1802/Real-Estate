using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models.Domain;
using System.Threading.Tasks;

namespace RealEstate.Repositories
{
    public class SQLNationRepository: INationRepository 
    {
        private readonly RealEstateDbContext dbContext;

        public SQLNationRepository(RealEstateDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Nation> CreateAsync(Nation nation)
        {
            await dbContext.Nations.AddAsync(nation);
            await dbContext.SaveChangesAsync();
            return nation;
        }

        public async Task<Nation?> DeleteAsync(int id)
        {
            var exitingNation = await dbContext.Nations.FirstOrDefaultAsync(n => n.Id == id);
            if (exitingNation == null)
            {
                return null;
            }
            dbContext .Nations.Remove(exitingNation);   
            await dbContext .SaveChangesAsync();
            return exitingNation;
        }

        public async Task<List<Nation>> GetAllAsync()
        {
            return await dbContext.Nations.ToListAsync();
        }

        public async Task<Nation?> GetByIdAsync(int id) 
        {
            return await dbContext.Nations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Nation?> UpdateAsync(int id, Nation nation)
        {
            var exitingNation = await dbContext.Nations.FirstOrDefaultAsync(x => x.Id == id);
            if (exitingNation == null)
            {
                return null;
            }
            exitingNation.Name = nation.Name;

            await dbContext.SaveChangesAsync();
            return exitingNation;
        }

    }
}
