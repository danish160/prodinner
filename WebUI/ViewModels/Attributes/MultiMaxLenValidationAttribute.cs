using System.ComponentModel.DataAnnotations;

namespace Omu.ProDinner.WebUI.ViewModels.Attributes
{
    public class CollectionMaxLenAttribute : ValidationAttribute
    {
        private readonly int maxLen;

        public CollectionMaxLenAttribute(int maxLen)
        {
            this.maxLen = maxLen;
        }
        
        public override string FormatErrorMessage(string name)
        {
            return "max " + maxLen + " items";
        }

        public override bool IsValid(object value)
        {
            var list = value as int[];

            return list == null || (list.Length <= maxLen);
        }
    }
}