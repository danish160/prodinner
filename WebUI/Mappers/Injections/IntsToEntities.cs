using System;
using System.Collections.Generic;
using System.Reflection;

using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.Infra;
using Omu.ValueInjecter.Injections;

namespace Omu.ProDinner.WebUI.Mappers.Injections
{
    // go from int[] to ICollection<Entity> 
    public class IntsToEntities : LoopInjection
    {
        protected override bool MatchTypes(Type src, Type trg)
        {
            return src == typeof(int[]) 
                && trg.IsGenericType
                && trg.GetGenericTypeDefinition() == typeof(ICollection<>)
                && trg.GetGenericArguments()[0].IsSubclassOf(typeof(Entity));
        }

        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            var sourceVal = sp.GetValue(source);
            if (sourceVal != null)
            {
                var tval = tp.GetValue(target);// make EF load the collection before modifying it; without it we get errors when saving an existing object

                dynamic repo = IoC.Resolve(typeof(IRepo<>).MakeGenericType(tp.PropertyType.GetGenericArguments()[0]));
                dynamic resList = Activator.CreateInstance(typeof(List<>).MakeGenericType(tp.PropertyType.GetGenericArguments()[0]));

                var sourceAsArr = (int[])sourceVal;
                foreach (var i in sourceAsArr)
                    resList.Add(repo.Get(i));

                tp.SetValue(target, resList);
            }
        }
    }
}