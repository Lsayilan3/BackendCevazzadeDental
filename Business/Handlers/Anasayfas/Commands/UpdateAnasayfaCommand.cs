
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
using Business.Handlers.Anasayfas.ValidationRules;


namespace Business.Handlers.Anasayfas.Commands
{


    public class UpdateAnasayfaCommand : IRequest<IResult>
    {
        public int AnasayfaId { get; set; }
        public string Baslik { get; set; }
        public string BaslikYazi { get; set; }
        public string Aciklama { get; set; }
        public string Photo { get; set; }
        public int Dil { get; set; }

        public class UpdateAnasayfaCommandHandler : IRequestHandler<UpdateAnasayfaCommand, IResult>
        {
            private readonly IAnasayfaRepository _anasayfaRepository;
            private readonly IMediator _mediator;

            public UpdateAnasayfaCommandHandler(IAnasayfaRepository anasayfaRepository, IMediator mediator)
            {
                _anasayfaRepository = anasayfaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateAnasayfaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateAnasayfaCommand request, CancellationToken cancellationToken)
            {
                var isThereAnasayfaRecord = await _anasayfaRepository.GetAsync(u => u.AnasayfaId == request.AnasayfaId);


                isThereAnasayfaRecord.Baslik = request.Baslik;
                isThereAnasayfaRecord.BaslikYazi = request.BaslikYazi;
                isThereAnasayfaRecord.Aciklama = request.Aciklama;
                isThereAnasayfaRecord.Photo = request.Photo;
                isThereAnasayfaRecord.Dil = request.Dil;


                _anasayfaRepository.Update(isThereAnasayfaRecord);
                await _anasayfaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

