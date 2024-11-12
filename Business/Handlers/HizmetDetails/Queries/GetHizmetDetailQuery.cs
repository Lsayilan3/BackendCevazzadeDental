
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.HizmetDetails.Queries
{
    public class GetHizmetDetailQuery : IRequest<IDataResult<HizmetDetail>>
    {
        public int HizmetDetailId { get; set; }

        public class GetHizmetDetailQueryHandler : IRequestHandler<GetHizmetDetailQuery, IDataResult<HizmetDetail>>
        {
            private readonly IHizmetDetailRepository _hizmetDetailRepository;
            private readonly IMediator _mediator;

            public GetHizmetDetailQueryHandler(IHizmetDetailRepository hizmetDetailRepository, IMediator mediator)
            {
                _hizmetDetailRepository = hizmetDetailRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<HizmetDetail>> Handle(GetHizmetDetailQuery request, CancellationToken cancellationToken)
            {
                var hizmetDetail = await _hizmetDetailRepository.GetAsync(p => p.HizmetDetailId == request.HizmetDetailId);
                return new SuccessDataResult<HizmetDetail>(hizmetDetail);
            }
        }
    }
}
