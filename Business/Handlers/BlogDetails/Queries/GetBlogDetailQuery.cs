
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.BlogDetails.Queries
{
    public class GetBlogDetailQuery : IRequest<IDataResult<BlogDetail>>
    {
        public int BlogDetailId { get; set; }

        public class GetBlogDetailQueryHandler : IRequestHandler<GetBlogDetailQuery, IDataResult<BlogDetail>>
        {
            private readonly IBlogDetailRepository _blogDetailRepository;
            private readonly IMediator _mediator;

            public GetBlogDetailQueryHandler(IBlogDetailRepository blogDetailRepository, IMediator mediator)
            {
                _blogDetailRepository = blogDetailRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<BlogDetail>> Handle(GetBlogDetailQuery request, CancellationToken cancellationToken)
            {
                var blogDetail = await _blogDetailRepository.GetAsync(p => p.BlogDetailId == request.BlogDetailId);
                return new SuccessDataResult<BlogDetail>(blogDetail);
            }
        }
    }
}
