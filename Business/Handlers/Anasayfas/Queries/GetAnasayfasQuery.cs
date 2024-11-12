
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

namespace Business.Handlers.Anasayfas.Queries
{

    public class GetAnasayfasQuery : IRequest<IDataResult<IEnumerable<Anasayfa>>>
    {
        public int Dil { get; set; }
        public class GetAnasayfasQueryHandler : IRequestHandler<GetAnasayfasQuery, IDataResult<IEnumerable<Anasayfa>>>
        {
            private readonly IAnasayfaRepository _anasayfaRepository;
            private readonly IMediator _mediator;

            public GetAnasayfasQueryHandler(IAnasayfaRepository anasayfaRepository, IMediator mediator)
            {
                _anasayfaRepository = anasayfaRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Anasayfa>>> Handle(GetAnasayfasQuery request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<Anasayfa>>(await _anasayfaRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Anasayfa>>(await _anasayfaRepository.GetListAsync(x => x.Dil == request.Dil));
            }


        }
    }
}

//public async Task<IDataResult<IEnumerable<Anasayfa>>> Handle(GetAnasayfasQuery request, CancellationToken cancellationToken)
//{
//    return new SuccessDataResult<IEnumerable<Anasayfa>>(await _anasayfaRepository.GetListAsync(x => x.Dil == request.Dil));
//}