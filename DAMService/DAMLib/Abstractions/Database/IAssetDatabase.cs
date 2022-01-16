using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAMLib.Abstractions.Models;

namespace DAMLib.Abstractions.Database
{
    public interface IAssetDatabase
    {
        public Task<IReadOnlyCollection<IAsset>> GetAllAssetsAsync();
        public Task<IAsset> GetAssetAsync(int id);
    }
}