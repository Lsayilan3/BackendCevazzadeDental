
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

namespace Business.Handlers.HizmetDetails.Queries
{

    public class GetHizmetDetailsQuery : IRequest<IDataResult<IEnumerable<HizmetDetail>>>
    {
        public class GetHizmetDetailsQueryHandler : IRequestHandler<GetHizmetDetailsQuery, IDataResult<IEnumerable<HizmetDetail>>>
        {
            private readonly IHizmetDetailRepository _hizmetDetailRepository;
            private readonly IMediator _mediator;

            public GetHizmetDetailsQueryHandler(IHizmetDetailRepository hizmetDetailRepository, IMediator mediator)
            {
                _hizmetDetailRepository = hizmetDetailRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<HizmetDetail>>> Handle(GetHizmetDetailsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<HizmetDetail>>(await _hizmetDetailRepository.GetListAsync());
            }
        }
    }
}