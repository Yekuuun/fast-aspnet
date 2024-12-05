using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class AddUserDto : BaseDto
{
    public required string Username {get; set;}

    [EmailAddress]
    public required string Email {get; set;}
    public DateTime BirthDate {get; set;}
}