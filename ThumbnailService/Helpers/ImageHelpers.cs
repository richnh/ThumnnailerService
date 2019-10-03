using ThumbnailService.Models;

namespace ThumbnailService.Helpers
{
    public static class ImageHelpers
    {
        public static double CalculateRatioResizingOfImage(ImageModelBase model)
        {
            double ratio = 1.0;

            // Figure out the ratio
            double ratioX = (double)model.ThumbnailWidth / (double)model.MasterFileWidth;
            double ratioY = (double)model.ThumbnailHeight / (double)model.MasterFileHeight;

            // Use whichever multiplier is smaller
            ratio = ratioX < ratioY ? ratioX : ratioY;

            return ratio;
        }
    }
}
