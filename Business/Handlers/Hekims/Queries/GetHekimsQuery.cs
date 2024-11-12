
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

namespace Business.Handlers.Hekims.Queries
{

    public class GetHekimsQuery : IRequest<IDataResult<IEnumerable<Hekim>>>
    {
        public int Dil { get; set; }
        public class GetHekimsQueryHandler : IRequestHandler<GetHekimsQuery, IDataResult<IEnumerable<Hekim>>>
        {
            private readonly IHekimRepository _hekimRepository;
            private readonly IMediator _mediator;

            public GetHekimsQueryHandler(IHekimRepository hekimRepository, IMediator mediator)
            {
                _hekimRepository = hekimRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Hekim>>> Handle(GetHekimsQuery request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<Hekim>>(await _hekimRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Hekim>>(await _hekimRepository.GetListAsync(x => x.Dil == request.Dil));
            }
        }
    }
}