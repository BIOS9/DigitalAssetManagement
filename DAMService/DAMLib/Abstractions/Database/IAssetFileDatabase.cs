using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAMLib.Abstractions.Models;

namespace DAMLib.Abstractions.Database
{
    public interface IAssetFileDatabase
    {
        public Task<IReadOnlyCollection<IAssetFile>> GetAllAssetFilesAsync();
        public Task<IAssetFile> GetAssetFileAsync(int id);
    }
}