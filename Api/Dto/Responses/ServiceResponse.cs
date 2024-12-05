namespace Api.Dtos;

//Base Http response model => global response model usage.
public class ServiceResponse<T> where T : BaseDto
{
    public T? Data {get; set;}
    public string Message {get; set;} = string.Empty;

    public EErrorType ErrorType {get; set;} = EErrorType.SUCCESS;

    //other ?
}