namespace Omu.ProDinner.Core.Model
{
    public class Chef : DelEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}