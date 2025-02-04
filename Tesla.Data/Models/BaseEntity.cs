using System.ComponentModel.DataAnnotations;

namespace Tesla.Data.Models;

public class BaseEntity<TId>
where TId : struct
{
    [Key]
    public TId Id { get; set; }
}