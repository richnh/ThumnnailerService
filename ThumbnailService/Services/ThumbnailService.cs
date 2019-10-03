using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using ThumbnailService.Helpers;
using ThumbnailService.Models;

namespace ThumbnailService.Services
{
    interface IThumbnailService
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

            // Figure out the ratio
            double ratioX = (double)thumbnailWidth / (double)masterFileWidth;
            double ratioY = (double)thumbnailHeight / (double)masterFileHeight;

            // use whichever multiplier is smaller
            double ratio = ImageHelpers.CalculateRatioResizingOfImage(request);
          
            // now we can get the new height and width
            int newHeight = Convert.ToInt32(masterFileHeight * ratio);
            int newWidth = Convert.ToInt32(masterFileWidth * ratio);

            // Now calculate the X,Y position of the upper-left corner 
            // (one of these will always be zero)
            int posX = Convert.ToInt32((thumbnailWidth - (masterFileWidth * ratio)) / 2);
            int posY = Convert.ToInt32((thumbnailHeight - (masterFileHeight * ratio)) / 2);

            using (FileStream pngStream = new FileStream(masterFilePath, FileMode.Open, FileAccess.Read))

            using (var image = new Bitmap(pngStream))
            {
                var thumbNailImage = new Bitmap(thumbnailWidth, thumbnailHeight);

                using (var graphics = Graphics.FromImage(thumbNailImage))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode  = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode    = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, posX, posY, newWidth, newHeight);
                    thumbNailImage.Save(thumbNailFilePath, ImageFormat.Png);
                }
            }

            return true;
        }
    }
}
