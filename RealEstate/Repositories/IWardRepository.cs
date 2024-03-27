using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public interface IWardRepository
    {
        Task<List<Ward>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);
        Task<Ward?> GetByIdAsync(int id);
        Task<Ward> CreateAsync(Ward ward);
        Task<Ward?> UpdateAsync(int id, Ward ward);
        Task<Ward?> DeleteAsync(int id);
    }
}
