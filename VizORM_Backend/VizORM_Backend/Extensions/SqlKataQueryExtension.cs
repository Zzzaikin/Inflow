using SqlKata;
using VizORM.DataService.DTO.DataRequestBodyItems;
using VizORMJoin = VizORM.DataService.DTO.DataRequestBodyItems.Join;
using VizORMNotImplementedException = VizORM.Common.Exceptions.NotImplementedException;

namespace VizORM.DataService.Extensions
{
    public static class SqlKataQueryExtension
    {
        public static Query Join(this Query query, IEnumerable<VizORMJoin> joins) 
        {
            foreach (var join in joins)
            {
                var joinedEntityName = join.JoinedEntityName;
                var leftColumnName = join.LeftColumnName;
                var rightColumnName = join.RightColumnName;

                var joinType = join.Type;

                switch (joinType)
                {
                    case JoinType.Left:
                        query.LeftJoin(joinedEntityName, leftColumnName, rightColumnName);
                        break;

                    case JoinType.Inner:
                        query.Join(joinedEntityName, leftColumnName, rightColumnName);
                        break;

                    case JoinType.Right:
                        query.RightJoin(joinedEntityName, leftColumnName, rightColumnName);
                        break;

                    case JoinType.Cross:
                        query.CrossJoin(joinedEntityName);
                        break;

                    default:
                        var joinTypeType = joinType.GetType();
                        var joinTypeTypeName = joinTypeType.Name;
                        var exceptionMessage = GetEnumExceptionMessage(joinTypeType, (int)joinType);

                        throw new VizORMNotImplementedException(joinTypeTypeName, "JoinTypeNotImplemented", exceptionMessage);
                }
            }

            return query;
        }

        public static Query Where(this Query query, IEnumerable<Filter> filters)
        {
            foreach (var filter in filters)
            {
                var comparisonType = filter.ComparisonType;

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
                        var comparisonTypeType = comparisonType.GetType();
                        var comparisonTypeTypeName = comparisonTypeType.Name;
                        var exceptionMessage = GetEnumExceptionMessage(comparisonTypeType, (int)comparisonType);

                        throw new VizORMNotImplementedException(comparisonTypeTypeName, "ComparisonTypeNotImpemented", exceptionMessage);
                }
            }

            return query;
        }

        public static Query OrderBy(this Query query, Order order)
        {
            var orderMode = order.Mode;

            switch (orderMode)
            {
                case OrderMode.Asc:
                    query.OrderBy(order.OrderColumnName);
                    break;

                case OrderMode.Desc:
                    query.OrderByDesc(order.OrderColumnName);
                    break;

                default:
                    var orderModeType = orderMode.GetType();
                    var orderModeTypeName = orderModeType.Name;
                    var exceptionMessage = GetEnumExceptionMessage(orderModeType, (int)orderMode);

                    throw new VizORMNotImplementedException(orderModeTypeName, "OrderModeNotImplemented", exceptionMessage);
            }

            return query;
        }

        private static string GetEnumExceptionMessage(Type type, int value)
        {
            return $"Enum {type.FullName} is not implement constant such as {value}";
        }
    }
}
