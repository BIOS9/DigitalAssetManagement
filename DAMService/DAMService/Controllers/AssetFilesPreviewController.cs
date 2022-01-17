 using System.Collections.Generic;
 using System.IO;
 using System.Threading.Tasks;
using DAMLib.Abstractions;
 using DAMLib.Abstractions.Data;
 using DAMLib.Abstractions.Database;
using DAMLib.Abstractions.Models;
using DAMService.JsonConverters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DAMService.Controllers
{
    [ApiController]
    [Route("/")]
    public class AssetFilePreviewController : ControllerBase
    {
        private readonly IDatabaseFactory _databaseFactory;
        private readonly ILogger<AssetFilePreviewController> _logger;
        private readonly IPreviewImageFactory _previewImageFactory;
        
        public AssetFilePreviewController(
            ILogger<AssetFilePreviewController> logger,
            IDatabaseFactory databaseFactory,
            IPreviewImageFactory previewImageFactory)
        {
            _logger = logger;
            _databaseFactory = databaseFactory;
            _previewImageFactory = previewImageFactory;
        }

        [HttpGet("repositories/{repositoryId}/assets/{assetId}/files/{fileId}/preview")]
        public async Task<IActionResult> GetFilePreview(int repositoryId, int assetId, int fileId, int? maxWidth, int? maxHeight)
        {
            IPreviewImage previewImage = await _previewImageFactory.GetPreviewImageAsync(repositoryId, assetId, fileId, maxWidth, maxHeight);
            return File(await previewImage.GetPngImageStreamAsync(), "image/png");
        }
    }
}