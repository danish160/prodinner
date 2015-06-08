using System.ComponentModel.DataAnnotations;

using Omu.AwesomeMvc;
using Omu.ProDinner.Resources;
using Omu.ProDinner.WebUI.ViewModels.Attributes;

namespace Omu.ProDinner.WebUI.ViewModels.Input
{
    public class ChefInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(15)]
        [Display(ResourceType = typeof(Mui), Name = "First_Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(15)]
        [Display(ResourceType = typeof(Mui), Name = "Last_Name")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [UIHint("Odropdown")]
        [AweUrl(Action = "GetCountries", Controller = "Data")]
        [Display(ResourceType = typeof(Mui), Name = "Country")]
        public int? CountryId { get; set; }
    }
}