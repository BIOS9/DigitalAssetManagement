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
    public class RepositoriesController : ControllerBase
    {
        private readonly IDatabaseFactory _databaseFactory;
        private readonly ILogger<RepositoriesController> _logger;

        public RepositoriesController(ILogger<RepositoriesController> logger, IDatabaseFactory databaseFactory)
        {
            _logger = logger;
            _databaseFactory = databaseFactory;
        }

        [HttpGet("repositories")]
        public async Task<IEnumerable<IAssetRepository>> GetAllRepositories()
        {
            return await _databaseFactory.GetRepositoryDatabase().GetAllAssetRepositoriesAsync();
        }
        
        [HttpGet("repositories/{repositoryId}")]
        public async Task<ActionResult<IAssetRepository>> GetRepository(int repositoryId)
        {
            IAssetRepository repo = await _databaseFactory.GetRepositoryDatabase().GetAssetRepositoryAsync(repositoryId);
            if (repo == null)
            {
                return NotFound();
            }
            return new ActionResult<IAssetRepository>(repo);
        }
    }
}