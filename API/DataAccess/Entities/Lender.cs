namespace DataAccess.Entities;

public class Lender
{
    public int Id { get; set; }

    public bool IsVerified { get; set; }
    
    //some info that needed from Lender
    public IEnumerable<Deal> Deals { get; set; }
    public IEnumerable<Car> Cars { get; set; }
    public Roles Roles { get; set; }

    
}