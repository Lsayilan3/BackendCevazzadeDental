
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Hizmets.Queries
{
    public class GetHizmetQuery : IRequest<IDataResult<Hizmet>>
    {
        public int HizmetId { get; set; }

        public class GetHizmetQueryHandler : IRequestHandler<GetHizmetQuery, IDataResult<Hizmet>>
        {
            private readonly IHizmetRepository _hizmetRepository;
            private readonly IMediator _mediator;

            public GetHizmetQueryHandler(IHizmetRepository hizmetRepository, IMediator mediator)
            {
                _hizmetRepository = hizmetRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Hizmet>> Handle(GetHizmetQuery request, CancellationToken cancellationToken)
            {
                var hizmet = await _hizmetRepository.GetAsync(p => p.HizmetId == request.HizmetId);
                return new SuccessDataResult<Hizmet>(hizmet);
            }
        }
    }
}
