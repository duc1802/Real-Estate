using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public interface IDistrictRepository
    {
        Task<List<District>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);
        Task<District> CreateAsync(District district);
        Task<District?> GetByIdAsync(int id);
        Task<District?> UpdateAsync(int id, District district);
        Task<District?> DeleteAsync(int id);
    }
}
