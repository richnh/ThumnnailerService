using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using ThumbnailService.Helpers;
using ThumbnailService.Models;

namespace ThumbnailService.Services
{
    public interface IThumbnailService
    {
        bool GenerateThumbnail(GenerateThumbnailRequest request);
    }

    public class ThumbnailService : IThumbnailService
    {
        public bool GenerateThumbnail(GenerateThumbnailRequest request)
        {
            var masterFilePath    = request.MasterFilePath;
            var thumbNailFilePath = request.ThumbnailFilePath;

            using (FileStream pngStream = new FileStream(request.MasterFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var image = new Bitmap(pngStream))
                {
                    request.MasterFileWidth = image.Width;
                    request.MasterFileHeight = image.Height;

                    double ratio = ImageHelpers.CalculateRatioResizingOfImage(request);

                    int thumbnailCalculatedHeight = Convert.ToInt32(image.Height * ratio);
                    int thumbnailCalculatedWidth = Convert.ToInt32(image.Width * ratio);

                    var thumbNailImage = new Bitmap(thumbnailCalculatedWidth, thumbnailCalculatedHeight);

                    using (var graphics = Graphics.FromImage(thumbNailImage))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.CompositingMode = CompositingMode.SourceCopy;
                        graphics.DrawImage(image, 0, 0, thumbnailCalculatedWidth, thumbnailCalculatedHeight);
                        thumbNailImage.Save(request.ThumbnailFilePath);
                    }
                }
            }

            return true;
        }
    }
}
