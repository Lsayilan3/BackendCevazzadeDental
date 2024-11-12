
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
using Business.Handlers.Hakkimizdas.ValidationRules;

namespace Business.Handlers.Hakkimizdas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateHakkimizdaCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Editor { get; set; }
        public int Dil { get; set; }


        public class CreateHakkimizdaCommandHandler : IRequestHandler<CreateHakkimizdaCommand, IResult>
        {
            private readonly IHakkimizdaRepository _hakkimizdaRepository;
            private readonly IMediator _mediator;
            public CreateHakkimizdaCommandHandler(IHakkimizdaRepository hakkimizdaRepository, IMediator mediator)
            {
                _hakkimizdaRepository = hakkimizdaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateHakkimizdaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateHakkimizdaCommand request, CancellationToken cancellationToken)
            {
                //var isThereHakkimizdaRecord = _hakkimizdaRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereHakkimizdaRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedHakkimizda = new Hakkimizda
                {
                    Photo = request.Photo,
                    Editor = request.Editor,
                    Dil = request.Dil,

                };

                _hakkimizdaRepository.Add(addedHakkimizda);
                await _hakkimizdaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}