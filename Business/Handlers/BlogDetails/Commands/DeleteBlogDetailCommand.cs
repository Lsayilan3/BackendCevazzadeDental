
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.BlogDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBlogDetailCommand : IRequest<IResult>
    {
        public int BlogDetailId { get; set; }

        public class DeleteBlogDetailCommandHandler : IRequestHandler<DeleteBlogDetailCommand, IResult>
        {
            private readonly IBlogDetailRepository _blogDetailRepository;
            private readonly IMediator _mediator;

            public DeleteBlogDetailCommandHandler(IBlogDetailRepository blogDetailRepository, IMediator mediator)
            {
                _blogDetailRepository = blogDetailRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBlogDetailCommand request, CancellationToken cancellationToken)
            {
                var blogDetailToDelete = _blogDetailRepository.Get(p => p.BlogDetailId == request.BlogDetailId);

                _blogDetailRepository.Delete(blogDetailToDelete);
                await _blogDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

