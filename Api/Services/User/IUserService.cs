namespace Api.Services;

public interface IUserService 
{
    Task<ServiceResponse<UserDto>> GetUserById(int id);
    Task<ServiceResponseList<UserDto>> GetAllUsers();
    Task<Pagination<UserDto>> GetUsers(int page, int pageSize = 20);
    Task<ServiceResponse<UserDto>> AddUser(AddUserDto payload);
    Task<ServiceResponse<UserDto>> UpdateUser(UpdateUserDto payload, int id);
}