using Omu.ProDinner.Core.Model;

namespace Omu.ProDinner.Core.Service
{
    public interface IMealService : ICrudService<Meal>
    {
        void SetPicture(int id, string root, string filename, int x, int y, int w, int h);
    }
}