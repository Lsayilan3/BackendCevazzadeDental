
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

namespace Business.Handlers.Blogs.Queries
{

    public class GetBlogsQuery : IRequest<IDataResult<IEnumerable<Blog>>>
    {
        public int Dil { get; set; }
        public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, IDataResult<IEnumerable<Blog>>>
        {
            private readonly IBlogRepository _blogRepository;
            private readonly IMediator _mediator;

            public GetBlogsQueryHandler(IBlogRepository blogRepository, IMediator mediator)
            {
                _blogRepository = blogRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Blog>>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<Blog>>(await _blogRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Blog>>(await _blogRepository.GetListAsync(x => x.Dil == request.Dil));
            }
        }
    }
}
