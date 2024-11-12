
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

namespace Business.Handlers.Hizmets.Queries
{

    public class GetHizmetsQuery : IRequest<IDataResult<IEnumerable<Hizmet>>>
    {
        public int Dil { get; set; }
        public class GetHizmetsQueryHandler : IRequestHandler<GetHizmetsQuery, IDataResult<IEnumerable<Hizmet>>>
        {
            private readonly IHizmetRepository _hizmetRepository;
            private readonly IMediator _mediator;

            public GetHizmetsQueryHandler(IHizmetRepository hizmetRepository, IMediator mediator)
            {
                _hizmetRepository = hizmetRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Hizmet>>> Handle(GetHizmetsQuery request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<Hizmet>>(await _hizmetRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Hizmet>>(await _hizmetRepository.GetListAsync(x => x.Dil == request.Dil));
            }
        }
    }
}

