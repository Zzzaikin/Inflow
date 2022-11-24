using System.Configuration;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using VizORM.DataService.DTO;

namespace DataServiceTests
{
    public class ExceptionHandlerTests
    {
        private readonly string _host = ConfigurationManager.AppSettings["host"];

        private readonly HttpClient _httpClient = new HttpClient();

        [SetUp]
        public void Setup()
        {
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
    }
}