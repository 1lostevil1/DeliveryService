using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;

[Table("OrderStatus")]
public class OrderStatus : BaseEntity
{
   public String StatusName { get; set; }
   
   public virtual ICollection<Order>? Orders { get; set; }
}