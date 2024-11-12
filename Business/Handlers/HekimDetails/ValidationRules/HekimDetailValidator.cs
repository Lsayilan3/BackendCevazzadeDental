
using Business.Handlers.HekimDetails.Commands;
using FluentValidation;

namespace Business.Handlers.HekimDetails.ValidationRules
{

    public class CreateHekimDetailValidator : AbstractValidator<CreateHekimDetailCommand>
    {
        public CreateHekimDetailValidator()
        {
            //RuleFor(x => x.HekimId).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Ad).NotEmpty();
            //RuleFor(x => x.Uzmanlik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.SosyalFace).NotEmpty();
            //RuleFor(x => x.SosyalTwitter).NotEmpty();
            //RuleFor(x => x.Sosyalingstagram).NotEmpty();
            //RuleFor(x => x.SosyalMail).NotEmpty();

        }
    }
    public class UpdateHekimDetailValidator : AbstractValidator<UpdateHekimDetailCommand>
    {
        public UpdateHekimDetailValidator()
        {
            //RuleFor(x => x.HekimId).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Ad).NotEmpty();
            //RuleFor(x => x.Uzmanlik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.SosyalFace).NotEmpty();
            //RuleFor(x => x.SosyalTwitter).NotEmpty();
            //RuleFor(x => x.Sosyalingstagram).NotEmpty();
            //RuleFor(x => x.SosyalMail).NotEmpty();

        }
    }
}