namespace Inflow.Data.DTO
{
    public class InsertDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<Dictionary<string, string>> InsertingData { get; set; }
    }
}
