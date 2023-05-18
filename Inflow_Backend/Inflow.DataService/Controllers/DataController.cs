using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Inflow.Data.DTO.DataRequest;
using Inflow.Data.Options;
using InflowDataQuery = Inflow.Data.Query;

namespace Inflow.DataService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly InflowDataQuery _query;

        public DataController(IOptions<Configuration> configuration, BaseSqlOptions sqlOptions)
        {
            sqlOptions.DbConnection.ConnectionString = configuration.Value.ConnectionStrings.DbConnectionString;
            _query = new InflowDataQuery(sqlOptions);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(DeleteDataRequestBody deleteDataRequestBody)
        {
            var afectedRowsCount = await _query.DeleteAsync(deleteDataRequestBody);
            return Ok(afectedRowsCount);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(InsertDataRequestBody insertDataRequestBody)
        {
            var insertedRecordIds = await _query.InsertAsync(insertDataRequestBody);
            return Ok(insertedRecordIds);
        }

        [HttpPost("Select")]
        public async Task<IActionResult> Select([FromBody] SelectDataRequestBody selectDataRequestBody)
        {
            var records = await _query.SelectAsync(selectDataRequestBody);
            return Ok(records);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateDataRequestBody updateDataRequestBody)
        {
            var afectedRowsCount = await _query.UpdateAsync(updateDataRequestBody);
            return Ok(afectedRowsCount);
        }
    }
}
