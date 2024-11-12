
using Business.Handlers.Hekims.Commands;
using FluentValidation;

namespace Business.Handlers.Hekims.ValidationRules
{

    public class CreateHekimValidator : AbstractValidator<CreateHekimCommand>
    {
        public CreateHekimValidator()
        {
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
    public class UpdateHekimValidator : AbstractValidator<UpdateHekimCommand>
    {
        public UpdateHekimValidator()
        {
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