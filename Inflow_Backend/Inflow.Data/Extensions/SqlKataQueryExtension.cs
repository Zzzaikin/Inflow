using SqlKataQuery = SqlKata.Query;
using Inflow.Data.DTO.DataRequestBodyItems;
using InflowJoin = Inflow.Data.DTO.DataRequestBodyItems.Join;

namespace Inflow.Data.Extensions
{
    public static class SqlKataQueryExtension
    {
        public static SqlKataQuery Join(this SqlKataQuery query, IEnumerable<InflowJoin> joins)
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
                        var exceptionMessage = string.Format(Resources.JoinTypeNotImplemented, joinType);
                        throw new NotImplementedException(exceptionMessage);
                }
            }

            return query;
        }

        public static SqlKataQuery Where(this SqlKataQuery query, IEnumerable<FiltersGroups> filtersGroups)
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

        public static SqlKataQuery OrderBy(this SqlKataQuery query, Order order)
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
                    var exceptionMessage = string.Format(Resources.OrderModeNotImplemented, orderMode);
                    throw new NotImplementedException(exceptionMessage);
            }

            return query;
        }

        private static void SetOrConditionalOperatorIfExists(SqlKataQuery query, ConditionalOperator conditionalOperator)
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
                (conditionalOperator != ConditionalOperator.And)
                && (conditionalOperator != ConditionalOperator.Or)
            )
            {
                var exceptionMessage = string.Format(Resources.ConditionalOperatorNotImplemented, conditionalOperator);
                throw new NotImplementedException(exceptionMessage);
            }
        }

        private static SqlKataQuery SetFiltersFromGroup(SqlKataQuery query, IEnumerable<Filter> filters)
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
                        var exceptionMessage = string.Format(Resources.ComparisonTypeNotImpemented, comparisonType);
                        throw new NotImplementedException(exceptionMessage);
                }
            }

            return query;
        }
    }
}
