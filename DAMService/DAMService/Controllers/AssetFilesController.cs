 using System;
 using System.Collections.Generic;
using System.Threading.Tasks;
using DAMLib.Abstractions;
using DAMLib.Abstractions.Database;
 using DAMLib.Abstractions.Metadata;
 using DAMLib.Abstractions.Models;
using DAMService.JsonConverters;
 using DAMService.Models;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.Extensions.DependencyInjection;
 using Microsoft.Extensions.Logging;

namespace DAMService.Controllers
{
    [ApiController]
    [Route("/")]
    public class AssetFilesController : ControllerBase
    {
        private readonly IDatabaseFactory _databaseFactory;
        private readonly ILogger<AssetFilesController> _logger;
        private readonly IEnumerable<IMetadataSource> _metadataSources;

        public AssetFilesController(ILogger<AssetFilesController> logger, IDatabaseFactory databaseFactory, IServiceProvider services)
        {
            _logger = logger;
            _databaseFactory = databaseFactory;
            _metadataSources = services.GetServices<IMetadataSource>();
        }
        
        [HttpGet("repositories/{repositoryId}/assets/{assetId}/files")]
        public async Task<IEnumerable<IAssetFile>> GetAssetFiles(int repositoryId, int assetId)
        {
            IEnumerable<IAssetFile> files = await _databaseFactory.GetAssetFileDatabase(repositoryId, assetId).GetAllAssetFilesAsync();
            return files;
        }
        
        [HttpGet("repositories/{repositoryId}/assets/{assetId}/files/{fileId}")]
        public async Task<ActionResult<FileWithMetadata>> GetAssetFile(int repositoryId, int assetId, int fileId)
        {
            IAssetFile file = await _databaseFactory.GetAssetFileDatabase(repositoryId, assetId).GetAssetFileAsync(fileId);
            if (file == null)
            {
                return NotFound();
            }

            FileWithMetadata result = new FileWithMetadata();
            result.File = file;
            
            foreach (IMetadataSource source in _metadataSources)
            {
                IMetadataRecord metadataRecord = await source.GetAssetFileMetadataAsync(repositoryId, assetId, fileId);
                if (metadataRecord == null)
                {
                    continue;
                }
                result.Metadata.Add(source.Name, metadataRecord);
            }
            return new ActionResult<FileWithMetadata>(result);
        }
    }
}