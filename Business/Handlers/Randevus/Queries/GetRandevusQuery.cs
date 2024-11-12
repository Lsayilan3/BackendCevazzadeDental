
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

namespace Business.Handlers.Randevus.Queries
{

    public class GetRandevusQuery : IRequest<IDataResult<IEnumerable<Randevu>>>
    {
        public class GetRandevusQueryHandler : IRequestHandler<GetRandevusQuery, IDataResult<IEnumerable<Randevu>>>
        {
            private readonly IRandevuRepository _randevuRepository;
            private readonly IMediator _mediator;

            public GetRandevusQueryHandler(IRandevuRepository randevuRepository, IMediator mediator)
            {
                _randevuRepository = randevuRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Randevu>>> Handle(GetRandevusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Randevu>>(await _randevuRepository.GetListAsync());
            }
        }
    }
}