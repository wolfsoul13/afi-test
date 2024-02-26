
using AFI.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace AFI.DataAccess
{
    public class PolicyHolderContext : DbContext
    {
        public DbSet<Person> People => Set<Person>();
        public DbSet<Policy> Policy => Set<Policy>();
        public DbSet<Contact> Contacts => Set<Contact>();


        public PolicyHolderContext(DbContextOptions<PolicyHolderContext> options) : base(options)
        {
        }
    }
}
