using FluentValidation;

namespace CamelRegistry.Validators
{
    public static class CamelValidationRules
    {
        public static IRuleBuilderOptions<T, string?> NameRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("A név megadása kötelező.")
                .MinimumLength(3).WithMessage("A név legalább 3 karakter legyen.")
                .Matches("^[a-zA-Z]+$").WithMessage("A név csak betűket tartalmazhat.");
        }

        public static IRuleBuilderOptions<T, string?> ColorRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MinimumLength(3).WithMessage("A szín legalább 3 karakter legyen.");
        }

        public static IRuleBuilderOptions<T, int> HumpCountRule<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .InclusiveBetween(1, 2).WithMessage("A púpok száma 1 vagy 2 lehet.");
        }

        public static IRuleBuilderOptions<T, DateTime> LastFedRule<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Az etetés ideje nem lehet a jövőben.");
        }
    }
}
