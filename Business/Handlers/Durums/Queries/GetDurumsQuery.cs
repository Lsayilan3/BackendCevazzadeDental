
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

namespace Business.Handlers.Durums.Queries
{

    public class GetDurumsQuery : IRequest<IDataResult<IEnumerable<Durum>>>
    {
        public int Dil { get; set; }
        public class GetDurumsQueryHandler : IRequestHandler<GetDurumsQuery, IDataResult<IEnumerable<Durum>>>
        {
            private readonly IDurumRepository _durumRepository;
            private readonly IMediator _mediator;

            public GetDurumsQueryHandler(IDurumRepository durumRepository, IMediator mediator)
            {
                _durumRepository = durumRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Durum>>> Handle(GetDurumsQuery request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<Durum>>(await _durumRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Durum>>(await _durumRepository.GetListAsync(x => x.Dil == request.Dil));
            }
        }
    }
}