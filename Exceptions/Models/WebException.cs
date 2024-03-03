namespace Filebin.Common.Util.Exceptions;

public abstract class WebException : ApplicationException {
    public abstract int statusCode { get; }
    public string Title;
    public WebException(string title) => Title = title;
    public WebException(string title, string? message) : base(message) => Title = title;
    public WebException(string title, string? message, Exception? innerException)
        : base(message, innerException) => Title = title;
}