using System.Collections.Generic;

namespace Omu.ProDinner.Core.Model
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}