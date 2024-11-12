
using Business.Handlers.AnasayfaPhotoUrls.Commands;
using FluentValidation;

namespace Business.Handlers.AnasayfaPhotoUrls.ValidationRules
{

    public class CreateAnasayfaPhotoUrlValidator : AbstractValidator<CreateAnasayfaPhotoUrlCommand>
    {
        public CreateAnasayfaPhotoUrlValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
    public class UpdateAnasayfaPhotoUrlValidator : AbstractValidator<UpdateAnasayfaPhotoUrlCommand>
    {
        public UpdateAnasayfaPhotoUrlValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
}