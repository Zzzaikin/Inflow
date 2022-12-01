using SqlKata;
using VizORM.DataService.DTO.DataRequestBodyItems;
using VizORMJoin = VizORM.DataService.DTO.DataRequestBodyItems.Join;
using VizORMNotImplementedException = VizORM.Common.Exceptions.NotImplementedException;

namespace VizORM.DataService.Extensions
{
    public static class SqlKataQueryExtension
    {
        public static Query Join(this Query query, IEnumerable<VizORMJoin> vizORMJoins) 
        {
            foreach (var vizORMJoin in vizORMJoins)
            {
                var joinedEntityName = vizORMJoin.JoinedEntityName;
                var leftColumnName = vizORMJoin.LeftColumnName;
                var rightColumnName = vizORMJoin.RightColumnName;

                switch (vizORMJoin.Type)
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
                        var joinTypeName = vizORMJoin.Type.GetType().ToString();
                        throw new VizORMNotImplementedException(joinTypeName, "JoinTypeNotImplemented");
                }
            }

            return query;
        }

        public static Query Where(this Query query, IEnumerable<Filter> filters)
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
                        var comparisonTypeName = filter.ComparisonType.GetType().ToString();
                        throw new VizORMNotImplementedException(comparisonTypeName, "ComparisonTypeNotImpemented");
                }
            }

            return query;
        }

        public static Query OrderBy(this Query query, Order order)
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
                    var orderModeName = order.OrderMode.GetType().ToString();
                    throw new VizORMNotImplementedException(orderModeName, "OrderModeNotImplemented");
            }

            return query;
        }
    }
}
