namespace Filebin.Common.Validation;

public static class ValidationExtensions {

    public static IRuleBuilderOptions<T, string> PasswordValidation<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.NotEmpty().Length(8, 256);
    }

    public static IRuleBuilderOptions<T, string> UsernameValidation<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.LoginValidation().Matches("^[A-Za-z0-9_\\.-]{3,30}$");
    }

    public static IRuleBuilderOptions<T, string> EmailValidation<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.LoginValidation().Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,10}$");
    }

    public static IRuleBuilderOptions<T, string> LoginValidation<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.NotEmpty().MaximumLength(80);
    }

    public static IRuleBuilderOptions<T, decimal> PriceValidation<T>(this IRuleBuilder<T, decimal> ruleBuilder) {
        return ruleBuilder.Must(x => x >= 0.00m);
    }

    public static IRuleBuilderOptions<T, string> TitleValidation<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.NotEmpty().Length(3, 128);
    }

    public static IRuleBuilderOptions<T, string> DescriptionValidation<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.MaximumLength(1024);
    }

    public static IRuleBuilderOptions<T, string> SearchStringValidation<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.MaximumLength(256);
    }

    public static IRuleBuilderOptions<T, string> ImageContentValidation<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.NotEmpty().Matches("^image\\/(gif|jpeg|pjpeg|png|svg\\+xml|tiff|webp)$");
    }

    public static IRuleBuilderOptions<T, string> GuidValidation<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.NotEmpty().Must(x => Guid.TryParse(x, out _));
    }
}