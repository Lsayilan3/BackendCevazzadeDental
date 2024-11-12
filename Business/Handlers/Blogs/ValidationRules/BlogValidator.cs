
using Business.Handlers.Blogs.Commands;
using FluentValidation;

namespace Business.Handlers.Blogs.ValidationRules
{

    public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Aciklayan).NotEmpty();
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
    public class UpdateBlogValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Aciklayan).NotEmpty();
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
}