using System.Collections.Generic;
using System.Threading.Tasks;
using DAMLib.Abstractions.Models;

namespace DAMLib.Abstractions.Database
{
    public interface IRepositoryDatabase
    {
        public Task<IReadOnlyCollection<IAssetRepository>> GetAllAssetRepositoriesAsync();
        public Task<IAssetRepository> GetAssetRepositoryAsync(int id);
    }
}