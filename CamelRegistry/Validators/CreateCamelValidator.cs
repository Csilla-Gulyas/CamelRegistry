using CamelRegistry.DTOs.Requests;
using FluentValidation;

namespace CamelRegistry.Validators
{
    public class CreateCamelValidator : AbstractValidator<CreateCamelDto>
    {
        public CreateCamelValidator()
        {
            RuleFor(x => x.Name).NameRule();
            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("A szín megadása kötelező.")
                .ColorRule();
            RuleFor(x => x.HumpCount).HumpCountRule();
            RuleFor(x => x.LastFed).LastFedRule();
        }
    }
}
