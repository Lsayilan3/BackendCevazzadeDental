
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
using Business.Handlers.Hizmets.ValidationRules;

namespace Business.Handlers.Hizmets.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateHizmetCommand : IRequest<IResult>
    {

        public string Photo { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }


        public class CreateHizmetCommandHandler : IRequestHandler<CreateHizmetCommand, IResult>
        {
            private readonly IHizmetRepository _hizmetRepository;
            private readonly IMediator _mediator;
            public CreateHizmetCommandHandler(IHizmetRepository hizmetRepository, IMediator mediator)
            {
                _hizmetRepository = hizmetRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateHizmetValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateHizmetCommand request, CancellationToken cancellationToken)
            {
                //var isThereHizmetRecord = _hizmetRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereHizmetRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedHizmet = new Hizmet
                {
                    Photo = request.Photo,
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    Sira = request.Sira,
                    Dil = request.Dil,

                };

                _hizmetRepository.Add(addedHizmet);
                await _hizmetRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}