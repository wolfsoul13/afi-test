
using AFI.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace AFI.DataAccess.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }
}
