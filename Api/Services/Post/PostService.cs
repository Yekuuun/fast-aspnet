
using AutoMapper;

namespace Api.Services;

public class PostService(IMapper mapper, PostRepository repository) : IPostService
{
    private readonly IMapper _mapper = mapper; 
    private readonly PostRepository _repository = repository;

    /// <summary>
    /// Adding a new single post.
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceResponse<PostDto>> AddPost(AddPostDto payload, int userId)
    {
        ServiceResponse<PostDto> response = new();
        try
        {
            Post newPost = new()
            {
                Description = payload.Description,
                Likes = 0,
                Status = true,
                UserId = userId
            };

            newPost = await _repository.InsertEntity(newPost);
            response.Data = _mapper.Map<PostDto>(newPost);
            return response;
        }
        catch(Exception ex)
        {
            return ErrorManager.ReturnError<PostDto>(EErrorType.BAD, ex.Message);
        }
    }

    /// <summary>
    /// Deleting a single POST.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceResponse<PostDto>> DeletePost(int id, int userId)
    {
        ServiceResponse<PostDto> response = new();
        try
        {
            Post p = await _repository.GetByIdAsync(id) ?? throw new NullReferenceException("post not found.");

            if(p.UserId != userId)
            {
                return ErrorManager.ReturnError<PostDto>(EErrorType.UNAUTHORIZED, "not owner.");
            }
            else
            {
                await _repository.DeleteAsync(p);
                response.Data = _mapper.Map<PostDto>(p);
                return response;
            }
        }
        catch(Exception ex)
        {
            return ErrorManager.ReturnError<PostDto>((ex is NullReferenceException) ? EErrorType.NOTFOUND : EErrorType.BAD, ex.Message);
        }
    }

    /// <summary>
    /// Getting all POSTS.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceResponseList<PostDto>> GetAllPosts()
    {
        ServiceResponseList<PostDto> response = new();
        try
        {
            response.Data = _mapper.Map<List<PostDto>>(await _repository.GetAllAsync());
            return response;
        }
        catch(Exception ex)
        {
            return ErrorManager.ReturnErrorList<PostDto>(EErrorType.BAD, ex.Message);
        }
    }

    /// <summary>
    /// Getting a post using it's ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<ServiceResponse<PostDto>> GetPostById(int id)
    {
        ServiceResponse<PostDto> response = new();
        try
        {
            Post p = await _repository.GetByIdAsync(id) ?? throw new NullReferenceException("post not found");
            response.Data = _mapper.Map<PostDto>(p);
            return response;
        }
        catch(Exception ex)
        {
            return ErrorManager.ReturnError<PostDto>((ex is NullReferenceException) ? EErrorType.NOTFOUND : EErrorType.BAD, ex.Message);
        }
    }

    /// <summary>
    /// Getting post list using pagination mecanism.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="userId"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<Pagination<PostDto>> GetPostList(int page, int userId, int pageSize = 20)
    {
        
        Pagination<PostDto> response = new();
        try
        {
            page = page == 0 ? ++page : page;

            int total = await _repository.CountUserPosts(userId);
            if(total == 0)
            {
                return ErrorManager.ReturnPageError<PostDto>();
            }
            else
            {
                int totalPages = (int)Math.Ceiling((double)total / pageSize);
                response.Data = _mapper.Map<List<PostDto>>(await _repository.GetUserPostPages(page, pageSize, userId));
                return response;
            }
        }
        catch(Exception)
        {
            return ErrorManager.ReturnPageError<PostDto>();
        }
    }

    /// <summary>
    /// Updating a single POST.
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceResponse<PostDto>> UpdatePost(UpdatePostDto payload, int userId)
    {
        ServiceResponse<PostDto> response = new();
        try
        {
            Post p = await _repository.GetByIdAsync(payload.Id) ?? throw new NullReferenceException("post not found.");

            if(p.UserId != userId)
                throw new UnauthorizedAccessException("access violation");
            
            p.Description = payload.Description;
            p.UpdateDate = DateTime.Now;

            await _repository.UpdateEntity(p);

            response.Data = _mapper.Map<PostDto>(p);
            return response;
        }
        catch(Exception ex)
        {
            response.Message = ex.Message;

            response.ErrorType = ex switch
            {
                NullReferenceException => EErrorType.NOTFOUND,
                UnauthorizedAccessException => EErrorType.UNAUTHORIZED,
                _ => EErrorType.BAD,
            };
            return response;
        }
    }
}