using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;

[Table("products")]
public class Product : BaseEntity
{
    
    public string ProductName { get; set; }
    public decimal Cost { get; set; }
    public int Rating { get; set; }
    
    public ICollection<OrderProduct>? OrderProducts { get; set; }
}