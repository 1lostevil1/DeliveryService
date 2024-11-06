using System.ComponentModel.DataAnnotations.Schema;
namespace DeliveryServiceDataAccess.Entities;

[Table("Order")]
public class Order : BaseEntity
{

    public String Address { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int DeliveryId { get; set; }
    public DeliveryMan DeliveryMan { get; set; }
    public int StatusId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    
    public  ICollection<OrderProduct>? OrderProducts { get; set; }
}