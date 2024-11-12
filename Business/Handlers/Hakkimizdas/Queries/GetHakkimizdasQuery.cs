
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

namespace Business.Handlers.Hakkimizdas.Queries
{

    public class GetHakkimizdasQuery : IRequest<IDataResult<IEnumerable<Hakkimizda>>>
    {
        public int Dil { get; set; }
        public class GetHakkimizdasQueryHandler : IRequestHandler<GetHakkimizdasQuery, IDataResult<IEnumerable<Hakkimizda>>>
        {
            private readonly IHakkimizdaRepository _hakkimizdaRepository;
            private readonly IMediator _mediator;

            public GetHakkimizdasQueryHandler(IHakkimizdaRepository hakkimizdaRepository, IMediator mediator)
            {
                _hakkimizdaRepository = hakkimizdaRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Hakkimizda>>> Handle(GetHakkimizdasQuery request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<Hakkimizda>>(await _hakkimizdaRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Hakkimizda>>(await _hakkimizdaRepository.GetListAsync(x => x.Dil == request.Dil));
            }
        }
    }
}