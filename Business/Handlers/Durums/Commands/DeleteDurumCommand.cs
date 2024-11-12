
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


namespace Business.Handlers.Durums.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDurumCommand : IRequest<IResult>
    {
        public int DurumId { get; set; }

        public class DeleteDurumCommandHandler : IRequestHandler<DeleteDurumCommand, IResult>
        {
            private readonly IDurumRepository _durumRepository;
            private readonly IMediator _mediator;

            public DeleteDurumCommandHandler(IDurumRepository durumRepository, IMediator mediator)
            {
                _durumRepository = durumRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDurumCommand request, CancellationToken cancellationToken)
            {
                var durumToDelete = _durumRepository.Get(p => p.DurumId == request.DurumId);

                _durumRepository.Delete(durumToDelete);
                await _durumRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

