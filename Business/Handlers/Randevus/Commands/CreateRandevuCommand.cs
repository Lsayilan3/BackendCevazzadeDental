
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
using Business.Handlers.Randevus.ValidationRules;

namespace Business.Handlers.Randevus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRandevuCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Tel { get; set; }
        public int HizmetlerimizId { get; set; }
        public System.DateTime Tarih { get; set; }
        public System.DateTime CraeteDate { get; set; }
        public string Mesaj { get; set; }


        public class CreateRandevuCommandHandler : IRequestHandler<CreateRandevuCommand, IResult>
        {
            private readonly IRandevuRepository _randevuRepository;
            private readonly IMediator _mediator;
            public CreateRandevuCommandHandler(IRandevuRepository randevuRepository, IMediator mediator)
            {
                _randevuRepository = randevuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRandevuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRandevuCommand request, CancellationToken cancellationToken)
            {
                //var isThereRandevuRecord = _randevuRepository.Query().Any(u => u.Ad == request.Ad);

                //if (isThereRandevuRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRandevu = new Randevu
                {
                    Ad = request.Ad,
                    Soyad = request.Soyad,
                    Tel = request.Tel,
                    HizmetlerimizId = request.HizmetlerimizId,
                    Tarih = request.Tarih,
                    CraeteDate = System.DateTime.UtcNow,
                    Mesaj = request.Mesaj,

                };

                _randevuRepository.Add(addedRandevu);
                await _randevuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}