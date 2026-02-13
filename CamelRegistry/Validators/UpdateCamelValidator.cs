using CamelRegistry.DTOs.Requests;
using FluentValidation;

namespace CamelRegistry.Validators
{
    public class UpdateCamelValidator : AbstractValidator<UpdateCamelDto>
    {
        public UpdateCamelValidator()
        {
            When(x => x.Name != null, () => RuleFor(x => x.Name!).NameRule());
            When(x => x.Color != null, () => RuleFor(x => x.Color!).ColorRule());
            When(x => x.HumpCount.HasValue, () => RuleFor(x => x.HumpCount.Value).HumpCountRule());
            When(x => x.LastFed.HasValue, () => RuleFor(x => x.LastFed.Value).LastFedRule());
        }
    }
}
