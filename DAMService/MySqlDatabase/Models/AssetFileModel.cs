using System;
using DAMLib.Abstractions.Models;

namespace MySqlDatabase.Models
{
    internal class AssetFileModel : IAssetFile
    {
        private int id;
        int IAssetFile.Id => id;

        private long dateAdded;
        DateTime IAssetFile.DateAdded => DateTimeOffset.FromUnixTimeSeconds(dateAdded).DateTime;
    }
}