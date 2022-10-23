using VizORM_Backend.DTO.DataRequestBodyItems;

namespace DataService.DTO.DataRequestBodyItems
{
    public class Order
    {
        public OrderMode OrderMode { get; set; } = OrderMode.Asc;

        public string OrderColumnName { get; set; } = "Id";
    }
}
