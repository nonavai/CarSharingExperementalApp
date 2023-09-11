using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace DataAccess.Entities;

public class Car : EntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int LenderId { get; set; }
    
    
    public int Year { get; set; }
    public string RegistrationNumber { get; set; }
    public string Mark { get; set; }
    public string Model { get; set; }
    public float Price { get; set; }
    [Column(TypeName = "nvarchar(20)")]
    public FuelType FuelType { get; set; }
    [Column(TypeName = "nvarchar(20)")]
    public VehicleType VehicleType  { get; set; }

    public string VehicleBody { get; set; }
    public string Color  { get; set; }
    
    
    
    
    
    //срок действия техосмотра
    
    // добавить сервис который будет проверять техосмотр каждый день
    
    // добавить страховку

    public IEnumerable<FeedBack>? FeedBack { get; set; }
    public IEnumerable<Deal> Deals { get; set; }
    public Lender Owner { get; set; }
    public Activity Activity { get; set; }

}