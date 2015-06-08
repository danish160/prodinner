using System;
using System.Reflection;

using Omu.ValueInjecter.Injections;

namespace Omu.ProDinner.WebUI.Mappers.Injections
{
    public class NullablesToNormal : PropertyInjection
    {
        protected override void Execute(PropertyInfo sp, object source, object target)
        {
            var tp = target.GetType().GetProperty(sp.Name);
            if (tp != null && IsNotIgnored(sp.Name) && Nullable.GetUnderlyingType(sp.PropertyType) == tp.PropertyType)
            {
                var val = sp.GetValue(source);
                if (val != null)
                    tp.SetValue(target, val);
            }
        }
    }
}