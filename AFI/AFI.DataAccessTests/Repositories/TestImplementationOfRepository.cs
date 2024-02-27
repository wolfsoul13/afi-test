using AFI.BusinessLogic.Entities;
using AFI.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AFI.DataAccessTests.Repositories
{
    public  class TestImplementationOfRepository : Repository<Person>
    {
        public TestImplementationOfRepository(DbContext context) : base(context)
        {
        }

        public DbContext ExposedDbContext => context;
        public DbSet<Person> ExposedDbSet => dbSet;
    }
}
