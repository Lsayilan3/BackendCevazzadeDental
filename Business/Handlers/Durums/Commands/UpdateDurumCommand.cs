
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
using Business.Handlers.Durums.ValidationRules;


namespace Business.Handlers.Durums.Commands
{


    public class UpdateDurumCommand : IRequest<IResult>
    {
        public int DurumId { get; set; }
        public string Baslik { get; set; }
        public string ParagrafOne { get; set; }
        public string ParagrafTwo { get; set; }
        public string Photo { get; set; }
        public int Dil { get; set; }
        public class UpdateDurumCommandHandler : IRequestHandler<UpdateDurumCommand, IResult>
        {
            private readonly IDurumRepository _durumRepository;
            private readonly IMediator _mediator;

            public UpdateDurumCommandHandler(IDurumRepository durumRepository, IMediator mediator)
            {
                _durumRepository = durumRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDurumValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDurumCommand request, CancellationToken cancellationToken)
            {
                var isThereDurumRecord = await _durumRepository.GetAsync(u => u.DurumId == request.DurumId);


                isThereDurumRecord.Baslik = request.Baslik;
                isThereDurumRecord.ParagrafOne = request.ParagrafOne;
                isThereDurumRecord.ParagrafTwo = request.ParagrafTwo;
                isThereDurumRecord.Photo = request.Photo;
                isThereDurumRecord.Dil = request.Dil;


                _durumRepository.Update(isThereDurumRecord);
                await _durumRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

