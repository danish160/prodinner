using System;
using System.Linq;

using Omu.ProDinner.Data;

namespace Omu.ProDinner.WebUI.Utils
{
    public class Worker
    {
        public void Start()
        {
            var t = new System.Timers.Timer();
            t.Elapsed += Execute;
            t.Interval = 120 * 60 * 1000;
            t.Enabled = true;
            t.AutoReset = true;
            t.Start();
        }

        protected void Execute(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                using (var r = new Db())
                {
                    var dinners = r.Dinners.Where(o => o.Meals.Count() > 2 && o.IsDeleted);

                    foreach (var dinner in dinners)
                    {
                        dinner.IsDeleted = false;
                    }

                    var meals = r.Meals.Where(o => o.Picture != null && o.IsDeleted && o.Name.Length > 3);

                    foreach (var meal in meals)
                    {
                        meal.IsDeleted = false;
                    }

                    var chefs = r.Chefs.Where(o => o.IsDeleted && o.LastName.Length > 3);

                    foreach (var chef in chefs)
                    {
                        chef.IsDeleted = false;
                    }

                    var countries = r.Countries.Where(o => o.IsDeleted && o.Name.Length > 3);

                    foreach (var country in countries)
                    {
                        country.IsDeleted = false;
                    }

                    r.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.Log();
            }
        }
    }
}