
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Hakkimizdas.Queries
{
    public class GetHakkimizdaQuery : IRequest<IDataResult<Hakkimizda>>
    {
        public int HakkimizdaId { get; set; }

        public class GetHakkimizdaQueryHandler : IRequestHandler<GetHakkimizdaQuery, IDataResult<Hakkimizda>>
        {
            private readonly IHakkimizdaRepository _hakkimizdaRepository;
            private readonly IMediator _mediator;

            public GetHakkimizdaQueryHandler(IHakkimizdaRepository hakkimizdaRepository, IMediator mediator)
            {
                _hakkimizdaRepository = hakkimizdaRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Hakkimizda>> Handle(GetHakkimizdaQuery request, CancellationToken cancellationToken)
            {
                var hakkimizda = await _hakkimizdaRepository.GetAsync(p => p.HakkimizdaId == request.HakkimizdaId);
                return new SuccessDataResult<Hakkimizda>(hakkimizda);
            }
        }
    }
}
