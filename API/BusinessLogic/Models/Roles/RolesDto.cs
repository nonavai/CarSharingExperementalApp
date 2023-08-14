namespace BusinessLogic.Models.Roles;

public class RolesDto
{
    public int Id { get; set; }
    public bool Admin { get; set; }
    public int? BorrowerId { get; set; }
    public int? LenderId { get; set; }
}