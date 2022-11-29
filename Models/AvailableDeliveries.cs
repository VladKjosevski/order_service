namespace OrderService.Models
{
    public class AvailableDeliveries
    {
        public int ProductId { get; set; }
        public string PostalCode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsGreenDelivery { get; set; } = false;
    }
}
