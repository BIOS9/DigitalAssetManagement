using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAMLib.Abstractions
{
    public interface IAssetRepository
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime DateAdded { get; }

        public Task<IReadOnlyCollection<IAsset>> GetAllAssetsAsync();
        public Task<IAsset> GetAssetAsync(int id);
    }
}