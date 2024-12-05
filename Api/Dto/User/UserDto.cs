namespace Api.Dtos;

public class UserDto : BaseDto
{
    public required string Username {get; set;}
    public required string Email {get; set;}
    public DateTime BirthDate {get; set;}
}