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

        public static Query Where(this Query query, IEnumerable<FiltersGroups> filtersGroups)
        {
            if (filtersGroups == null)
            {
                return query;
            }

            foreach (var filtersGroup in filtersGroups)
            {
                SetOrConditionalOperatorIfExists(query, filtersGroup.ConditionalOperator);
                query.Where(q => SetFiltersFromGroup(q, filtersGroup.Filters));
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

        private static void SetOrConditionalOperatorIfExists(Query query, ConditionalOperator conditionalOperator)
        {
            /* Sqlkata allows to add only Or instruction. If do not call Or() it will be And instruction by default.
             * Therefore field ConditionalOperator can by null and it's mean that ConditionalOperator will be And.
             */
            if (conditionalOperator == ConditionalOperator.Or)
            {
                query.Or();
            }

            else if
            (
                (conditionalOperator != ConditionalOperator.Or)
                && (conditionalOperator != ConditionalOperator.And)
            )
            {
                var conditionalOperatorType = conditionalOperator.GetType();
                var conditionalOperatorTypeName = conditionalOperatorType.Name;
                var exceptionMessage = GetEnumExceptionMessage(conditionalOperatorType, (int)conditionalOperator);

                throw new InflowNotImplementedException(conditionalOperatorTypeName, "ConditionalOperatorNotImplemented", exceptionMessage);
            }
        }

        private static string GetEnumExceptionMessage(Type type, int value)
        {
            // Add localizable string.
            return $"Enum {type.FullName} is not implement constant such as {value}";
        }

        private static Query SetFiltersFromGroup(Query query, IEnumerable<Filter> filters)
        {
            if (filters == null)
            {
                return query;
            }

            foreach (var filter in filters)
            {
                SetOrConditionalOperatorIfExists(query, filter.ConditionalOperator);

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
    }
}
