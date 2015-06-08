using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Service;
using Omu.ProDinner.WebUI.Mappers;
using Omu.ProDinner.WebUI.ViewModels.Input;

namespace Omu.ProDinner.WebUI.Controllers
{
    /// <summary>
    /// generic crud controller for entities where there is no difference between the edit and create view
    /// </summary>
    /// <typeparam name="TEntity">the entity</typeparam>
    /// <typeparam name="TInput"> viewmodel </typeparam>
    public abstract class Cruder<TEntity, TInput> : Crudere<TEntity,TInput,TInput>
        where TInput : Input, new()
        where TEntity : DelEntity, new()
    {
        public Cruder(ICrudService<TEntity> service, IProMapper mapper) : base(service, mapper)
        {
        }
        
        protected override string EditView
        {
            get { return "create"; }
        }
    }
}