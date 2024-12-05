namespace Api.Services;

public static class ErrorManager
{
    public static ServiceResponse<T> ReturnError<T>(EErrorType errorType, string message) where T : BaseDto
    {
        ServiceResponse<T> errorResponse = new()
        {
            ErrorType = errorType,
            Message = message,
        };

        return errorResponse;
    }

    public static ServiceResponseList<T> ReturnErrorList<T>(EErrorType errorType, string message) where T : BaseDto
    {
        ServiceResponseList<T> errorResponse = new()
        {
            ErrorType = errorType,
            Message = message,
        };

        return errorResponse;
    }

    public static Pagination<T> ReturnPageError<T>() where T : BaseDto
    {
        Pagination<T> error = new()
        {
            Page = 0,
            Total = 0,
            Data = []
        };

        return error;
    }
}