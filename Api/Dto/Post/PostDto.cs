namespace Api.Dtos;

public class PostDto : BaseDto
{
    public required string Description {get; set;}
    public int Likes {get; set;}
    public bool Status {get; set;} = true;
    public DateTime UpdateDate {get; set;} = DateTime.Now;
    public int UserId {get; set;}
}