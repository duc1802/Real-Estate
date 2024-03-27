using RealEstate.Models.Domain;

namespace RealEstate.Repositories
{
    public interface IPackageRepository
    {
        Task<List<Package>> GetAllAsync();
        Task<Package?> GetByIdAsync(int id);
        Task<Package> CreateAsync(Package package);
        Task<Package?> UpdateAsync(int id, Package package);
        Task<Package?> DeleteAsync(int id);
    }
}
