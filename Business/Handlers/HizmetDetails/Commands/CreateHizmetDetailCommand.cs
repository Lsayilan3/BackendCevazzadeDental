
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
using Business.Handlers.HizmetDetails.ValidationRules;

namespace Business.Handlers.HizmetDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateHizmetDetailCommand : IRequest<IResult>
    {

        public int HizmetId { get; set; }
        public string PhotoService { get; set; }
        public string Photo { get; set; }
        public string Editor { get; set; }
        public int Dil { get; set; }


        public class CreateHizmetDetailCommandHandler : IRequestHandler<CreateHizmetDetailCommand, IResult>
        {
            private readonly IHizmetDetailRepository _hizmetDetailRepository;
            private readonly IMediator _mediator;
            public CreateHizmetDetailCommandHandler(IHizmetDetailRepository hizmetDetailRepository, IMediator mediator)
            {
                _hizmetDetailRepository = hizmetDetailRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateHizmetDetailValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateHizmetDetailCommand request, CancellationToken cancellationToken)
            {
                //var isThereHizmetDetailRecord = _hizmetDetailRepository.Query().Any(u => u.HizmetId == request.HizmetId);

                //if (isThereHizmetDetailRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedHizmetDetail = new HizmetDetail
                {
                    HizmetId = request.HizmetId,
                    PhotoService = request.PhotoService,
                    Photo = request.Photo,
                    Editor = request.Editor,
                    Dil = request.Dil,
                };

                _hizmetDetailRepository.Add(addedHizmetDetail);
                await _hizmetDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}