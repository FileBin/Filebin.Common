using Microsoft.AspNetCore.Http;

namespace Filebin.Common.Util.Exceptions;

public class ForbiddenException : WebException {

    public override int statusCode => StatusCodes.Status403Forbidden;
    private static readonly string title = "Forbidden";
    
    public ForbiddenException() : base(title) { }
    public ForbiddenException(string? message) : base(title, message) { }
    public ForbiddenException(string? message, Exception? innerException) : base(title, message, innerException) { }
}