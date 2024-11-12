
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


namespace Business.Handlers.Hekims.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteHekimCommand : IRequest<IResult>
    {
        public int HekimId { get; set; }

        public class DeleteHekimCommandHandler : IRequestHandler<DeleteHekimCommand, IResult>
        {
            private readonly IHekimRepository _hekimRepository;
            private readonly IMediator _mediator;

            public DeleteHekimCommandHandler(IHekimRepository hekimRepository, IMediator mediator)
            {
                _hekimRepository = hekimRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteHekimCommand request, CancellationToken cancellationToken)
            {
                var hekimToDelete = _hekimRepository.Get(p => p.HekimId == request.HekimId);

                _hekimRepository.Delete(hekimToDelete);
                await _hekimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

