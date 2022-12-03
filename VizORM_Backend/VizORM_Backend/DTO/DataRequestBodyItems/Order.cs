namespace VizORM.DataService.DTO.DataRequestBodyItems
{
    public class Order
    {
        public OrderMode Mode { get; set; }

        public string OrderColumnName { get; set; } = "Id";
    }
}
