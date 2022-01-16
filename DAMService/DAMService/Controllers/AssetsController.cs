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
    [Route("")]
    public class AssetsController : ControllerBase
    {
        private readonly IDatabaseFactory _databaseFactory;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(ILogger<AssetsController> logger, IDatabaseFactory databaseFactory)
        {
            _logger = logger;
            _databaseFactory = databaseFactory;
        }
        
        [HttpGet("repositories/{repositoryId}/assets")]
        public async Task<IEnumerable<IAsset>> GetAssets(int repositoryId)
        {
            IEnumerable<IAsset> assets = await _databaseFactory.GetAssetDatabase(repositoryId).GetAllAssetsAsync();
            return assets;
        }
        
        [HttpGet("repositories/{repositoryId}/assets/{assetId}")]
        public async Task<ActionResult<IAsset>> GetAsset(int repositoryId, int assetId)
        {
            IAsset asset = await _databaseFactory.GetAssetDatabase(repositoryId).GetAssetAsync(assetId);
            if (asset == null)
            {
                return NotFound();
            }
            return new ActionResult<IAsset>(asset);
        }
    }
}