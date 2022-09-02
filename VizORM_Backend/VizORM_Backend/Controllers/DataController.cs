﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VizORM_Backend.Config;
using System.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using VizORM_Common;
using VizORM_Backend.Controllers.Interfaces;
using VizORM_Backend.Bodies;

namespace VizORM_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase, IDataController
    {
        private readonly ILogger<DataController> _logger;

        private readonly Configuration _configuration;

        private QueryFactory _queryFactory;

        public DataController(ILogger<DataController> logger, IOptions<Configuration> configuration)
        {
            _logger = logger;
            _configuration = configuration.Value;

            SetupDbConnection();
        }

        [HttpPost]
        public Task<IActionResult> Delete(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> Insert(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> Select(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> Update(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        private Compiler GetDbCompiler(string sqlCompilerName)
        {
            Argument.NotNullOrEmpty(sqlCompilerName, nameof(sqlCompilerName));

            return sqlCompilerName switch
            {
                nameof(SqlServerCompiler) => new SqlServerCompiler(),
                nameof(MySqlCompiler) => new MySqlCompiler(),
                nameof(FirebirdCompiler) => new FirebirdCompiler(),
                nameof(OracleCompiler) => new OracleCompiler(),
                nameof(PostgresCompiler) => new PostgresCompiler(),
                nameof(SqliteCompiler) => new SqliteCompiler(),
                _ => throw new NotImplementedException($"Sql compiler: {sqlCompilerName} not supported.")
            };
        }

        private void SetupDbConnection()
        {
            var sqlCompiler = GetDbCompiler(_configuration.SqlCompilerName);
            var connection = new SqlConnection(_configuration?.ConnectionStrings?.DbConnectionString);
            _queryFactory = new QueryFactory(connection, sqlCompiler);
        }
    }
}
