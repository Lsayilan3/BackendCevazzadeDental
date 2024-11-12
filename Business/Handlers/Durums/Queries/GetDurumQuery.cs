
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Durums.Queries
{
    public class GetDurumQuery : IRequest<IDataResult<Durum>>
    {
        public int DurumId { get; set; }

        public class GetDurumQueryHandler : IRequestHandler<GetDurumQuery, IDataResult<Durum>>
        {
            private readonly IDurumRepository _durumRepository;
            private readonly IMediator _mediator;

            public GetDurumQueryHandler(IDurumRepository durumRepository, IMediator mediator)
            {
                _durumRepository = durumRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Durum>> Handle(GetDurumQuery request, CancellationToken cancellationToken)
            {
                var durum = await _durumRepository.GetAsync(p => p.DurumId == request.DurumId);
                return new SuccessDataResult<Durum>(durum);
            }
        }
    }
}
