using SqlKata.Compilers;
using System.Data;

namespace VizORM.Common.Database
{
    public class Options
    {
        public Compiler SqlCompiler { get; set; }

        public IDbConnection SqlConnection { get; set; }
    }
}
