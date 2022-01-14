using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAMLib.Abstractions
{
    public interface IDatabase
    {
        public Task<IReadOnlyCollection<IAssetRepository>> GetAllAssetRepositoriesAsync();
        public Task<IAssetRepository> GetAssetRepositoryAsync(int id);
    }
}