
using Business.Handlers.Durums.Commands;
using FluentValidation;

namespace Business.Handlers.Durums.ValidationRules
{

    public class CreateDurumValidator : AbstractValidator<CreateDurumCommand>
    {
        public CreateDurumValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.ParagrafOne).NotEmpty();
            //RuleFor(x => x.ParagrafTwo).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
    public class UpdateDurumValidator : AbstractValidator<UpdateDurumCommand>
    {
        public UpdateDurumValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.ParagrafOne).NotEmpty();
            //RuleFor(x => x.ParagrafTwo).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
}