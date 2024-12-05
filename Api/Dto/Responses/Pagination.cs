namespace Api.Dtos;

public class Pagination<T>() where T : BaseDto
{
    public List<T> Data {get; set;} = [];
    public int Page {get; set;}
    public int Total {get; set;}
}