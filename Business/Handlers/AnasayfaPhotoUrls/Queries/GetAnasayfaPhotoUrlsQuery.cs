
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.AnasayfaPhotoUrls.Queries
{

    public class GetAnasayfaPhotoUrlsQuery : IRequest<IDataResult<IEnumerable<AnasayfaPhotoUrl>>>
    {
        public class GetAnasayfaPhotoUrlsQueryHandler : IRequestHandler<GetAnasayfaPhotoUrlsQuery, IDataResult<IEnumerable<AnasayfaPhotoUrl>>>
        {
            private readonly IAnasayfaPhotoUrlRepository _anasayfaPhotoUrlRepository;
            private readonly IMediator _mediator;

            public GetAnasayfaPhotoUrlsQueryHandler(IAnasayfaPhotoUrlRepository anasayfaPhotoUrlRepository, IMediator mediator)
            {
                _anasayfaPhotoUrlRepository = anasayfaPhotoUrlRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<AnasayfaPhotoUrl>>> Handle(GetAnasayfaPhotoUrlsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<AnasayfaPhotoUrl>>(await _anasayfaPhotoUrlRepository.GetListAsync());
            }
        }
    }
}