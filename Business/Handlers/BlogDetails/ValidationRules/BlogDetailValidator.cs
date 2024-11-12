
using Business.Handlers.BlogDetails.Commands;
using FluentValidation;

namespace Business.Handlers.BlogDetails.ValidationRules
{

    public class CreateBlogDetailValidator : AbstractValidator<CreateBlogDetailCommand>
    {
        public CreateBlogDetailValidator()
        {
            //RuleFor(x => x.BlogId).NotEmpty();
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.Aciklayan).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
    public class UpdateBlogDetailValidator : AbstractValidator<UpdateBlogDetailCommand>
    {
        public UpdateBlogDetailValidator()
        {
            //RuleFor(x => x.BlogId).NotEmpty();
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.Aciklayan).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
}