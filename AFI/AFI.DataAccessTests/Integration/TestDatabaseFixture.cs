using Microsoft.EntityFrameworkCore;
using AFI.DataAccess;
using NUnit.Framework;

namespace AFI.DataAccessTests.Integration
{
    [TestFixture]
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Server=HWLAP2412\SQLEXPRESS;Database=AfiIntegrationTest;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                       
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public PolicyHolderContext CreateContext()
            => new PolicyHolderContext(
                new DbContextOptionsBuilder<PolicyHolderContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);
    }
}
