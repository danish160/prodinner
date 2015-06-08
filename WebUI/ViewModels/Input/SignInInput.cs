using System.ComponentModel.DataAnnotations;

using Omu.ProDinner.Resources;

namespace Omu.ProDinner.WebUI.ViewModels.Input
{
    public class SignInInput
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        // [Display(ResourceType = typeof(Mui), Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [UIHint("Password")]
        //   [Display(ResourceType = typeof(Mui), Name = "Password")]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}