using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public interface IStreetRepository
    {
        Task<List<Street>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);
        Task<Street?> GetByIdAsync(int id);
        Task<Street> CreateAsync(Street street);
        Task<Street?> UpdateAsync(int id, Street street);
        Task<Street?> DeleteAsync(int id);
    }
}
