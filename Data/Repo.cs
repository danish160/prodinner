using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Omu.ProDinner.Core;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.Infra;
using Omu.ValueInjecter;

namespace Omu.ProDinner.Data
{
    public class Repo<T> : IRepo<T> where T : Entity, new()
    {
        protected readonly DbContext dbContext;

        public Repo(IDbContextFactory f)
        {
            dbContext = f.GetContext();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
        
        public T Insert(T o)
        {
            var t = dbContext.Set<T>().Create();
            t.InjectFrom(o);
            dbContext.Set<T>().Add(t);
            return t;
        }
       
        public virtual void Delete(T o)
        {
            if (o is IDel)
                (o as IDel).IsDeleted = true;
            else
                dbContext.Set<T>().Remove(o);
        }

        public T Get(int id)
        {
            var entity = dbContext.Set<T>().Find(id);
            if (entity == null) throw new ProDinnerException("this entity doesn't exist anymore");
            return entity;
        }

        public void Restore(T o)
        {
            if (o is IDel)
                IoC.Resolve<IDelRepo<T>>().Restore(o);
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            if (typeof(IDel).IsAssignableFrom(typeof(T)))
                return IoC.Resolve<IDelRepo<T>>().Where(predicate, showDeleted);
            return dbContext.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            if (typeof(IDel).IsAssignableFrom(typeof(T)))
                return IoC.Resolve<IDelRepo<T>>().GetAll();
            return dbContext.Set<T>();
        }
    }
}