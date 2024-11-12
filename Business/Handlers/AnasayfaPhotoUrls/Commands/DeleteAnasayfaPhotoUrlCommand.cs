
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


namespace Business.Handlers.AnasayfaPhotoUrls.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteAnasayfaPhotoUrlCommand : IRequest<IResult>
    {
        public int AnasayfaPhotoUrlId { get; set; }

        public class DeleteAnasayfaPhotoUrlCommandHandler : IRequestHandler<DeleteAnasayfaPhotoUrlCommand, IResult>
        {
            private readonly IAnasayfaPhotoUrlRepository _anasayfaPhotoUrlRepository;
            private readonly IMediator _mediator;

            public DeleteAnasayfaPhotoUrlCommandHandler(IAnasayfaPhotoUrlRepository anasayfaPhotoUrlRepository, IMediator mediator)
            {
                _anasayfaPhotoUrlRepository = anasayfaPhotoUrlRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteAnasayfaPhotoUrlCommand request, CancellationToken cancellationToken)
            {
                var anasayfaPhotoUrlToDelete = _anasayfaPhotoUrlRepository.Get(p => p.AnasayfaPhotoUrlId == request.AnasayfaPhotoUrlId);

                _anasayfaPhotoUrlRepository.Delete(anasayfaPhotoUrlToDelete);
                await _anasayfaPhotoUrlRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

