
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
using Business.Handlers.Hizmets.ValidationRules;


namespace Business.Handlers.Hizmets.Commands
{


    public class UpdateHizmetCommand : IRequest<IResult>
    {
        public int HizmetId { get; set; }
        public string Photo { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }

        public class UpdateHizmetCommandHandler : IRequestHandler<UpdateHizmetCommand, IResult>
        {
            private readonly IHizmetRepository _hizmetRepository;
            private readonly IMediator _mediator;

            public UpdateHizmetCommandHandler(IHizmetRepository hizmetRepository, IMediator mediator)
            {
                _hizmetRepository = hizmetRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateHizmetValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateHizmetCommand request, CancellationToken cancellationToken)
            {
                var isThereHizmetRecord = await _hizmetRepository.GetAsync(u => u.HizmetId == request.HizmetId);


                isThereHizmetRecord.Photo = request.Photo;
                isThereHizmetRecord.Baslik = request.Baslik;
                isThereHizmetRecord.Aciklama = request.Aciklama;
                isThereHizmetRecord.Sira = request.Sira;
                isThereHizmetRecord.Dil = request.Dil;


                _hizmetRepository.Update(isThereHizmetRecord);
                await _hizmetRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

