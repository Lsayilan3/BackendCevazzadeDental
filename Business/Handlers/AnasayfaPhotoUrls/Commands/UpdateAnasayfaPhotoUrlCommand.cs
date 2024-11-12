
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.AnasayfaPhotoUrls.ValidationRules;


namespace Business.Handlers.AnasayfaPhotoUrls.Commands
{


    public class UpdateAnasayfaPhotoUrlCommand : IRequest<IResult>
    {
        public int AnasayfaPhotoUrlId { get; set; }
        public string Photo { get; set; }

        public class UpdateAnasayfaPhotoUrlCommandHandler : IRequestHandler<UpdateAnasayfaPhotoUrlCommand, IResult>
        {
            private readonly IAnasayfaPhotoUrlRepository _anasayfaPhotoUrlRepository;
            private readonly IMediator _mediator;

            public UpdateAnasayfaPhotoUrlCommandHandler(IAnasayfaPhotoUrlRepository anasayfaPhotoUrlRepository, IMediator mediator)
            {
                _anasayfaPhotoUrlRepository = anasayfaPhotoUrlRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateAnasayfaPhotoUrlValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateAnasayfaPhotoUrlCommand request, CancellationToken cancellationToken)
            {
                var isThereAnasayfaPhotoUrlRecord = await _anasayfaPhotoUrlRepository.GetAsync(u => u.AnasayfaPhotoUrlId == request.AnasayfaPhotoUrlId);


                isThereAnasayfaPhotoUrlRecord.Photo = request.Photo;


                _anasayfaPhotoUrlRepository.Update(isThereAnasayfaPhotoUrlRecord);
                await _anasayfaPhotoUrlRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

