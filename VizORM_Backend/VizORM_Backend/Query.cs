using DataService.DTO.DataRequestBodyItems;
using Microsoft.Extensions.Localization;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SqlClient;
using VizORM_Backend.Config;
using VizORM_Backend.Controllers;
using VizORM_Backend.DTO;
using VizORM_Backend.DTO.DataRequestBodyItems;

namespace VizORM_Backend
{
    public class Query : IQueryExecutable
    {
        private QueryFactory _queryFactory;

        private IStringLocalizer<DataController> _stringLocalizer;

        public Query(SqlConnection sqlConnection, Compiler compiler, IStringLocalizer<DataController> stringLocalizer)
        {
            _queryFactory = new QueryFactory(sqlConnection, compiler);
            _stringLocalizer = stringLocalizer;
        }

        public Task<int> DeleteAsync(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<dynamic>> SelectAsync(DataRequestBody dataRequestBody)
        {
            var columns = dataRequestBody.ColumnNames.ToArray();

            var query = _queryFactory.Query()
                .Select(columns)
                .From(dataRequestBody.EntityName);

            foreach (var join in dataRequestBody.Joins)
            {
                var joinedEntityName = join.JoinedEntityName;
                var leftColumnName = join.LeftColumnName;
                var rightColumnName = join.RightColumnName;

                switch (join.JoinType)
                {
                    case JoinType.Left:
                        query.LeftJoin(joinedEntityName, leftColumnName, rightColumnName);
                        break;

                    case JoinType.Right:
                        query.RightJoin(joinedEntityName, leftColumnName, rightColumnName);
                        break;

                    case JoinType.Inner:
                        query.Join(joinedEntityName, leftColumnName, rightColumnName);
                        break;

                    case JoinType.Cross:
                        query.CrossJoin(joinedEntityName);
                        break;

                    default:
                        var joinTypeNotImpementedMessage = _stringLocalizer["JoinTypeNotImpementedMessage"].Value;
                        throw new NotImplementedException(joinTypeNotImpementedMessage);
                }
            }
        }

        public Task<int> UpdateAsync(DataRequestBody dataRequestBody)
        {
            throw new NotImplementedException();
        }

        private void SetOrder(SqlKata.Query query, Order order)
        {
            var orderColumnName = order.OrderColumnName;

            if (order.OrderMode == OrderMode.Asc)
                query.OrderBy(orderColumnName);

            else
                query.OrderByDesc(orderColumnName);
        }

        private void SetWhere(DataRequestBody dataRequestBody)
        {

        }
    }
}
