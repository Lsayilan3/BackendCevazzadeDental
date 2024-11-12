
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

    public class GetBlogDetailsQuery : IRequest<IDataResult<IEnumerable<BlogDetail>>>
    {
        public class GetBlogDetailsQueryHandler : IRequestHandler<GetBlogDetailsQuery, IDataResult<IEnumerable<BlogDetail>>>
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
            public async Task<IDataResult<IEnumerable<BlogDetail>>> Handle(GetBlogDetailsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<BlogDetail>>(await _blogDetailRepository.GetListAsync());
            }
        }
    }
}