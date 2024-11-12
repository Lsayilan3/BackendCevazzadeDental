
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Blogs.ValidationRules;


namespace Business.Handlers.Blogs.Commands
{


    public class UpdateBlogCommand : IRequest<IResult>
    {
        public int BlogId { get; set; }
        public string Photo { get; set; }
        public string Aciklayan { get; set; }
        public string Tarih { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Dil { get; set; }

        public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, IResult>
        {
            private readonly IBlogRepository _blogRepository;
            private readonly IMediator _mediator;

            public UpdateBlogCommandHandler(IBlogRepository blogRepository, IMediator mediator)
            {
                _blogRepository = blogRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBlogValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
            {
                var isThereBlogRecord = await _blogRepository.GetAsync(u => u.BlogId == request.BlogId);


                isThereBlogRecord.Photo = request.Photo;
                isThereBlogRecord.Aciklayan = request.Aciklayan;
                isThereBlogRecord.Tarih = request.Tarih;
                isThereBlogRecord.Baslik = request.Baslik;
                isThereBlogRecord.Aciklama = request.Aciklama;
                isThereBlogRecord.Dil = request.Dil;


                _blogRepository.Update(isThereBlogRecord);
                await _blogRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

