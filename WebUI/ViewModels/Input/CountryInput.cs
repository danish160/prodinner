using System.ComponentModel.DataAnnotations;

using Omu.ProDinner.Resources;
using Omu.ProDinner.WebUI.ViewModels.Attributes;

namespace Omu.ProDinner.WebUI.ViewModels.Input
{
    public class CountryInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(20)]
        [Display(ResourceType = typeof(Mui), Name = "Name")]
        public string Name { get; set; }
    }
}