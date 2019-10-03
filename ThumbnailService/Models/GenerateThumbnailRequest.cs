
namespace ThumbnailService.Models
{
    public class GenerateThumbnailRequest : ImageModelBase
    {
        public string MasterFilePath { get; set; }

        public string ThumbnailFilePath { get; set; }
    }
}
