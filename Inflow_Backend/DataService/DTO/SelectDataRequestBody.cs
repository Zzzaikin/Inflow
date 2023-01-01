﻿using Inflow.DataService.DTO.DataRequestBodyItems;

namespace Inflow.DataService.DTO
{
    public class SelectDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<string>? ColumnNames { get; set; }

        public IEnumerable<FiltersGroups>? FiltersGroups { get; set; }

        public IEnumerable<Join>? Joins { get; set; }

        public int From { get; set; }

        public int Count { get; set; }

        public Order? Order { get; set; }
    }
}
