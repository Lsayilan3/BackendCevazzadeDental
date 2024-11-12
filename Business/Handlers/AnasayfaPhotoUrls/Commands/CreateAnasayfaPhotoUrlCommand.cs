
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.AnasayfaPhotoUrls.ValidationRules;

namespace Business.Handlers.AnasayfaPhotoUrls.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateAnasayfaPhotoUrlCommand : IRequest<IResult>
    {

        public string Photo { get; set; }


        public class CreateAnasayfaPhotoUrlCommandHandler : IRequestHandler<CreateAnasayfaPhotoUrlCommand, IResult>
        {
            private readonly IAnasayfaPhotoUrlRepository _anasayfaPhotoUrlRepository;
            private readonly IMediator _mediator;
            public CreateAnasayfaPhotoUrlCommandHandler(IAnasayfaPhotoUrlRepository anasayfaPhotoUrlRepository, IMediator mediator)
            {
                _anasayfaPhotoUrlRepository = anasayfaPhotoUrlRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateAnasayfaPhotoUrlValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAnasayfaPhotoUrlCommand request, CancellationToken cancellationToken)
            {
                var isThereAnasayfaPhotoUrlRecord = _anasayfaPhotoUrlRepository.Query().Any(u => u.Photo == request.Photo);

                if (isThereAnasayfaPhotoUrlRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedAnasayfaPhotoUrl = new AnasayfaPhotoUrl
                {
                    Photo = request.Photo,

                };

                _anasayfaPhotoUrlRepository.Add(addedAnasayfaPhotoUrl);
                await _anasayfaPhotoUrlRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}