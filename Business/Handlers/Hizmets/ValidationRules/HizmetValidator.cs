
using Business.Handlers.Hizmets.Commands;
using FluentValidation;

namespace Business.Handlers.Hizmets.ValidationRules
{

    public class CreateHizmetValidator : AbstractValidator<CreateHizmetCommand>
    {
        public CreateHizmetValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
    public class UpdateHizmetValidator : AbstractValidator<UpdateHizmetCommand>
    {
        public UpdateHizmetValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();

        }
    }
}