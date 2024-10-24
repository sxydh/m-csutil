using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class BitmapHelperTest
    {
        [TestMethod]
        public void TestCompressJpeg()
        {
            var source = Directory.GetCurrentDirectory();
            var sourceFileName = Path.Combine(source, $"{typeof(BitmapHelperTest).FullName}.jpeg");
            var destFileName = Path.Combine(source, $"{typeof(BitmapHelperTest).FullName}.{DateTimeHelper.Timestamp()}.jpeg");
            var jpeg = new Bitmap(100, 100);
            jpeg.Save(sourceFileName, ImageFormat.Jpeg);

            var success = BitmapHelper.CompressJpeg(sourceFileName, destFileName, 50);
            Assert.IsTrue(success);

            File.Delete(sourceFileName);
            File.Delete(destFileName);
        }

        [TestMethod]
        public void TestCompressPng()
        {
            var source = Directory.GetCurrentDirectory();
            var sourceFileName = Path.Combine(source, $"{typeof(BitmapHelperTest).FullName}.png");
            var destFileName = Path.Combine(source, $"{typeof(BitmapHelperTest).FullName}.{DateTimeHelper.Timestamp()}.png");
            var jpeg = new Bitmap(100, 100);
            jpeg.Save(sourceFileName, ImageFormat.Png);

            var success = BitmapHelper.CompressPng(sourceFileName, destFileName, 50);
            Assert.IsTrue(success);

            File.Delete(sourceFileName);
            File.Delete(destFileName);
        }
    }
}