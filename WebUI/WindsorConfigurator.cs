using Omu.Encrypto;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Security;
using Omu.ProDinner.Core.Service;
using Omu.ProDinner.Infra;
using Omu.ProDinner.Service;
using Omu.ProDinner.WebUI.Mappers;
using Omu.ProDinner.WebUI.Controllers;
using Omu.ProDinner.WebUI.Dto;

namespace Omu.ProDinner.WebUI
{
    public class WindsorConfigurator
    {
        public static void Configure()
        {
            WindsorRegistrar.Register(typeof(IFormsAuthentication), typeof(FormAuthService));
            WindsorRegistrar.Register(typeof(IHasher), typeof(Hasher));
            WindsorRegistrar.Register(typeof(IMapper<Dinner,DinnerInput>), typeof(DinnerMapper));
            WindsorRegistrar.Register(typeof(IUserService), typeof(UserService));
            WindsorRegistrar.Register(typeof(IMealService), typeof(MealService));

            WindsorRegistrar.RegisterAllFromAssemblies("Omu.ProDinner.Data");
            WindsorRegistrar.RegisterAllFromAssemblies("Omu.ProDinner.Service");
            WindsorRegistrar.RegisterAllFromAssemblies("Omu.ProDinner.WebUI");
        }
    }
}