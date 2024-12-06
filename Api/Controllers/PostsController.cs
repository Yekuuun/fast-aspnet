using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[EnableRateLimiting("fixed")] // You can use it on specific controllers => THIS IS A SAMPLE for DEMO.
public class PostsController(IPostService service) : ControllerBase
{
    private readonly IPostService _service = service;

    [HttpGet("all")]
    public async Task<ActionResult<ServiceResponseList<PostDto>>> GetAllPosts()
    {
        ServiceResponseList<PostDto> serviceResponse = await _service.GetAllPosts();
        ActionResult<ServiceResponseList<PostDto>> httpResult = await HttpManager.HttpListResponse(serviceResponse);
        return httpResult;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<PostDto>>> GetPostById(int id)
    {
        ServiceResponse<PostDto> serviceResponse = await _service.GetPostById(id);
        ActionResult<ServiceResponse<PostDto>> httpResult = await HttpManager.HttpResponse(serviceResponse);
        return httpResult;
    }

    [HttpGet("")]
    public async Task<ActionResult<Pagination<PostDto>>> GetPosts([FromQuery] int page, [FromQuery] int? userId, [FromQuery] int? window)
    {
        Pagination<PostDto> serviceResponse = await _service.GetPostList(page, userId ?? 0, window ?? 20);
        ActionResult<Pagination<PostDto>> httpResult = await HttpManager.HttpPagination(serviceResponse);
        return httpResult;
    }

    [HttpPost("")]
    public async Task<ActionResult<ServiceResponse<PostDto>>> AddPost(AddPostDto post)
    {
        if(!PayloadValidator.IsDcoPayloadSafe(post))
        {
            return PayloadValidator.BuildError<PostDto>("bad payload");
        }
        ServiceResponse<PostDto> response = await _service.AddPost(post, 1); //USING TOKEN BASE USER ID. => TO DO.
        ActionResult<ServiceResponse<PostDto>> result = await HttpManager.HttpResponse(response);
        return result;
    }

    [HttpPut("")]
    public async Task<ActionResult<ServiceResponse<PostDto>>> UpdatePost(UpdatePostDto payload)
    {
        if(!PayloadValidator.IsDcoPayloadSafe(payload))
        {
            return PayloadValidator.BuildError<PostDto>("bad payload");
        }
        ServiceResponse<PostDto> response = await _service.UpdatePost(payload, 1); //USING TOKEN BASE USER ID. => TO DO.
        ActionResult<ServiceResponse<PostDto>> result = await HttpManager.HttpResponse(response);
        return result;
    }

    [HttpDelete("")]
    public async Task<ActionResult<ServiceResponse<PostDto>>> DeletePost([FromQuery] int id)
    {
        ServiceResponse<PostDto> response = await _service.DeletePost(id, 1); //USING TOKEN BASE USER ID. => TO DO.
        ActionResult<ServiceResponse<PostDto>> result = await HttpManager.HttpResponse(response);
        return result;
    }
}