
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Blogs.Queries
{
    public class GetBlogQuery : IRequest<IDataResult<Blog>>
    {
        public int BlogId { get; set; }

        public class GetBlogQueryHandler : IRequestHandler<GetBlogQuery, IDataResult<Blog>>
        {
            private readonly IBlogRepository _blogRepository;
            private readonly IMediator _mediator;

            public GetBlogQueryHandler(IBlogRepository blogRepository, IMediator mediator)
            {
                _blogRepository = blogRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Blog>> Handle(GetBlogQuery request, CancellationToken cancellationToken)
            {
                var blog = await _blogRepository.GetAsync(p => p.BlogId == request.BlogId);
                return new SuccessDataResult<Blog>(blog);
            }
        }
    }
}
