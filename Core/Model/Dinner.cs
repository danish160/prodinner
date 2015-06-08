using System;
using System.Collections.Generic;

namespace Omu.ProDinner.Core.Model
{
    public class Dinner : DelEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int ChefId { get; set; }
        public virtual Chef Chef { get; set; }
        public string Address { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
    }
}