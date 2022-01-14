using System.ComponentModel.DataAnnotations.Schema;

namespace DAMLib.Database.MySql.Models
{
    internal class AssetRepositoryModel
    {
        public int Id;
        public string Name;
        public long DateAdded;
    }
}