using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpGet("all")]
    public async Task<ActionResult<ServiceResponseList<UserDto>>> GetUsers()
    {
        ServiceResponseList<UserDto> serviceResponse = await _service.GetAllUsers();
        ActionResult<ServiceResponseList<UserDto>> httpResult = await HttpManager.HttpListResponse(serviceResponse);
        return httpResult;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<UserDto>>> GetUserById(int id)
    {
        ServiceResponse<UserDto> serviceResponse = await _service.GetUserById(id);
        ActionResult<ServiceResponse<UserDto>> httpResult = await HttpManager.HttpResponse(serviceResponse);
        return httpResult;
    }

    [HttpGet("")]
    public async Task<ActionResult<Pagination<UserDto>>> GetUsers([FromQuery] int page, [FromQuery] int? window)
    {
        Pagination<UserDto> serviceResponse = await _service.GetUsers(page, window ?? 20);
        ActionResult<Pagination<UserDto>> httpResult = await HttpManager.HttpPagination(serviceResponse);
        return httpResult;
    }

    [HttpPost("")]
    public async Task<ActionResult<ServiceResponse<UserDto>>> AddUser(AddUserDto user)
    {
        ServiceResponse<UserDto> response = await _service.AddUser(user);
        ActionResult<ServiceResponse<UserDto>> result = await HttpManager.HttpResponse(response);
        return result;
    }

    [HttpPut("")]
    public async Task<ActionResult<ServiceResponse<UserDto>>> UpdateUser(UpdateUserDto payload)
    {
        ServiceResponse<UserDto> response = await _service.UpdateUser(payload, 1); //USING TOKEN BASE USER ID. => TO DO.
        ActionResult<ServiceResponse<UserDto>> result = await HttpManager.HttpResponse(response);
        return result;
    }
}