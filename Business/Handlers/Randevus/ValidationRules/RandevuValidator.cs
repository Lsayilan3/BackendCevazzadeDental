
using Business.Handlers.Randevus.Commands;
using FluentValidation;

namespace Business.Handlers.Randevus.ValidationRules
{

    public class CreateRandevuValidator : AbstractValidator<CreateRandevuCommand>
    {
        public CreateRandevuValidator()
        {
            //RuleFor(x => x.Ad).NotEmpty();
            //RuleFor(x => x.Soyad).NotEmpty();
            //RuleFor(x => x.Tel).NotEmpty();
            //RuleFor(x => x.HizmetlerimizId).NotEmpty();
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.CraeteDate).NotEmpty();
            //RuleFor(x => x.Mesaj).NotEmpty();

        }
    }
    public class UpdateRandevuValidator : AbstractValidator<UpdateRandevuCommand>
    {
        public UpdateRandevuValidator()
        {
            //RuleFor(x => x.Ad).NotEmpty();
            //RuleFor(x => x.Soyad).NotEmpty();
            //RuleFor(x => x.Tel).NotEmpty();
            //RuleFor(x => x.HizmetlerimizId).NotEmpty();
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.CraeteDate).NotEmpty();
            //RuleFor(x => x.Mesaj).NotEmpty();

        }
    }
}