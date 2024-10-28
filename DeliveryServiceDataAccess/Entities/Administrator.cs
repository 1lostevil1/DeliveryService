using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;


[Table("users")]
public class Administrator : User
{
    [Column("admin_level")]
    public string AdminLevel { get; set; }

    [Column("department")]
    public string Department { get; set; }
}