using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public static class HttpManager
{
    public static Task<ActionResult<ServiceResponse<T>>> HttpResponse<T>(ServiceResponse<T> packet) where T : BaseDto
    {
        return packet.ErrorType switch
        {
            EErrorType.NOTFOUND => Task.FromResult<ActionResult<ServiceResponse<T>>>(new NotFoundObjectResult(packet)),
            EErrorType.SUCCESS => Task.FromResult<ActionResult<ServiceResponse<T>>>(new OkObjectResult(packet)),
            EErrorType.UNAUTHORIZED => Task.FromResult<ActionResult<ServiceResponse<T>>>(new UnauthorizedObjectResult(packet)),
            EErrorType.BAD => Task.FromResult<ActionResult<ServiceResponse<T>>>(new BadRequestObjectResult(packet)),
            _ => Task.FromResult<ActionResult<ServiceResponse<T>>>(new BadRequestObjectResult(packet)),
        };
    }

    public static Task<ActionResult<ServiceResponseList<T>>> HttpListResponse<T>(ServiceResponseList<T> packet) where T : BaseDto
    {
        return packet.ErrorType switch
        {
            EErrorType.NOTFOUND => Task.FromResult<ActionResult<ServiceResponseList<T>>>(new NotFoundObjectResult(packet)),
            EErrorType.SUCCESS => Task.FromResult<ActionResult<ServiceResponseList<T>>>(new OkObjectResult(packet)),
            EErrorType.UNAUTHORIZED => Task.FromResult<ActionResult<ServiceResponseList<T>>>(new UnauthorizedObjectResult(packet)),
            EErrorType.BAD => Task.FromResult<ActionResult<ServiceResponseList<T>>>(new BadRequestObjectResult(packet)),
            _ => Task.FromResult<ActionResult<ServiceResponseList<T>>>(new BadRequestObjectResult(packet)),
        };
    }

    public static Task<ActionResult<Pagination<T>>> HttpPagination<T>(Pagination<T> packet) where T : BaseDto
    {
        return packet.ErrorType switch
        {
            EErrorType.NOTFOUND => Task.FromResult<ActionResult<Pagination<T>>>(new NotFoundObjectResult(packet)),
            EErrorType.SUCCESS => Task.FromResult<ActionResult<Pagination<T>>>(new OkObjectResult(packet)),
            EErrorType.UNAUTHORIZED => Task.FromResult<ActionResult<Pagination<T>>>(new UnauthorizedObjectResult(packet)),
            EErrorType.BAD => Task.FromResult<ActionResult<Pagination<T>>>(new BadRequestObjectResult(packet)),
            _ => Task.FromResult<ActionResult<Pagination<T>>>(new BadRequestObjectResult(packet)),
        };
    }
}