using SqlKata.Execution;

namespace DataServiceTests.Database
{
    public class TestTable
    {
        private readonly QueryFactory _database;

        public TestTable()
        {
            _database = new QueryFactory();
        }
    }
}
