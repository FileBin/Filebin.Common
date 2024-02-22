using Filebin.Common.Validation.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace Filebin.Common.Exceptions;

public static class ConfigureServices {
    public static void ConfigureExceptions(this IServiceCollection services) => services.AddExceptionHandler<ExceptionHandler>();
}