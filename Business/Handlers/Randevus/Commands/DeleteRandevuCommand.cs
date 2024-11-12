
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


namespace Business.Handlers.Randevus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRandevuCommand : IRequest<IResult>
    {
        public int RandevuId { get; set; }

        public class DeleteRandevuCommandHandler : IRequestHandler<DeleteRandevuCommand, IResult>
        {
            private readonly IRandevuRepository _randevuRepository;
            private readonly IMediator _mediator;

            public DeleteRandevuCommandHandler(IRandevuRepository randevuRepository, IMediator mediator)
            {
                _randevuRepository = randevuRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRandevuCommand request, CancellationToken cancellationToken)
            {
                var randevuToDelete = _randevuRepository.Get(p => p.RandevuId == request.RandevuId);

                _randevuRepository.Delete(randevuToDelete);
                await _randevuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

