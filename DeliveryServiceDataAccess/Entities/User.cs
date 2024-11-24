using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;

[Table("User")]
public class User : BaseEntity
{
       public string Name { get; set; }
       public string Surname { get; set; }
       public string Phone { get; set; }
       public string PasswordHash { get; set; }
       public string EMail { get; set; }
       
       public  ICollection<Review>? Reviews { get; set; }
       public ICollection<Order>? Orders { get; set; }
}
