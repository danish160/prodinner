using System.ComponentModel.DataAnnotations;

using Omu.ProDinner.Resources;
using Omu.ProDinner.WebUI.ViewModels.Attributes;

namespace Omu.ProDinner.WebUI.ViewModels.Input
{
    public class UserCreateInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(15)]
        [LoginUnique]
        public string Login { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(20)]
        [UIHint("password")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [UIHint("MultiLookup")]
        public int[] Roles { get; set; }
    }
}