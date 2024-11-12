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

namespace Business.Handlers.HekimDetails.Queries
{

    public class GetHekimDetailListByHekimId : IRequest<IDataResult<IEnumerable<HekimDetail>>>
    {
        public int HekimId { get; set; }
        public int Dil { get; set; }

        public class GetHekimDetailsQueryHandler : IRequestHandler<GetHekimDetailListByHekimId, IDataResult<IEnumerable<HekimDetail>>>
        {
            private readonly IHekimDetailRepository _hekimDetailRepository;
            private readonly IMediator _mediator;

            public GetHekimDetailsQueryHandler(IHekimDetailRepository hekimDetailRepository, IMediator mediator)
            {
                _hekimDetailRepository = hekimDetailRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<HekimDetail>>> Handle(GetHekimDetailListByHekimId request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<HekimDetail>>(await _hekimDetailRepository.GetListAsync(x => x.HekimId == request.HekimId));
                }
                return new SuccessDataResult<IEnumerable<HekimDetail>>(await _hekimDetailRepository.GetListAsync(x => x.HekimId == request.HekimId && x.Dil == request.Dil));
            }
        }
    }
}
