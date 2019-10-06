using Microsoft.AspNetCore.Mvc;
using ThumbnailService.Models;
using ThumbnailService.Services;

namespace ThumbnailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThumbnailerController : Controller
    {
        IThumbnailService service;

        public ThumbnailerController(IThumbnailService service)
        {
            this.service = service;
        }

        [HttpPost]
        public void Post([FromForm] GenerateThumbnailRequest request)
        {
            var success = service.GenerateThumbnail(request);
        }
    }
}
