using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

//Base user model.
public class User : BaseEntity
{
    [Column("username")]
    public required string Username {get; set;}

    [Column("email")]
    public required string Email {get; set;}

    [Column("birth_date")]
    public DateTime BirthDate {get; set;}
}