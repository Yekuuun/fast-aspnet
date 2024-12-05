using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class UpdateUserDto : BaseDto
{
    public required string Username {get; set;}

    [EmailAddress]
    public required string Email {get; set;}
}