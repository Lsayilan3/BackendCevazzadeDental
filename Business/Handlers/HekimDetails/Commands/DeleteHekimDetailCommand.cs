
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


namespace Business.Handlers.HekimDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteHekimDetailCommand : IRequest<IResult>
    {
        public int HekimDetailId { get; set; }

        public class DeleteHekimDetailCommandHandler : IRequestHandler<DeleteHekimDetailCommand, IResult>
        {
            private readonly IHekimDetailRepository _hekimDetailRepository;
            private readonly IMediator _mediator;

            public DeleteHekimDetailCommandHandler(IHekimDetailRepository hekimDetailRepository, IMediator mediator)
            {
                _hekimDetailRepository = hekimDetailRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteHekimDetailCommand request, CancellationToken cancellationToken)
            {
                var hekimDetailToDelete = _hekimDetailRepository.Get(p => p.HekimDetailId == request.HekimDetailId);

                _hekimDetailRepository.Delete(hekimDetailToDelete);
                await _hekimDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

