using Omu.ValueInjecter;

namespace Omu.ProDinner.WebUI.Mappers
{
    public class ProMapper : IProMapper
    {
        public TResult Map<TSource, TResult>(TSource src, object tag = null)
        {
            // using Mapper.Map<TSource, TResult> to specify the types because EF will use a proxy type which we can't have registered
            return Mapper.Map<TSource, TResult>(src, tag);
        }
    }
}