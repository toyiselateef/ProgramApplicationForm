
using ProgramApplicationForm.Api.Models; 
using ProgramApplicationForm.Application.Exceptions;

using Newtonsoft.Json;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message, e);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = exception switch
        {
            BadRequestException _ => StatusCodes.Status400BadRequest,
            NotFoundException _ => StatusCodes.Status404NotFound,
            UnAuthorizedException _ => StatusCodes.Status401Unauthorized,
            ForbiddenException _ => StatusCodes.Status403Forbidden,
            UnProcessedEntityException _ => StatusCodes.Status422UnprocessableEntity,
            ServerErrorException _ => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = ApiResponse<object>.ErrorResponse(exception.Message);



        if (httpContext.Response.StatusCode == StatusCodes.Status500InternalServerError)
        {
            response = ApiResponse<object>.ErrorResponse("an unexpected result occured");
        }

        await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }



}

