using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using DAMLib.Abstractions;
using DAMLib.Database.MySql.Models;

namespace DAMLib.Database.MySql
{
    public class MySqlAssetRepository : IAssetRepository
    {
        public int Id => _model.Id;
        public string Name => _model.Name;
        public DateTime DateAdded => DateTimeOffset.FromUnixTimeSeconds(_model.DateAdded).DateTime;
        
        private readonly AssetRepositoryModel _model;

        internal MySqlAssetRepository(AssetRepositoryModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

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