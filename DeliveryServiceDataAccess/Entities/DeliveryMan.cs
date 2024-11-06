using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;

[Table("DeliveryMan")]
public class DeliveryMan : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public long Phone { get; set; }
    public string PasswordHash { get; set; }
    public string EMail { get; set; }
    public bool IsAvailable { get; set; }
    public int Rating { get; set; }
    
    public ICollection<Order>? Orders { get; set; }
}