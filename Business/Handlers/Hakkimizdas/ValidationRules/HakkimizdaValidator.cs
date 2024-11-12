
using Business.Handlers.Hakkimizdas.Commands;
using FluentValidation;

namespace Business.Handlers.Hakkimizdas.ValidationRules
{

    public class CreateHakkimizdaValidator : AbstractValidator<CreateHakkimizdaCommand>
    {
        public CreateHakkimizdaValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();

        }
    }
    public class UpdateHakkimizdaValidator : AbstractValidator<UpdateHakkimizdaCommand>
    {
        public UpdateHakkimizdaValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();

        }
    }
}