using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SqlKata.Execution;
using Inflow.DataService.Controllers.Interfaces;
using Inflow.DataService.DTO;
using Inflow.DataService.Extensions;

namespace Inflow.DataService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase, IDataController
    {
        private readonly ILogger<DataController> _logger;

        private readonly Configuration _configuration;

        private readonly IStringLocalizer<DataController> _stringLocalizer;

        private QueryFactory _database;

        public DataController(ILogger<DataController> logger, IOptions<Configuration> configuration, 
            IStringLocalizer<DataController> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _logger = logger;
            _configuration = configuration.Value;

            SetupDbConnection();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(DeleteDataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(InsertDataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        [HttpPost("Select")]
        public async Task<IActionResult> Select([FromBody] SelectDataRequestBody dataRequestBody)
        {
            // TODO: Rename dataRequestBody parameter to selectDataRequestBody. And in another endpoints.
            var result = await _database.Query()
                .Select(dataRequestBody.ColumnNames.ToArray())
                .From(dataRequestBody.EntityName)
                .Join(joins: dataRequestBody.Joins)
                .Where(filtersGroups: dataRequestBody.FiltersGroups)
                .OrderBy(order: dataRequestBody.Order)
                .GetAsync();

            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateDataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        private void SetupDbConnection()
        {
            var dbOptions = _configuration.DbOptions;
            _database = new QueryFactory(dbOptions.SqlConnection, dbOptions.SqlCompiler);
        }
    }
}
