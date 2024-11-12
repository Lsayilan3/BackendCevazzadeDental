
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.HekimDetails.Queries
{
    public class GetHekimDetailQuery : IRequest<IDataResult<HekimDetail>>
    {
        public int HekimDetailId { get; set; }

        public class GetHekimDetailQueryHandler : IRequestHandler<GetHekimDetailQuery, IDataResult<HekimDetail>>
        {
            private readonly IHekimDetailRepository _hekimDetailRepository;
            private readonly IMediator _mediator;

            public GetHekimDetailQueryHandler(IHekimDetailRepository hekimDetailRepository, IMediator mediator)
            {
                _hekimDetailRepository = hekimDetailRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<HekimDetail>> Handle(GetHekimDetailQuery request, CancellationToken cancellationToken)
            {
                var hekimDetail = await _hekimDetailRepository.GetAsync(p => p.HekimDetailId == request.HekimDetailId);
                return new SuccessDataResult<HekimDetail>(hekimDetail);
            }
        }
    }
}
