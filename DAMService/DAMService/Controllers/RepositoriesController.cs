using System.Threading.Tasks;
using DAMLib.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DAMService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RepositoriesController : ControllerBase
    {
        private readonly IAssetDatabase _assetAssetDatabase;
        private readonly ILogger<RepositoriesController> _logger;

        public RepositoriesController(ILogger<RepositoriesController> logger, IAssetDatabase assetDatabase)
        {
            _logger = logger;
            _assetAssetDatabase = assetDatabase;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            await _assetAssetDatabase.GetAllAssetRepositoriesAsync();
            return "test";
        }
    }
}