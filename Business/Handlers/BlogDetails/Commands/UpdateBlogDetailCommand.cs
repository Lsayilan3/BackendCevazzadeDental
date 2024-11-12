
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
using Business.Handlers.BlogDetails.ValidationRules;


namespace Business.Handlers.BlogDetails.Commands
{


    public class UpdateBlogDetailCommand : IRequest<IResult>
    {
        public int BlogDetailId { get; set; }
        public int BlogId { get; set; }
        public string Tarih { get; set; }
        public string Aciklayan { get; set; }
        public string Editor { get; set; }
        public string Photo { get; set; }
        public int Dil { get; set; }

        public class UpdateBlogDetailCommandHandler : IRequestHandler<UpdateBlogDetailCommand, IResult>
        {
            private readonly IBlogDetailRepository _blogDetailRepository;
            private readonly IMediator _mediator;

            public UpdateBlogDetailCommandHandler(IBlogDetailRepository blogDetailRepository, IMediator mediator)
            {
                _blogDetailRepository = blogDetailRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBlogDetailValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBlogDetailCommand request, CancellationToken cancellationToken)
            {
                var isThereBlogDetailRecord = await _blogDetailRepository.GetAsync(u => u.BlogDetailId == request.BlogDetailId);


                isThereBlogDetailRecord.BlogId = request.BlogId;
                isThereBlogDetailRecord.Tarih = request.Tarih;
                isThereBlogDetailRecord.Aciklayan = request.Aciklayan;
                isThereBlogDetailRecord.Editor = request.Editor;
                isThereBlogDetailRecord.Photo = request.Photo;
                isThereBlogDetailRecord.Dil = request.Dil;


                _blogDetailRepository.Update(isThereBlogDetailRecord);
                await _blogDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

