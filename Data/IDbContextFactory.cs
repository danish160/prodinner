using System.Data.Entity;

namespace Omu.ProDinner.Data
{
    public interface IDbContextFactory
    {
        DbContext GetContext();
    }
}