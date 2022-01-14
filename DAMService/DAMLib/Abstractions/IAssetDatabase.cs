using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAMLib.Abstractions
{
    public interface IAssetDatabase
    {
        public Task<IReadOnlyCollection<IAssetRepository>> GetAllAssetRepositoriesAsync();
        public Task<IAssetRepository> GetAssetRepositoryAsync(int id);
    }
}