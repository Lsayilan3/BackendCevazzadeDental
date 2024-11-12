
using Business.Handlers.Anasayfas.Commands;
using FluentValidation;

namespace Business.Handlers.Anasayfas.ValidationRules
{

    public class CreateAnasayfaValidator : AbstractValidator<CreateAnasayfaCommand>
    {
        public CreateAnasayfaValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.BaslikYazi).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
    public class UpdateAnasayfaValidator : AbstractValidator<UpdateAnasayfaCommand>
    {
        public UpdateAnasayfaValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.BaslikYazi).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
}