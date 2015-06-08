using System.Collections.Generic;
using System.Data.Entity;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ValueInjecter;

namespace Omu.ProDinner.Data
{
    public class UniRepo : IUniRepo
    {
        private readonly DbContext c;

        public UniRepo(IDbContextFactory a)
        {
            c = a.GetContext();
        }

        public T Insert<T>(T o) where T : Entity, new()
        {
            var t = new T();
            t.InjectFrom(o);
            c.Set<T>().Add(t);
            return t;
        }

        public void Save()
        {
            c.SaveChanges();
        }

        public T Get<T>(int id) where T : Entity
        {
            return c.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return c.Set<T>();
        }
    }
}