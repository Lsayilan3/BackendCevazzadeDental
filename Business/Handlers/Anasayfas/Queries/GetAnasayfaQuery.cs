
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Anasayfas.Queries
{
    public class GetAnasayfaQuery : IRequest<IDataResult<Anasayfa>>
    {
        public int AnasayfaId { get; set; }

        public class GetAnasayfaQueryHandler : IRequestHandler<GetAnasayfaQuery, IDataResult<Anasayfa>>
        {
            private readonly IAnasayfaRepository _anasayfaRepository;
            private readonly IMediator _mediator;

            public GetAnasayfaQueryHandler(IAnasayfaRepository anasayfaRepository, IMediator mediator)
            {
                _anasayfaRepository = anasayfaRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Anasayfa>> Handle(GetAnasayfaQuery request, CancellationToken cancellationToken)
            {
                var anasayfa = await _anasayfaRepository.GetAsync(p => p.AnasayfaId == request.AnasayfaId);
                return new SuccessDataResult<Anasayfa>(anasayfa);
            }
        }
    }
}
