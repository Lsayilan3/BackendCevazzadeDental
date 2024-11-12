
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.BlogDetails.ValidationRules;

namespace Business.Handlers.BlogDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBlogDetailCommand : IRequest<IResult>
    {

        public int BlogId { get; set; }
        public string Tarih { get; set; }
        public string Aciklayan { get; set; }
        public string Editor { get; set; }
        public string Photo { get; set; }
        public int Dil { get; set; }


        public class CreateBlogDetailCommandHandler : IRequestHandler<CreateBlogDetailCommand, IResult>
        {
            private readonly IBlogDetailRepository _blogDetailRepository;
            private readonly IMediator _mediator;
            public CreateBlogDetailCommandHandler(IBlogDetailRepository blogDetailRepository, IMediator mediator)
            {
                _blogDetailRepository = blogDetailRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBlogDetailValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBlogDetailCommand request, CancellationToken cancellationToken)
            {
                //var isThereBlogDetailRecord = _blogDetailRepository.Query().Any(u => u.BlogId == request.BlogId);

                //if (isThereBlogDetailRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedBlogDetail = new BlogDetail
                {
                    BlogId = request.BlogId,
                    Tarih = request.Tarih,
                    Aciklayan = request.Aciklayan,
                    Editor = request.Editor,
                    Photo = request.Photo,
                    Dil = request.Dil,

                };

                _blogDetailRepository.Add(addedBlogDetail);
                await _blogDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}