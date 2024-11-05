using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;


[Table("Admitistrator")]
public class Administrator : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public long Phone { get; set; }
    public string PasswordHash { get; set; }
    public string EMail { get; set; }
    public string AdminLevel { get; set; }
    public string Department { get; set; }
}