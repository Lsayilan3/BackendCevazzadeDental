using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.BlogDetails.Queries
{
    public class GetBlogDetailListByBlogId : IRequest<IDataResult<IEnumerable<BlogDetail>>>
    {
        public int BlogId { get; set; }
        public int Dil { get; set; }

        public class GetBlogDetailsQueryHandler : IRequestHandler<GetBlogDetailListByBlogId, IDataResult<IEnumerable<BlogDetail>>>
        {
            private readonly IBlogDetailRepository _blogDetailRepository;
            private readonly IMediator _mediator;

            public GetBlogDetailsQueryHandler(IBlogDetailRepository blogDetailRepository, IMediator mediator)
            {
                _blogDetailRepository = blogDetailRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BlogDetail>>> Handle(GetBlogDetailListByBlogId request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<BlogDetail>>(await _blogDetailRepository.GetListAsync(x => x.BlogId == request.BlogId));
                }
                return new SuccessDataResult<IEnumerable<BlogDetail>>(await _blogDetailRepository.GetListAsync(x => x.BlogId == request.BlogId && x.Dil == request.Dil));
                
            }
        }
    }
}
