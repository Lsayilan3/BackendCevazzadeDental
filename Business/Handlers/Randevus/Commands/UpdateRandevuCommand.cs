
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
using Business.Handlers.Randevus.ValidationRules;


namespace Business.Handlers.Randevus.Commands
{


    public class UpdateRandevuCommand : IRequest<IResult>
    {
        public int RandevuId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Tel { get; set; }
        public int HizmetlerimizId { get; set; }
        public System.DateTime Tarih { get; set; }
        public System.DateTime CraeteDate { get; set; }
        public string Mesaj { get; set; }

        public class UpdateRandevuCommandHandler : IRequestHandler<UpdateRandevuCommand, IResult>
        {
            private readonly IRandevuRepository _randevuRepository;
            private readonly IMediator _mediator;

            public UpdateRandevuCommandHandler(IRandevuRepository randevuRepository, IMediator mediator)
            {
                _randevuRepository = randevuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRandevuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRandevuCommand request, CancellationToken cancellationToken)
            {
                var isThereRandevuRecord = await _randevuRepository.GetAsync(u => u.RandevuId == request.RandevuId);


                isThereRandevuRecord.Ad = request.Ad;
                isThereRandevuRecord.Soyad = request.Soyad;
                isThereRandevuRecord.Tel = request.Tel;
                isThereRandevuRecord.HizmetlerimizId = request.HizmetlerimizId;
                isThereRandevuRecord.Tarih = request.Tarih;
                isThereRandevuRecord.CraeteDate = request.CraeteDate;
                isThereRandevuRecord.Mesaj = request.Mesaj;


                _randevuRepository.Update(isThereRandevuRecord);
                await _randevuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

