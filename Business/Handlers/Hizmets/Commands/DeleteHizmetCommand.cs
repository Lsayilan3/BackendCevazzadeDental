
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


namespace Business.Handlers.Hizmets.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteHizmetCommand : IRequest<IResult>
    {
        public int HizmetId { get; set; }

        public class DeleteHizmetCommandHandler : IRequestHandler<DeleteHizmetCommand, IResult>
        {
            private readonly IHizmetRepository _hizmetRepository;
            private readonly IMediator _mediator;

            public DeleteHizmetCommandHandler(IHizmetRepository hizmetRepository, IMediator mediator)
            {
                _hizmetRepository = hizmetRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteHizmetCommand request, CancellationToken cancellationToken)
            {
                var hizmetToDelete = _hizmetRepository.Get(p => p.HizmetId == request.HizmetId);

                _hizmetRepository.Delete(hizmetToDelete);
                await _hizmetRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

