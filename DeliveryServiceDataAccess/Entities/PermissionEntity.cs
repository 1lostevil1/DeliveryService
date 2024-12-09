using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;


[Table("Permissions")]
public class PermissionEntity : BaseEntity
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    
    public string Type { get; set; }
    
    public List<User> Users { get; set; }
}