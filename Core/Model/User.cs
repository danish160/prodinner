using System.Collections.Generic;

namespace Omu.ProDinner.Core.Model
{
    public class User : DelEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}