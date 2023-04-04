namespace Inflow.Data.DTO
{
    public class InsertDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<Dictionary<string, string>> ColumnValuePairs { get; set; }
    }
}
