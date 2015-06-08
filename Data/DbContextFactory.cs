using System.Data.Entity;

namespace Omu.ProDinner.Data
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly DbContext dbContext;
        public DbContextFactory()
        {
            dbContext = new Db();
        }

        public DbContext GetContext()
        {
            return dbContext;
        }
    }
}