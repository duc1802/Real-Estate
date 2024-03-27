using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public interface ICityRespository
    {
        Task<List<City>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true );
        Task<City> CreateAsync(City city);
        Task<City?> GetByIdAsync(int id);
        Task<City?> UpdateAsync(int id,City city);
        Task<City?> DeleteAsync(int id);
    }
}
