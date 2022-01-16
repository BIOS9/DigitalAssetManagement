using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAMLib.Abstractions.Models;

namespace DAMLib.Database.MySql.Models
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