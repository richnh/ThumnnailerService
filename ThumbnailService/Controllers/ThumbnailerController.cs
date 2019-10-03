using Microsoft.AspNetCore.Mvc;
using ThumbnailService.Models;
using ThumbnailService.Services;

namespace ThumbnailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThumbnailerController : ControllerBase
    {
        IThumbnailService service;

        ThumbnailerController(IThumbnailService service)
        {
            this.service = service;
        }

        [HttpPost]
        public void Post([FromBody] GenerateThumbnailRequest request)
        {
            var success = service.GenerateThumbnail(request);
        }
    }
}
