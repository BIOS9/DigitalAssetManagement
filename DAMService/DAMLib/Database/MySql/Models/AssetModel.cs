using System;
using DAMLib.Abstractions.Models;

namespace DAMLib.Database.MySql.Models
{
    internal class AssetModel : IAsset
    {
        private int id;
        int IAsset.Id => id;

        private long dateAdded;
        DateTime IAsset.DateAdded => DateTimeOffset.FromUnixTimeSeconds(dateAdded).DateTime;
    }
}