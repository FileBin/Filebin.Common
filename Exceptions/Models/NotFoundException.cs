
using Microsoft.AspNetCore.Http;

namespace Filebin.Common.Util.Exceptions;

public class NotFoundException : WebException {
    private static readonly string title = "NotFound";

    public override int statusCode => StatusCodes.Status404NotFound;

    public static NotFoundException ParameterNotFound(string name) => new NotFoundException($"parameter {name} not found");

    public static NotFoundException NotFoundInDatabase(string name) => new NotFoundException($"{name} not found in database");

    public NotFoundException() : base(title) { }
    public NotFoundException(string? message) : base(title, message) { }
    public NotFoundException(string? message, Exception? innerException) : base(title, message, innerException) { }
}
