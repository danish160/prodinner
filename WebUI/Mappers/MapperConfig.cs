using System;

using Omu.ProDinner.Core.Model;
using Omu.ProDinner.WebUI.Mappers.Injections;
using Omu.ProDinner.WebUI.ViewModels.Input;
using Omu.ValueInjecter;

namespace Omu.ProDinner.WebUI.Mappers
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.DefaultMap = (src, resType, tag) =>
                {
                    var res = tag != null && tag.GetType().IsSubclassOf(typeof(Entity)) ? tag : Activator.CreateInstance(resType);

                    res.InjectFrom(src);
                    var srcType = src.GetType();    
                    
                    if (srcType.IsSubclassOf(typeof(Entity)) && resType.IsSubclassOf(typeof(Input)))
                    {
                        res.InjectFrom<NormalToNullables>(src)
                           .InjectFrom<EntitiesToInts>(src);
                    }
                    else if (srcType.IsSubclassOf(typeof(Input)) && resType.IsSubclassOf(typeof(Entity)))
                    {
                        res.InjectFrom<IntsToEntities>(src)
                           .InjectFrom<NullablesToNormal>(src);
                    }

                    return res;
                };

            Mapper.AddMap<Dinner, DinnerInput>((dinner, tag) =>
                {
                    var res = (DinnerInput)Mapper.DefaultMap(dinner, typeof(DinnerInput), tag);

                    res.Time = dinner.Start;
                    res.Duration = (int)dinner.End.Subtract(dinner.Start).TotalMinutes;
                    return res;
                });

            Mapper.AddMap<DinnerInput, Dinner>((input, tag) =>
                {
                    var res = (Dinner)Mapper.DefaultMap(input, typeof(Dinner), tag);

                    res.Start = res.Start.Date + input.Time.Value.TimeOfDay;
                    res.End = res.Start.AddMinutes(input.Duration);
                    return res;
                });
        }
    }
}