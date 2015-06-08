using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class DogController : Controller
    {
        public class Message
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public IEnumerable<Message> Specific = new[]
                                                   {
                                                       new Message{Key = "dinnerindex", Value = "click on <u>host dinner</u> to host a dinner"},
                                                       new Message{Key = "mealindex", Value = "to change the picture click on <u>picture</u> link under the picture"},
                                                       new Message{Key = "dinnerindex", Value = "you can search for dinners from the 'Search for a dinner' box"},
                                                       new Message{Key = "dinnerindex", Value = "to get more results click on the 'more' button on the bottom"},
                                                       new Message{Key = "dinnerindex", Value = "you can delete any dinner by clicking on the 'X' button"},
                                                       new Message{Key = "dinnerindex", Value = "you can select multiple meals by using up/down buttons or with drag and drop"},
                                                       new Message{Key = "mealindex", Value = "you can see more meals if you click on the 'more' button on the bottom"},
                                                       new Message{Key = "mealindex", Value = "you can delete any meal by clicking on the 'X' button"},
                                                   };

        public string[] Generic = new[] { 
            "login and you will be able to restore deleted meals, and other stuff", 
            "sign in with <br/>login: o <br/> password: 1 <br/> and you will be able to do much more",
            "you can change the UI language from the right top corner",
            "you can change the UI Theme from the right top corner",
            "click on this box to show more hints",
            "click on me to <b>hide/show</b> this box",
            "hovering the red validation bulbs will show the messages",
            "click on the message to show more tips; click on me to hide"
        };
            
        public ActionResult Tell(string c, string a)
        {
            var r = new Random();

            if (r.Next(2) == 1 && Specific.Any(o => o.Key == c + a))
            {
                var src = Specific.Where(o => o.Key == c + a);
                return Json(new { o = src.ToArray()[r.Next(src.Count())].Value });
            }

            return Json(new { o = Generic[r.Next(Generic.Count())] });
        }
    }
}