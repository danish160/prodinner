using System.ComponentModel.DataAnnotations;

using Omu.ProDinner.Resources;
using Omu.ProDinner.WebUI.ViewModels.Attributes;

namespace Omu.ProDinner.WebUI.ViewModels.Input
{
    public class MealInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(50)]
        [Display(ResourceType = typeof(Mui), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Mui), Name = "Comments")]
        [StrLen(150)]
        [UIHint("TextArea")]
        public string Comments { get; set; }
    }
}