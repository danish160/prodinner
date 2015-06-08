using System.IO;

namespace Omu.ProDinner.Core.Service
{
    public interface IFileManagerService
    {
        string SaveTempJpeg(string root, Stream inputStream, out int w, out int h);
        void MakeImages(string root, string filename, int x, int y, int w, int h);
        void DeleteImages(string root, string filename);
    }
}