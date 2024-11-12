
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


namespace Business.Handlers.Hakkimizdas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteHakkimizdaCommand : IRequest<IResult>
    {
        public int HakkimizdaId { get; set; }

        public class DeleteHakkimizdaCommandHandler : IRequestHandler<DeleteHakkimizdaCommand, IResult>
        {
            private readonly IHakkimizdaRepository _hakkimizdaRepository;
            private readonly IMediator _mediator;

            public DeleteHakkimizdaCommandHandler(IHakkimizdaRepository hakkimizdaRepository, IMediator mediator)
            {
                _hakkimizdaRepository = hakkimizdaRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteHakkimizdaCommand request, CancellationToken cancellationToken)
            {
                var hakkimizdaToDelete = _hakkimizdaRepository.Get(p => p.HakkimizdaId == request.HakkimizdaId);

                _hakkimizdaRepository.Delete(hakkimizdaToDelete);
                await _hakkimizdaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

