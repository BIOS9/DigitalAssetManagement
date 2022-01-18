using System;
using DAMLib.Abstractions.Models;

namespace AssetDatabase.Models
{
    internal class AssetRepositoryModel : IAssetRepository
    {
        private int id;
        int IAssetRepository.Id => id;
        
        private string name;
        string IAssetRepository.Name => name;
        
        private long dateAdded;
        DateTime IAssetRepository.DateAdded => DateTimeOffset.FromUnixTimeSeconds(dateAdded).DateTime;
    }
}