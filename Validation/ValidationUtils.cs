namespace Filebin.Common.Validation;

public static class ValidationUtils {
    public static void Validate<TValidator, T>(T obj)
    where TValidator : IValidator<T>, new()
    where T : class {
        var context = new ValidationContext<T>(obj);
        var result = new TValidator().Validate(context);
        if (!result.IsValid) {
            throw new ValidationException(result.Errors);
        }
    }
}
