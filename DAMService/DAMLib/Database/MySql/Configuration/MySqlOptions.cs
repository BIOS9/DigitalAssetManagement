using System;

namespace DAMLib.Database.MySql.Configuration
{
    public class MySqlOptions
    {
        public const string MySql = "MySql";
        
        public string ConnectionString { get; set; } = String.Empty;
    }
}