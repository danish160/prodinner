using System.Collections.Generic;

using Omu.ProDinner.Core.Model;

namespace Omu.ProDinner.Core.Repository
{
    public interface IUniRepo
    {
        T Insert<T>(T o) where T : Entity, new();
        void Save();
        T Get<T>(int id) where T : Entity;
        IEnumerable<T> GetAll<T>() where T : Entity;
    }
}