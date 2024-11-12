
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
using Business.Handlers.Hekims.ValidationRules;


namespace Business.Handlers.Hekims.Commands
{


    public class UpdateHekimCommand : IRequest<IResult>
    {
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

        public class UpdateHekimCommandHandler : IRequestHandler<UpdateHekimCommand, IResult>
        {
            private readonly IHekimRepository _hekimRepository;
            private readonly IMediator _mediator;

            public UpdateHekimCommandHandler(IHekimRepository hekimRepository, IMediator mediator)
            {
                _hekimRepository = hekimRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateHekimValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateHekimCommand request, CancellationToken cancellationToken)
            {
                var isThereHekimRecord = await _hekimRepository.GetAsync(u => u.HekimId == request.HekimId);


                isThereHekimRecord.Photo = request.Photo;
                isThereHekimRecord.Ad = request.Ad;
                isThereHekimRecord.Uzmanlik = request.Uzmanlik;
                isThereHekimRecord.Aciklama = request.Aciklama;
                isThereHekimRecord.SosyalFace = request.SosyalFace;
                isThereHekimRecord.SosyalTwitter = request.SosyalTwitter;
                isThereHekimRecord.Sosyalingstagram = request.Sosyalingstagram;
                isThereHekimRecord.SosyalMail = request.SosyalMail;
                isThereHekimRecord.Dil = request.Dil;


                _hekimRepository.Update(isThereHekimRecord);
                await _hekimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

