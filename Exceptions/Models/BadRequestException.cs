namespace Filebin.Common.Util.Exceptions;

public class BadRequestException : WebException {
    private static readonly string title = "BadRequest";
    public BadRequestException() : base(title) { }
    public BadRequestException(string? message) : base(title, message) { }
    public BadRequestException(string? message, Exception? innerException) : base(title, message, innerException) { }
}