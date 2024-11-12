
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.AnasayfaPhotoUrls.Queries
{
    public class GetAnasayfaPhotoUrlQuery : IRequest<IDataResult<AnasayfaPhotoUrl>>
    {
        public int AnasayfaPhotoUrlId { get; set; }

        public class GetAnasayfaPhotoUrlQueryHandler : IRequestHandler<GetAnasayfaPhotoUrlQuery, IDataResult<AnasayfaPhotoUrl>>
        {
            private readonly IAnasayfaPhotoUrlRepository _anasayfaPhotoUrlRepository;
            private readonly IMediator _mediator;

            public GetAnasayfaPhotoUrlQueryHandler(IAnasayfaPhotoUrlRepository anasayfaPhotoUrlRepository, IMediator mediator)
            {
                _anasayfaPhotoUrlRepository = anasayfaPhotoUrlRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<AnasayfaPhotoUrl>> Handle(GetAnasayfaPhotoUrlQuery request, CancellationToken cancellationToken)
            {
                var anasayfaPhotoUrl = await _anasayfaPhotoUrlRepository.GetAsync(p => p.AnasayfaPhotoUrlId == request.AnasayfaPhotoUrlId);
                return new SuccessDataResult<AnasayfaPhotoUrl>(anasayfaPhotoUrl);
            }
        }
    }
}
