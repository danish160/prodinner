using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using Omu.ProDinner.Resources;

namespace Omu.ProDinner.WebUI.Dto
{
    public class Input
    {
        public int Id { get; set; }
    }

    public class FeedbackInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(500)]
        [AdditionalMetadata("DontSurround", true)]
        [UIHint("TinyMCE")]
        public string Comments { get; set; }
    }

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
        public IEnumerable<int> Roles { get; set; }
    }

    public class UserEditInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [UIHint("MultiLookup")]
        public IEnumerable<int> Roles { get; set; }
    }

    public class ChangePasswordInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(20)]
        [UIHint("password")]
        public string Password { get; set; }
    }

    public class CountryInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(20)]
        [Display(ResourceType = typeof(Mui), Name = "Name")]
        public string Name { get; set; }
    }

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
        [UIHint("AjaxDropdown")]
        [Display(ResourceType = typeof(Mui), Name = "Country")]
        public int? CountryId { get; set; }
    }

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
        [UIHint("AjaxDropdown")]
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
        [UIHint("MultiLookup")]
        [AdditionalMetadata("DragAndDrop",true)]
        [AdditionalMetadata("PopupClass","mealsLookup")]
        [AdditionalMetadata("ParameterFunc", "getMealsLookupPageSize")]
        [MultiLookup(Fullscreen = true)]
        [Display(ResourceType = typeof(Mui), Name = "Meals")]
        public IEnumerable<int> Meals { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [Range(0, 23, ErrorMessageResourceName = "range", ErrorMessageResourceType = typeof(Mui))]
        public int Hour { get; set; }

        [Range(0, 59, ErrorMessageResourceName = "range", ErrorMessageResourceType = typeof(Mui))]  
        public int Minute { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [Range(3, 9000, ErrorMessageResourceName = "range", ErrorMessageResourceType = typeof(Mui))]
        [Display(ResourceType = typeof(Mui), Name = "Duration")]
        public int Duration { get; set; }
    }

    public class DelBtn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CropInput
    {
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public int Id { get; set; }
        public string FileName { get; set; }
    }
}