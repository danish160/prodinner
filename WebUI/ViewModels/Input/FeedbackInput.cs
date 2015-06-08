using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Omu.ProDinner.Resources;
using Omu.ProDinner.WebUI.ViewModels.Attributes;

namespace Omu.ProDinner.WebUI.ViewModels.Input
{
    public class FeedbackInput : Input
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(Mui))]
        [StrLen(500)]
        [AdditionalMetadata("DontSurround", true)]
        [UIHint("TinyMCE")]
        public string Comments { get; set; }
    }
}