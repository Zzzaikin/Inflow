﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SqlKata.Execution;
using System.Data.SqlClient;
using VizORM.DataService.Config;
using VizORM.DataService.Controllers.Interfaces;
using VizORM.DataService.DTO;
using VizORM.DataService.Extensions;

namespace VizORM.DataService.Controllers
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
        public async Task<IActionResult> Delete(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        [HttpPost("Select")]
        public async Task<IActionResult> Select([FromBody] DataRequestBody dataRequestBody)
        {
            var result = await _database.Query()
                .Select(dataRequestBody.ColumnNames.ToArray())
                .From(dataRequestBody.EntityName)
                .Join(dataRequestBody.Joins)
                .Where(dataRequestBody.Filters)
                .OrderBy(dataRequestBody.Order)
                .GetAsync();

            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        private void SetupDbConnection()
        {
            var sqlCompiler = _configuration.GetDbCompiler(_configuration.SqlCompilerName);
            var connection = new SqlConnection(_configuration?.ConnectionStrings?.DbConnectionString);
            _database = new QueryFactory(connection, sqlCompiler);
        }
    }
}
