using System.ComponentModel.DataAnnotations.Schema;

namespace DAMLib.Database.MySql.Models
{
    public class AssetRepositoryModel
    {
        public int Id;
        public string Name;
        public long DateAdded;
    }
}