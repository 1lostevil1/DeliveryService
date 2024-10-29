using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;

[Table("users")]
public class DeliveryMan : User
{
    [Column("is_available")]
    public bool IsAvailable { get; set; }

    [Column("rating")]
    public int Rating { get; set; }
}