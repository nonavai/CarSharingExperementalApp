namespace DataAccess.Entities;

public class Lender : EntityBase
{
    public int UserId { get; set; }
    public bool IsVerified { get; set; }
    
    //some info that needed from Lender
    public User User { get; set; }
    public IEnumerable<Deal> Deals { get; set; }
    public IEnumerable<Car> Cars { get; set; }
    public Roles Roles { get; set; }

    
}