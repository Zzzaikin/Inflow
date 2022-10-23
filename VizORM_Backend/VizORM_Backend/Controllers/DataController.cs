using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VizORM_Backend.Config;
using System.Data.SqlClient;
using VizORM_Backend.Controllers.Interfaces;
using VizORM_Backend.DTO;

namespace VizORM_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase, IDataController
    {
        private readonly ILogger<DataController> _logger;

        private readonly Configuration _configuration;

        private Query _query;

        public DataController(ILogger<DataController> logger, IOptions<Configuration> configuration)
        {
            _logger = logger;
            _configuration = configuration.Value;

            SetupDbConnection();
        }

        public async Task<IActionResult> Delete(DataRequestBody dataRequestBody)
        {
            var deletedRecordsCount = await _query.DeleteAsync(dataRequestBody);
            var response = new { deletedRecordsCount };

            return Ok(response);
        }

        public async Task<IActionResult> Insert(DataRequestBody dataRequestBody)
        {
            var insertedRecordsCount = await _query.InsertAsync(dataRequestBody);
            var response = new { insertedRecordsCount };

            return Ok(response);
        }

        [HttpPost("Select")]
        public async Task<IActionResult> Select([FromBody] DataRequestBody dataRequestBody)
        {
            var selectedRecords = await _query.SelectAsync(dataRequestBody);
            return Ok(selectedRecords);
        }

        public async Task<IActionResult> Update(DataRequestBody dataRequestBody)
        {
            var updatedRecordsCount = await _query.UpdateAsync(dataRequestBody);
            var response = new { updatedRecordsCount };

            return Ok(response);
        }

        private void SetupDbConnection()
        {
            var sqlCompiler = _configuration.GetDbCompiler(_configuration.SqlCompilerName);
            var connection = new SqlConnection(_configuration?.ConnectionStrings?.DbConnectionString);
            _query = new Query(connection, sqlCompiler);
        }
    }
}
