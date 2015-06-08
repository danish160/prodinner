using System;
using System.Drawing;
using System.IO;
using Omu.Drawing;
using Omu.ProDinner.Core.Service;

namespace Omu.ProDinner.Service
{
    public class FileManagerService : IFileManagerService
    {
        private const string MealsPath = "/meals/";
        private const string TempPath = "/temp/";

        public void DeleteImages(string root, string filename)
        {
            var dirPath = root + MealsPath;
            File.Delete(dirPath + filename);
            File.Delete(dirPath + "s" + filename);
            File.Delete(dirPath + "m" + filename);
        }

        public void MakeImages(string root, string filename, int x, int y, int w, int h)
        {
            using (var image = Image.FromFile(root + TempPath + filename))
            {
                var dirPath = root + MealsPath;
                var img = Imager.Crop(image, new Rectangle(x, y, w, h));
                var resized = Imager.Resize(img, 200, 150, true);
                var small = Imager.Resize(img, 100, 75, true);
                var mini = Imager.Resize(img, 45, 34, true);
                Imager.SaveJpeg(dirPath + filename, resized);
                Imager.SaveJpeg(dirPath + "s" + filename, small);
                Imager.SaveJpeg(dirPath + "m" + filename, mini);
            }
        }

        public string SaveTempJpeg(string root, Stream inputStream, out int w, out int h)
        {
            var fileName = Guid.NewGuid() + ".jpg";
            var filePath = root + TempPath + fileName;
            using (var image = Image.FromStream(inputStream))
            {
                var resized = Imager.Resize(image, 533, 400, true);
                Imager.SaveJpeg(filePath, resized);

                w = resized.Width;
                h = resized.Height;
                return fileName;
            }
        }
    }
}