 using System.Collections.Generic;
using System.Threading.Tasks;
using DAMLib.Abstractions;
using DAMLib.Abstractions.Database;
using DAMLib.Abstractions.Models;
using DAMService.JsonConverters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DAMService.Controllers
{
    [ApiController]
    [Route("/")]
    public class AssetFilesController : ControllerBase
    {
        private readonly IDatabaseFactory _databaseFactory;
        private readonly ILogger<AssetFilesController> _logger;

        public AssetFilesController(ILogger<AssetFilesController> logger, IDatabaseFactory databaseFactory)
        {
            _logger = logger;
            _databaseFactory = databaseFactory;
        }
        
        [HttpGet("repositories/{repositoryId}/assets/{assetId}/files")]
        public async Task<IEnumerable<IAssetFile>> GetAssetFiles(int repositoryId, int assetId)
        {
            IEnumerable<IAssetFile> files = await _databaseFactory.GetAssetFileDatabase(repositoryId, assetId).GetAllAssetFilesAsync();
            return files;
        }
        
        [HttpGet("repositories/{repositoryId}/assets/{assetId}/files/{fileId}")]
        public async Task<ActionResult<IAssetFile>> GetAssetFile(int repositoryId, int assetId, int fileId)
        {
            IAssetFile file = await _databaseFactory.GetAssetFileDatabase(repositoryId, assetId).GetAssetFileAsync(fileId);
            if (file == null)
            {
                return NotFound();
            }
            return new ActionResult<IAssetFile>(file);
        }
    }
}