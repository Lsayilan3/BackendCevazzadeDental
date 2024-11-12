
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
using Business.Handlers.HekimDetails.ValidationRules;

namespace Business.Handlers.HekimDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateHekimDetailCommand : IRequest<IResult>
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


        public class CreateHekimDetailCommandHandler : IRequestHandler<CreateHekimDetailCommand, IResult>
        {
            private readonly IHekimDetailRepository _hekimDetailRepository;
            private readonly IMediator _mediator;
            public CreateHekimDetailCommandHandler(IHekimDetailRepository hekimDetailRepository, IMediator mediator)
            {
                _hekimDetailRepository = hekimDetailRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateHekimDetailValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateHekimDetailCommand request, CancellationToken cancellationToken)
            {
                //var isThereHekimDetailRecord = _hekimDetailRepository.Query().Any(u => u.HekimId == request.HekimId);

                //if (isThereHekimDetailRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedHekimDetail = new HekimDetail
                {
                    HekimId = request.HekimId,
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

                _hekimDetailRepository.Add(addedHekimDetail);
                await _hekimDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}