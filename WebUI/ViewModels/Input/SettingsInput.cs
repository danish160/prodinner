using System.Collections.Generic;

using Omu.AwesomeMvc;

namespace Omu.ProDinner.WebUI.ViewModels.Input
{
    public class SettingsInput
    {
        public IEnumerable<KeyContent> Themes { get; set; }

        public string SelectedTheme { get; set; }

        public IEnumerable<KeyContent> Langs { get; set; }

        public string SelectedLang { get; set; }
    }
}