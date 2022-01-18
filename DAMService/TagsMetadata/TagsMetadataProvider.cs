using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAMLib.Abstractions.Database;
using DAMLib.Abstractions.Metadata;
using Dapper;
using MySql;
using MySql.Data.MySqlClient;
using TagsMetadata.Models;

namespace TagsMetadata
{
    public class TagsMetadataProvider : IMetadataSource
    {
        private const string _name = "tags";
        public string Name => _name;
        
        private readonly MySqlConnectionFactory _mySqlConnectionFactory;

        public TagsMetadataProvider(MySqlConnectionFactory mySqlConnectionFactory)
        {
            _mySqlConnectionFactory = mySqlConnectionFactory;
        }

        public async Task<IMetadataRecord> GetAssetFileMetadataAsync(int repositoryId, int assetId, int fileId)
        {
            using MySqlConnection con = _mySqlConnectionFactory.CreateConnection();
            IEnumerable<TagModel> tags = await con.QueryAsync<TagModel>(
                "SELECT `metadata_tags`.name,`metadata_tags`.`asset_repository_id` FROM `asset_file_metadata_tags` INNER JOIN `metadata_tags` ON `metadata_tags`.`id` = `asset_file_metadata_tags`.`tag_id` WHERE `asset_file_id`=@fileId",
                new { fileId });
            
            if (!tags.Any())
            {
                return null;
            }
            
            if (!tags.All(tag => tag.AssetRepositoryId == repositoryId))
            {
                throw new InvalidDataException("Asset file is tagged with tag from another repository.");
            }
            
            return new TagCollection(tags.Select(x => new Tag(x.Name)));
        }
    }
}