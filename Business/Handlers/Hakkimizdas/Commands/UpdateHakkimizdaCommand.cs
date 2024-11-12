
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
using Business.Handlers.Hakkimizdas.ValidationRules;


namespace Business.Handlers.Hakkimizdas.Commands
{


    public class UpdateHakkimizdaCommand : IRequest<IResult>
    {
        public int HakkimizdaId { get; set; }
        public string Photo { get; set; }
        public string Editor { get; set; }
        public int Dil { get; set; }

        public class UpdateHakkimizdaCommandHandler : IRequestHandler<UpdateHakkimizdaCommand, IResult>
        {
            private readonly IHakkimizdaRepository _hakkimizdaRepository;
            private readonly IMediator _mediator;

            public UpdateHakkimizdaCommandHandler(IHakkimizdaRepository hakkimizdaRepository, IMediator mediator)
            {
                _hakkimizdaRepository = hakkimizdaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateHakkimizdaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateHakkimizdaCommand request, CancellationToken cancellationToken)
            {
                var isThereHakkimizdaRecord = await _hakkimizdaRepository.GetAsync(u => u.HakkimizdaId == request.HakkimizdaId);


                isThereHakkimizdaRecord.Photo = request.Photo;
                isThereHakkimizdaRecord.Editor = request.Editor;
                isThereHakkimizdaRecord.Dil = request.Dil;


                _hakkimizdaRepository.Update(isThereHakkimizdaRecord);
                await _hakkimizdaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

