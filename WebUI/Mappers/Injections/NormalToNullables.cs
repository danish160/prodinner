using System;
using System.Reflection;

using Omu.ValueInjecter.Injections;

namespace Omu.ProDinner.WebUI.Mappers.Injections
{
    public class NormalToNullables : PropertyInjection
    {
        protected override void Execute(PropertyInfo sp, object source, object target)
        {
            var tp = target.GetType().GetProperty(sp.Name);
            if (tp != null && IsNotIgnored(sp.Name) && sp.PropertyType == Nullable.GetUnderlyingType(tp.PropertyType))
            {
                var val = sp.GetValue(source);

                //ignore int = 0 and DateTime = to 1/01/0001
                if (sp.PropertyType == typeof(int) && (int)val == default(int) ||
                    sp.PropertyType == typeof(DateTime) && (DateTime)val == default(DateTime))
                {
                    return;
                }

                tp.SetValue(target, val);
            }
        }
    }
}