using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ChatService.Api.DTOS;

public sealed class ResultDTO<T>
{
    public T? Data { get; set; }

    private string? _message;
    public string? Message
    {
        get
        {
            if (Data == null && Errors == null)
            {
                return "The request has been successfully processed.";
            }

            if (StatusCode == HttpStatusCode.InternalServerError ||
                StatusCode == HttpStatusCode.BadRequest)
            {
                return _message ?? "Something went wrong.";
            }

            if (StatusCode == HttpStatusCode.NotFound)
            {
                return _message ?? "Not Found.";
            }

            return _message;
        }
        set
        {
            _message = value;
        }
    }

    private HttpStatusCode? _statusCode;
    public HttpStatusCode? StatusCode
    {
        get
        {
            if (Data == null && Errors == null)
            {
                return _statusCode ?? HttpStatusCode.NoContent;
            }
            if (Errors != null && Errors.Count > 0)
            {
                return _statusCode ?? HttpStatusCode.BadRequest;
            }
            if (!string.IsNullOrEmpty(_message) && _message.Contains("not found"))
            {
                return HttpStatusCode.NotFound;
            }

            return _statusCode ?? HttpStatusCode.OK;
        }
        set
        {
            _statusCode = value;
        }
    }

    public IDictionary<string, string>? Errors { get; set; }

    public static bool HasData([NotNullWhen(true)] T? data)
    {
        return data != null;
    }
}