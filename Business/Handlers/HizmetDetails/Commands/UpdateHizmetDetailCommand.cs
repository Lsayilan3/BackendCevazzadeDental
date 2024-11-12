
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
using Business.Handlers.HizmetDetails.ValidationRules;


namespace Business.Handlers.HizmetDetails.Commands
{


    public class UpdateHizmetDetailCommand : IRequest<IResult>
    {
        public int HizmetDetailId { get; set; }
        public int HizmetId { get; set; }
        public string PhotoService { get; set; }
        public string Photo { get; set; }
        public string Editor { get; set; }
        public int Dil { get; set; }

        public class UpdateHizmetDetailCommandHandler : IRequestHandler<UpdateHizmetDetailCommand, IResult>
        {
            private readonly IHizmetDetailRepository _hizmetDetailRepository;
            private readonly IMediator _mediator;

            public UpdateHizmetDetailCommandHandler(IHizmetDetailRepository hizmetDetailRepository, IMediator mediator)
            {
                _hizmetDetailRepository = hizmetDetailRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateHizmetDetailValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateHizmetDetailCommand request, CancellationToken cancellationToken)
            {
                var isThereHizmetDetailRecord = await _hizmetDetailRepository.GetAsync(u => u.HizmetDetailId == request.HizmetDetailId);


                isThereHizmetDetailRecord.HizmetId = request.HizmetId;
                isThereHizmetDetailRecord.PhotoService = request.PhotoService;
                isThereHizmetDetailRecord.Photo = request.Photo;
                isThereHizmetDetailRecord.Editor = request.Editor;
                isThereHizmetDetailRecord.Dil = request.Dil;


                _hizmetDetailRepository.Update(isThereHizmetDetailRecord);
                await _hizmetDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

