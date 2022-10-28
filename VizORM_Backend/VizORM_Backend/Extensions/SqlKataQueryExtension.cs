using Microsoft.Extensions.Localization;
using SqlKata;
using VizORM.DataService.Controllers;
using VizORM.DataService.DTO.DataRequestBodyItems;
using VizORMJoin = VizORM.DataService.DTO.DataRequestBodyItems.Join;

namespace VizORM.DataService.Extensions
{
    public static class SqlKataQueryExtension
    {
        public static Query Join(this Query query, IEnumerable<VizORMJoin> vizORMJoins, IStringLocalizer<DataController> stringLocalizer) 
        {
            foreach (var vizORMJoin in vizORMJoins)
            {
                var joinedEntityName = vizORMJoin.JoinedEntityName;
                var leftColumnName = vizORMJoin.LeftColumnName;
                var rightColumnName = vizORMJoin.RightColumnName;

                switch (vizORMJoin.JoinType)
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
                        var joinTypeNotImpementedMessage = stringLocalizer["JoinTypeNotImpementedMessage"].Value;
                        throw new NotImplementedException(joinTypeNotImpementedMessage);
                }
            }

            return query;
        }

        public static Query Where(this Query query, IEnumerable<Filter> filters, IStringLocalizer<DataController> stringLocalizer)
        {
            foreach (var filter in filters)
            {
                switch (filter.ComparisonType)
                {
                    case ComparisonType.Equal:
                        query.Where(filter.LeftColumn, "=", filter.RightColumn);
                        break;

                    case ComparisonType.NotEqual: 
                        query.WhereNot(filter.LeftColumn, "=", filter.RightColumn);
                        break;

                    case ComparisonType.IsNull:
                        query.WhereNull(filter.LeftColumn);
                        break;

                    case ComparisonType.NotNull:
                        query.WhereNotNull(filter.LeftColumn);
                        break;

                    default:
                        var compirisonTypeNotImpementedMessage = stringLocalizer["JoinTypeNotImpementedMessage"].Value;
                        throw new NotImplementedException(compirisonTypeNotImpementedMessage);
                }
            }

            return query;
        }

        public static Query OrderBy(this Query query, Order order, IStringLocalizer<DataController> stringLocalizer)
        {
            switch (order.OrderMode)
            {
                case OrderMode.Asc:
                    query.OrderBy(order.OrderColumnName);
                    break;

                case OrderMode.Desc:
                    query.OrderByDesc(order.OrderColumnName);
                    break;

                default:
                    var orderModeNotImpementedMessage = stringLocalizer["JoinTypeNotImpementedMessage"].Value;
                    throw new NotImplementedException(orderModeNotImpementedMessage);
            }

            return query;
        }
    }
}
