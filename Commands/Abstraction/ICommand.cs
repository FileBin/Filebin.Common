namespace Filebin.Common.Commands.Abstraction;

public interface IBaseCommand { }

public interface ICommand<out TResponse> : IRequest<TResponse>, IBaseCommand { }

public interface ICommand : IRequest, IBaseCommand { }