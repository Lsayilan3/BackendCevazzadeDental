
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Randevus.Queries
{
    public class GetRandevuQuery : IRequest<IDataResult<Randevu>>
    {
        public int RandevuId { get; set; }

        public class GetRandevuQueryHandler : IRequestHandler<GetRandevuQuery, IDataResult<Randevu>>
        {
            private readonly IRandevuRepository _randevuRepository;
            private readonly IMediator _mediator;

            public GetRandevuQueryHandler(IRandevuRepository randevuRepository, IMediator mediator)
            {
                _randevuRepository = randevuRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Randevu>> Handle(GetRandevuQuery request, CancellationToken cancellationToken)
            {
                var randevu = await _randevuRepository.GetAsync(p => p.RandevuId == request.RandevuId);
                return new SuccessDataResult<Randevu>(randevu);
            }
        }
    }
}
