using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.Core.Service;

namespace Omu.ProDinner.Service
{
    public class MealService : CrudService<Meal>, IMealService
    {
        private readonly IFileManagerService fileManagerService;

        public MealService(IRepo<Meal> repo, IFileManagerService fileManagerService) : base(repo)
        {
            this.fileManagerService = fileManagerService;
        }

        public void SetPicture(int id, string root, string filename, int x,int y, int w, int h)
        {
            fileManagerService.MakeImages(root, filename, x, y, w, h);
            
            var meal = repo.Get(id);

            if (meal.Picture == filename) return;
            
            var oldPictureFileName = meal.Picture;
            meal.Picture = filename;
            repo.Save();

            if(!string.IsNullOrWhiteSpace(oldPictureFileName)) fileManagerService.DeleteImages(root, oldPictureFileName);
        }
    }
}