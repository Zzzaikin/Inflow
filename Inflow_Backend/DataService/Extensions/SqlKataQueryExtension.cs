using SqlKata;
using Inflow.DataService.DTO.DataRequestBodyItems;
using InflowJoin = Inflow.DataService.DTO.DataRequestBodyItems.Join;
using InflowNotImplementedException = Inflow.Common.Exceptions.NotImplementedException;

namespace Inflow.DataService.Extensions
{
    public static class SqlKataQueryExtension
    {
        public static Query Join(this Query query, IEnumerable<InflowJoin> joins) 
        {
            if (joins == null)
            {
                return query;
            }

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

                        throw new InflowNotImplementedException(joinTypeTypeName, "JoinTypeNotImplemented", exceptionMessage);
                }
            }

            return query;
        }

        public static Query Where(this Query query, IEnumerable<Filter> filters)
        {
            if (filters == null)
            {
                return query;
            }

            foreach (var filter in filters)
            {
                var comparisonType = filter.ComparisonType;

                switch (filter.ComparisonType)
                {
                    case ComparisonType.Equal:
                        query.Where(filter.Column, "=", filter.Value);
                        break;

                    case ComparisonType.NotEqual: 
                        query.WhereNot(filter.Column, "=", filter.Value);
                        break;

                    case ComparisonType.IsNull:
                        query.WhereNull(filter.Column);
                        break;

                    case ComparisonType.NotNull:
                        query.WhereNotNull(filter.Column);
                        break;

                    case ComparisonType.More:
                        query.Where(filter.Column, ">", filter.Value);
                        break;

                    case ComparisonType.Less:
                        query.Where(filter.Column, "<", filter.Value);
                        break;

                    default:
                        var comparisonTypeType = comparisonType.GetType();
                        var comparisonTypeTypeName = comparisonTypeType.Name;
                        var exceptionMessage = GetEnumExceptionMessage(comparisonTypeType, (int)comparisonType);

                        throw new InflowNotImplementedException(comparisonTypeTypeName, "ComparisonTypeNotImpemented", exceptionMessage);
                }
            }

            return query;
        }

        public static Query OrderBy(this Query query, Order order)
        {
            if (order == null) 
            {
                return query; 
            }

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

                    throw new InflowNotImplementedException(orderModeTypeName, "OrderModeNotImplemented", exceptionMessage);
            }

            return query;
        }

        private static string GetEnumExceptionMessage(Type type, int value)
        {
            return $"Enum {type.FullName} is not implement constant such as {value}";
        }
    }
}
