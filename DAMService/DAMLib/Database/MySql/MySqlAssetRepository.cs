using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAMLib.Abstractions;

namespace DAMLib.Database.MySql
{
    public class MySqlAssetRepository : IAssetRepository
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime DateAdded { get; }

        public Task<IReadOnlyCollection<IAsset>> GetAllAssetsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IAsset> GetAssetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}