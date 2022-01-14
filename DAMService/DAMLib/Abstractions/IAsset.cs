using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAMLib.Abstractions
{
    public interface IAsset
    {
        public int Id { get; }
        public DateTime DateAdded { get; }

        public Task<IReadOnlyCollection<IAssetFile>> GetAllAssetFilesAsync();
        public Task<IAssetFile> GetAssetFileAsync(int id);
    }
}