using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccess.Entities;

public class Activity : EntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CarId { get; set; }
    public bool IsActive { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public Car Car { get; set; }
}