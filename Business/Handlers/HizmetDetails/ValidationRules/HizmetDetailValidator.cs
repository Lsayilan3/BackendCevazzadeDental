
using Business.Handlers.HizmetDetails.Commands;
using FluentValidation;

namespace Business.Handlers.HizmetDetails.ValidationRules
{

    public class CreateHizmetDetailValidator : AbstractValidator<CreateHizmetDetailCommand>
    {
        public CreateHizmetDetailValidator()
        {
            //RuleFor(x => x.HizmetId).NotEmpty();
            //RuleFor(x => x.PhotoService).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();

        }
    }
    public class UpdateHizmetDetailValidator : AbstractValidator<UpdateHizmetDetailCommand>
    {
        public UpdateHizmetDetailValidator()
        {
            //RuleFor(x => x.HizmetId).NotEmpty();
            //RuleFor(x => x.PhotoService).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();

        }
    }
}