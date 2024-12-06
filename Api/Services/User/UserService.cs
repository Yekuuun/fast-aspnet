
using AutoMapper;

namespace Api.Services;

public class UserService(UserRepository repository, IMapper mapper) : IUserService
{
    private readonly IMapper _mapper = mapper;
    private readonly UserRepository _repository = repository;

    /// <summary>
    /// Adding a new single user.
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceResponse<UserDto>> AddUser(AddUserDto payload)
    {
        ServiceResponse<UserDto> response = new();
        try
        {
            User newUser = new()
            {
                Username = payload.Username,
                BirthDate = payload.BirthDate,
                Email = payload.Email
            };

            newUser = await _repository.InsertEntity(newUser);
            response.Data = _mapper.Map<UserDto>(newUser);
            return response;
        }
        catch(Exception ex)
        {
            return ErrorManager.ReturnError<UserDto>(EErrorType.BAD, ex.Message);
        }
    }

    /// <summary>
    /// Get all users.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceResponseList<UserDto>> GetAllUsers()
    {
        ServiceResponseList<UserDto> response = new();
        try
        {
            List<User> users = await _repository.GetAllAsync();
            response.Data = _mapper.Map<List<UserDto>>(users);
            return response;
        }
        catch(Exception ex)
        {
            return ErrorManager.ReturnErrorList<UserDto>(EErrorType.BAD, ex.Message);
        }
    }
    
    /// <summary>
    /// Getting a user using it's id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<ServiceResponse<UserDto>> GetUserById(int id)
    {
        ServiceResponse<UserDto> response = new();
        try
        {
            User u = await _repository.GetByIdAsync(id) ?? throw new NullReferenceException("user not found");
            response.Data = _mapper.Map<UserDto>(u);
            return response;
        }
        catch(Exception ex)
        {
            return ErrorManager.ReturnError<UserDto>((ex is NullReferenceException )? EErrorType.NOTFOUND : EErrorType.BAD, ex.Message);
        }
    }

    /// <summary>
    /// Getting a list of users using pagination response model.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<Pagination<UserDto>> GetUsers(int page, int pageSize = 20)
    {
        Pagination<UserDto> response = new();
        try
        {
            page = page == 0 ? ++page : page;

            int total = await _repository.CountAsync();
            if(total == 0)
            {
                return ErrorManager.ReturnPageError<UserDto>(EErrorType.SUCCESS);
            }
            else
            {
                int totalPages = (int)Math.Ceiling((double)total / pageSize);

                List<User> users = await _repository.PaginteAsync(page, pageSize);
                response.Data = _mapper.Map<List<UserDto>>(users);
                return response;
            }
        }
        catch(Exception)
        {
            return ErrorManager.ReturnPageError<UserDto>(EErrorType.BAD);
        }
    }
    
    /// <summary>
    /// Updating a single user informations.
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceResponse<UserDto>> UpdateUser(UpdateUserDto payload, int id)
    {
        ServiceResponse<UserDto> response = new();
        try
        {
            User u = await _repository.GetByIdAsync(id) ?? throw new NullReferenceException("user not found");

            u.Email = payload.Email;
            u.Username = payload.Username;

            await _repository.UpdateEntity(u);

            response.Data = _mapper.Map<UserDto>(u);
            return response;
        }
        catch(Exception ex)
        {
            return ErrorManager.ReturnError<UserDto>((ex is NullReferenceException )? EErrorType.NOTFOUND : EErrorType.BAD, ex.Message);
        }
    }
}