
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


namespace Business.Handlers.HizmetDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteHizmetDetailCommand : IRequest<IResult>
    {
        public int HizmetDetailId { get; set; }

        public class DeleteHizmetDetailCommandHandler : IRequestHandler<DeleteHizmetDetailCommand, IResult>
        {
            private readonly IHizmetDetailRepository _hizmetDetailRepository;
            private readonly IMediator _mediator;

            public DeleteHizmetDetailCommandHandler(IHizmetDetailRepository hizmetDetailRepository, IMediator mediator)
            {
                _hizmetDetailRepository = hizmetDetailRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteHizmetDetailCommand request, CancellationToken cancellationToken)
            {
                var hizmetDetailToDelete = _hizmetDetailRepository.Get(p => p.HizmetDetailId == request.HizmetDetailId);

                _hizmetDetailRepository.Delete(hizmetDetailToDelete);
                await _hizmetDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

