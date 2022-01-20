using System.Threading.Tasks;
using DAMLib.Abstractions.Metadata;
using Dapper;
using GeneralMetadata.Models;
using MySql;
using MySql.Data.MySqlClient;

namespace GeneralMetadata
{
    public class GeneralMetadataProvider : IMetadataSource
    {
        private const string _name = "general";
        public string Name => _name;
        
        private readonly MySqlConnectionFactory _mySqlConnectionFactory;

        public GeneralMetadataProvider(MySqlConnectionFactory mySqlConnectionFactory)
        {
            _mySqlConnectionFactory = mySqlConnectionFactory;
        }

        public async Task<IMetadataRecord> GetAssetFileMetadataAsync(int repositoryId, int assetId, int fileId)
        {
            using MySqlConnection con = _mySqlConnectionFactory.CreateConnection();
            GeneralMetadataModel generalMetadataModel = await con.QuerySingleOrDefaultAsync<GeneralMetadataModel>(
                "SELECT * FROM `asset_file_metadata_general` WHERE `asset_file_id`=@fileId",
                new { fileId });
            
            if (generalMetadataModel == null)
            {
                return null;
            }

            return new GeneralMetadata(generalMetadataModel.Name, generalMetadataModel.Description);
        }
    }
}