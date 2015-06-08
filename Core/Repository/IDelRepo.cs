using System;
using System.Linq;
using System.Linq.Expressions;

namespace Omu.ProDinner.Core.Repository
{
    public interface IDelRepo<T>
    {
        IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false);
        IQueryable<T> GetAll();
        void Restore(T o);
    }
}