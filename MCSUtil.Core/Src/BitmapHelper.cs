using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace MCSUtil.Core
{
    public static class BitmapHelper
    {
        public static bool CompressJpeg(string sourceFileName, string destFileName, int quality)
        {
            return Compress(sourceFileName, destFileName, quality, ImageFormat.Jpeg.Guid);
        }

        public static bool CompressPng(string sourceFileName, string destFileName, int quality)
        {
            return Compress(sourceFileName, destFileName, quality, ImageFormat.Png.Guid);
        }

        private static bool Compress(string sourceFileName, string destFileName, int quality, Guid formatId)
        {
            using (var bitmap = new Bitmap(sourceFileName))
            {
                var jpgCodecInfo = ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => e.FormatID == formatId);
                if (jpgCodecInfo == null)
                {
                    return false;
                }

                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                bitmap.Save(destFileName, jpgCodecInfo, encoderParams);
                return true;
            }
        }
    }
}