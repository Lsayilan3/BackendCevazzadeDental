
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
using Business.Handlers.HekimDetails.ValidationRules;


namespace Business.Handlers.HekimDetails.Commands
{


    public class UpdateHekimDetailCommand : IRequest<IResult>
    {
        public int HekimDetailId { get; set; }
        public int HekimId { get; set; }
        public string Photo { get; set; }
        public string Ad { get; set; }
        public string Uzmanlik { get; set; }
        public string Aciklama { get; set; }
        public string SosyalFace { get; set; }
        public string SosyalTwitter { get; set; }
        public string Sosyalingstagram { get; set; }
        public string SosyalMail { get; set; }
        public int Dil { get; set; }

        public class UpdateHekimDetailCommandHandler : IRequestHandler<UpdateHekimDetailCommand, IResult>
        {
            private readonly IHekimDetailRepository _hekimDetailRepository;
            private readonly IMediator _mediator;

            public UpdateHekimDetailCommandHandler(IHekimDetailRepository hekimDetailRepository, IMediator mediator)
            {
                _hekimDetailRepository = hekimDetailRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateHekimDetailValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateHekimDetailCommand request, CancellationToken cancellationToken)
            {
                var isThereHekimDetailRecord = await _hekimDetailRepository.GetAsync(u => u.HekimDetailId == request.HekimDetailId);


                isThereHekimDetailRecord.HekimId = request.HekimId;
                isThereHekimDetailRecord.Photo = request.Photo;
                isThereHekimDetailRecord.Ad = request.Ad;
                isThereHekimDetailRecord.Uzmanlik = request.Uzmanlik;
                isThereHekimDetailRecord.Aciklama = request.Aciklama;
                isThereHekimDetailRecord.SosyalFace = request.SosyalFace;
                isThereHekimDetailRecord.SosyalTwitter = request.SosyalTwitter;
                isThereHekimDetailRecord.Sosyalingstagram = request.Sosyalingstagram;
                isThereHekimDetailRecord.SosyalMail = request.SosyalMail;
                isThereHekimDetailRecord.Dil = request.Dil;


                _hekimDetailRepository.Update(isThereHekimDetailRecord);
                await _hekimDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

