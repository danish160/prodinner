using Omu.Encrypto;
using Omu.ProDinner.Core.Service;
using Omu.ProDinner.Infra;
using Omu.ProDinner.Service;

namespace Omu.ProDinner.WebUI.App_Start
{
    public class WindsorConfig
    {
        public static void Configure()
        {
            WindsorRegistrar.Register(typeof(IHasher), typeof(Hasher));
            WindsorRegistrar.Register(typeof(IUserService), typeof(UserService));
            WindsorRegistrar.Register(typeof(IMealService), typeof(MealService));

            WindsorRegistrar.RegisterAllFromAssemblies("Omu.ProDinner.Data");
            WindsorRegistrar.RegisterAllFromAssemblies("Omu.ProDinner.Service");
            WindsorRegistrar.RegisterAllFromAssemblies("Omu.ProDinner.WebUI");
        }
    }
}