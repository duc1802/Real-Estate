using RealEstate.Models.Domain;

namespace RealEstate.Repositories

{
    public interface INationRepository
    {
        Task<List<Nation>> GetAllAsync();
        Task<Nation?> GetByIdAsync(int id);
        Task<Nation> CreateAsync(Nation nation);
        Task<Nation?> UpdateAsync(int id, Nation nation);
        Task<Nation?> DeleteAsync(int id);
    }
}
