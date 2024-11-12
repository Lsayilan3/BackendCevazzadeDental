
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Hekims.Queries
{
    public class GetHekimQuery : IRequest<IDataResult<Hekim>>
    {
        public int HekimId { get; set; }

        public class GetHekimQueryHandler : IRequestHandler<GetHekimQuery, IDataResult<Hekim>>
        {
            private readonly IHekimRepository _hekimRepository;
            private readonly IMediator _mediator;

            public GetHekimQueryHandler(IHekimRepository hekimRepository, IMediator mediator)
            {
                _hekimRepository = hekimRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Hekim>> Handle(GetHekimQuery request, CancellationToken cancellationToken)
            {
                var hekim = await _hekimRepository.GetAsync(p => p.HekimId == request.HekimId);
                return new SuccessDataResult<Hekim>(hekim);
            }
        }
    }
}
