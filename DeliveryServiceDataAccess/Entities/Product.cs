using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryServiceDataAccess.Entities;

[Table("products")]
public class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("product")]
    public string ProductName { get; set; }

    [Column("cost")]
    public decimal Cost { get; set; }

    [Column("rate")]
    public int Rating { get; set; }
}