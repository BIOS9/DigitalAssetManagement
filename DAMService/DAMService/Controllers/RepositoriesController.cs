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
    [Route("[controller]")]
    public class RepositoriesController : ControllerBase
    {
        private readonly IDatabaseFactory _databaseFactory;
        private readonly ILogger<RepositoriesController> _logger;

        public RepositoriesController(ILogger<RepositoriesController> logger, IDatabaseFactory databaseFactory)
        {
            _logger = logger;
            _databaseFactory = databaseFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<IAssetRepository>> GetAllRepositories()
        {
            return await _databaseFactory.GetRepositoryDatabase().GetAllAssetRepositoriesAsync();
        }
        
        [HttpGet("{repositoryId}")]
        public async Task<ActionResult<IAssetRepository>> GetRepository(int repositoryId)
        {
            IAssetRepository repo = await _databaseFactory.GetRepositoryDatabase().GetAssetRepositoryAsync(repositoryId);
            if (repo == null)
            {
                return NotFound();
            }
            return new ActionResult<IAssetRepository>(repo);
        }
        
        [HttpGet("{repositoryId}/assets")]
        public async Task<IEnumerable<IAsset>> GetAssets(int repositoryId)
        {
            IEnumerable<IAsset> assets = await _databaseFactory.GetAssetDatabase(repositoryId).GetAllAssetsAsync();
            return assets;
        }
        
        [HttpGet("{repositoryId}/assets/{assetId}")]
        public async Task<ActionResult<IAsset>> GetAsset(int repositoryId, int assetId)
        {
            IAsset asset = await _databaseFactory.GetAssetDatabase(repositoryId).GetAssetAsync(assetId);
            if (asset == null)
            {
                return NotFound();
            }
            return new ActionResult<IAsset>(asset);
        }
        
        [HttpGet("{repositoryId}/assets/{assetId}/files")]
        public async Task<IEnumerable<IAssetFile>> GetAssetFiles(int repositoryId, int assetId)
        {
            IEnumerable<IAssetFile> files = await _databaseFactory.GetAssetFileDatabase(repositoryId, assetId).GetAllAssetFilesAsync();
            return files;
        }
        
        [HttpGet("{repositoryId}/assets/{assetId}/files/{fileId}")]
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