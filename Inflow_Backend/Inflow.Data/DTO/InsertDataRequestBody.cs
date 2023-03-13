namespace Inflow.Data.DTO
{
    public class InsertDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<string>? ColumnNames { get; set; }
    }
}
