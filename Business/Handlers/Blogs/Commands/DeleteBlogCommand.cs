
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


namespace Business.Handlers.Blogs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBlogCommand : IRequest<IResult>
    {
        public int BlogId { get; set; }

        public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, IResult>
        {
            private readonly IBlogRepository _blogRepository;
            private readonly IMediator _mediator;

            public DeleteBlogCommandHandler(IBlogRepository blogRepository, IMediator mediator)
            {
                _blogRepository = blogRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
            {
                var blogToDelete = _blogRepository.Get(p => p.BlogId == request.BlogId);

                _blogRepository.Delete(blogToDelete);
                await _blogRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

