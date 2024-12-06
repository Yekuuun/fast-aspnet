namespace Api.Services;

public interface IPostService
{
    Task<ServiceResponse<PostDto>> GetPostById(int id);
    Task<ServiceResponseList<PostDto>> GetAllPosts();
    Task<Pagination<PostDto>> GetPostList(int page, int userId, int pageSize = 20);
    Task<ServiceResponse<PostDto>> AddPost(AddPostDto payload, int userId);
    Task<ServiceResponse<PostDto>> UpdatePost(UpdatePostDto payload, int userId);
    Task<ServiceResponse<PostDto>> DeletePost(int id, int userId);
}