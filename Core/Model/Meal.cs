using System.Collections.Generic;

namespace Omu.ProDinner.Core.Model
{
    public class Meal : DelEntity
    {
        public string Name { get; set; }
        public string Comments { get; set; }
        public virtual ICollection<Dinner> Dinners { get; set; }
        public string Picture { get; set; }
    }
}