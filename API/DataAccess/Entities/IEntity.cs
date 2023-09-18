using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public abstract class EntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}