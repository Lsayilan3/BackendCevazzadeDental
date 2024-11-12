
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Anasayfas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteAnasayfaCommand : IRequest<IResult>
    {
        public int AnasayfaId { get; set; }

        public class DeleteAnasayfaCommandHandler : IRequestHandler<DeleteAnasayfaCommand, IResult>
        {
            private readonly IAnasayfaRepository _anasayfaRepository;
            private readonly IMediator _mediator;

            public DeleteAnasayfaCommandHandler(IAnasayfaRepository anasayfaRepository, IMediator mediator)
            {
                _anasayfaRepository = anasayfaRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteAnasayfaCommand request, CancellationToken cancellationToken)
            {
                var anasayfaToDelete = _anasayfaRepository.Get(p => p.AnasayfaId == request.AnasayfaId);

                _anasayfaRepository.Delete(anasayfaToDelete);
                await _anasayfaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

