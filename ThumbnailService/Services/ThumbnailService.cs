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
            int thumbnailWidth    = request.ThumbnailWidth;
            int thumbnailHeight   = request.ThumbnailHeight;
            int masterFileWidth   = request.MasterFileWidth;
            int masterFileHeight  = request.MasterFileHeight;
            var masterFilePath    = request.MasterFilePath;
            var thumbNailFilePath = request.ThumbnailFilePath;

            double ratioX = (double)thumbnailWidth / (double)masterFileWidth;
            double ratioY = (double)thumbnailHeight / (double)masterFileHeight;

            double ratio = ImageHelpers.CalculateRatioResizingOfImage(request);
          
            int thumbnailCalculatedHeight = Convert.ToInt32(masterFileHeight * ratio);
            int thumbnailCalculatedWidth = Convert.ToInt32(masterFileWidth * ratio);

            using (FileStream pngStream = new FileStream(masterFilePath, FileMode.Open, FileAccess.Read))

            using (var image = new Bitmap(pngStream))
            {
                var thumbNailImage = new Bitmap(thumbnailCalculatedWidth, thumbnailCalculatedHeight);

                using (var graphics = Graphics.FromImage(thumbNailImage))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, thumbnailCalculatedWidth, thumbnailCalculatedHeight);
                    thumbNailImage.Save(thumbNailFilePath, ImageFormat.Png);
                }
            }

            return true;
        }
    }
}
