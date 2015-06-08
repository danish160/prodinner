using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Resources;
using Omu.ProDinner.WebUI.ViewModels.Attributes;

namespace Omu.ProDinner.WebUI.ViewModels.Input
{
    public class DinnerInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(50)]
        [Display(ResourceType = typeof(Mui), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [UIHint("Lookup")]
        [Display(ResourceType = typeof(Mui), Name = "Country")]
        [AdditionalMetadata("CustomSearch", true)]
        public int? CountryId { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [UIHint("Odropdown")]
        [AweUrl(Controller = "Data", Action = "GetChefs")]
        [Display(ResourceType = typeof(Mui), Name = "Chef")]
        public int? ChefId { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(20)]
        [Display(ResourceType = typeof(Mui), Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [DataType(DataType.DateTime)]
        [Display(ResourceType = typeof(Mui), Name = "Date")]
        public DateTime? Start { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [CollectionMaxLen(10)]
        [UIHint("MultiLookup")]
        [AdditionalMetadata("DragAndDrop",true)]
        [AdditionalMetadata("PopupClass","mealsLookup")]
        [AdditionalMetadata("ParameterFunc", "getMealsLookupPageSize")]
        [MultiLookup(Fullscreen = true)]
        [Display(ResourceType = typeof(Mui), Name = "Meals")]
        public int[] Meals { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [UIHint("TimePicker")]
        public DateTime? Time { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [Range(3, 9000, ErrorMessageResourceName = "range", ErrorMessageResourceType = typeof(Mui))]
        [Display(ResourceType = typeof(Mui), Name = "Duration")]
        public int Duration { get; set; }
    }
}