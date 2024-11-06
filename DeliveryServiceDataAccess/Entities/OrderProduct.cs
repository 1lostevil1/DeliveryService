using System.ComponentModel.DataAnnotations.Schema;
namespace DeliveryServiceDataAccess.Entities;

[Table("OrderProduct")]
public class OrderProduct : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}