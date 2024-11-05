using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;

[Table("Review")]
public class Review : BaseEntity
{
    public string Message { get; set; }
    public bool IsAnswered { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}