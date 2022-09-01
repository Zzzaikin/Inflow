using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VizORM_Backend.Config;
using System.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections;
using VizORM_Common;

namespace VizORM_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
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

        private void SetupDbConnection()
        {
            var connection = new SqlConnection(_configuration?.ConnectionStrings?.DbConnectionString);
            var compiler = new SqlServerCompiler();

            _queryFactory = new QueryFactory(connection, compiler);
        }

        [HttpGet]
        public IEnumerable Get()
        {
            return _queryFactory.Query("Categories").Get();
        }
    }
}
