
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
using Business.Handlers.Hekims.ValidationRules;

namespace Business.Handlers.Hekims.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateHekimCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Ad { get; set; }
        public string Uzmanlik { get; set; }
        public string Aciklama { get; set; }
        public string SosyalFace { get; set; }
        public string SosyalTwitter { get; set; }
        public string Sosyalingstagram { get; set; }
        public string SosyalMail { get; set; }
        public int Dil { get; set; }


        public class CreateHekimCommandHandler : IRequestHandler<CreateHekimCommand, IResult>
        {
            private readonly IHekimRepository _hekimRepository;
            private readonly IMediator _mediator;
            public CreateHekimCommandHandler(IHekimRepository hekimRepository, IMediator mediator)
            {
                _hekimRepository = hekimRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateHekimValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateHekimCommand request, CancellationToken cancellationToken)
            {
                //var isThereHekimRecord = _hekimRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereHekimRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedHekim = new Hekim
                {
                    Photo = request.Photo,
                    Ad = request.Ad,
                    Uzmanlik = request.Uzmanlik,
                    Aciklama = request.Aciklama,
                    SosyalFace = request.SosyalFace,
                    SosyalTwitter = request.SosyalTwitter,
                    Sosyalingstagram = request.Sosyalingstagram,
                    SosyalMail = request.SosyalMail,
                    Dil = request.Dil,

                };

                _hekimRepository.Add(addedHekim);
                await _hekimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}