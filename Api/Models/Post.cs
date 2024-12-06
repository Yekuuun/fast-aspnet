using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

/// <summary>
/// Base POST simple model for demo (no complex DB implementation)
/// </summary>
public class Post : BaseEntity
{
    [MaxLength(500)]
    [Column("description")]
    public required string Description {get; set;}
    [Column("likes")]
    public int Likes {get; set;}

    [Column("status")]
    public bool Status {get; set;} = true;

    [Column("update_date")]
    public DateTime UpdateDate {get; set;} = DateTime.Now;

    //FK
    [Column("user_id")]
    public int UserId {get; set;}
    public User? User {get; set;}
}