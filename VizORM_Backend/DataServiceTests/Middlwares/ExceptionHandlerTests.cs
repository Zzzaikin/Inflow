using SqlKata.Execution;
using System.Configuration;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using VizORM.DataService.DTO;

namespace DataServiceTests.Middlwares
{
    public class ExceptionHandlerTests
    {
        private readonly string _host = ConfigurationManager.AppSettings["host"];

        private readonly HttpClient _httpClient = new HttpClient();

        private QueryFactory _database;

        [SetUp]
        public void Setup()
        {
            SetupDBConnection();
            CreateTestTableInDB();
        }

        [Test]
        public async Task InvokeAsync_WhenSqlExceptionOccured_ShouldGetHttpResponseWithErrorMessage()
        {
            await CheckIfDataServiceIsAlife();

            var response = await SendSimpleSelectRequestAsync();
            var responseBody = await response.Content.ReadFromJsonAsync<Error>();

            Assert.That(responseBody, Is.Not.Null);
            Assert.That(responseBody.Message, Is.Not.Empty);
        }

        [TearDown]
        public void CleanUp()
        {
            DropTestTableInDB();
        }

        private Task<HttpResponseMessage> SendSimpleSelectRequestAsync()
        {
            throw new NotImplementedException();
        }

        private async Task CheckIfDataServiceIsAlife()
        {
            var pingSender = new Ping();
            var response = await pingSender.SendPingAsync(_host);

            Assert.That(response.Status, Is.EqualTo(IPStatus.Success));
        }

        private void CreateTestTableInDB()
        {
            throw new NotImplementedException();
        }

        private void DropTestTableInDB()
        {
            throw new NotImplementedException();
        }

        private void SetupDBConnection()
        {

        }
    }
}