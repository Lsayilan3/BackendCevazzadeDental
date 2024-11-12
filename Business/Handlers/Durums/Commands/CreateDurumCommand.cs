
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
using Business.Handlers.Durums.ValidationRules;

namespace Business.Handlers.Durums.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDurumCommand : IRequest<IResult>
    {

        public string Baslik { get; set; }
        public string ParagrafOne { get; set; }
        public string ParagrafTwo { get; set; }
        public string Photo { get; set; }
        public int Dil { get; set; }


        public class CreateDurumCommandHandler : IRequestHandler<CreateDurumCommand, IResult>
        {
            private readonly IDurumRepository _durumRepository;
            private readonly IMediator _mediator;
            public CreateDurumCommandHandler(IDurumRepository durumRepository, IMediator mediator)
            {
                _durumRepository = durumRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDurumValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDurumCommand request, CancellationToken cancellationToken)
            {
                //var isThereDurumRecord = _durumRepository.Query().Any(u => u.Baslik == request.Baslik);

                //if (isThereDurumRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDurum = new Durum
                {
                    Baslik = request.Baslik,
                    ParagrafOne = request.ParagrafOne,
                    ParagrafTwo = request.ParagrafTwo,
                    Photo = request.Photo,
                    Dil = request.Dil,

                };

                _durumRepository.Add(addedDurum);
                await _durumRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}