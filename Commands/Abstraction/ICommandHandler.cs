namespace Filebin.Common.Commands.Abstraction;

public interface ICommandHandler { }

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>, ICommandHandler
    where TCommand : ICommand<TResponse> { }

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>, ICommandHandler
where TCommand : ICommand { }