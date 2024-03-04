namespace Filebin.Common.Auth.Abstraction;

public interface IAuthorizedResource {
    public string OwnerId { get; }
}