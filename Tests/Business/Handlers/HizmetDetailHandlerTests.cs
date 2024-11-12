
using Business.Handlers.HizmetDetails.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.HizmetDetails.Queries.GetHizmetDetailQuery;
using Entities.Concrete;
using static Business.Handlers.HizmetDetails.Queries.GetHizmetDetailsQuery;
using static Business.Handlers.HizmetDetails.Commands.CreateHizmetDetailCommand;
using Business.Handlers.HizmetDetails.Commands;
using Business.Constants;
using static Business.Handlers.HizmetDetails.Commands.UpdateHizmetDetailCommand;
using static Business.Handlers.HizmetDetails.Commands.DeleteHizmetDetailCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class HizmetDetailHandlerTests
    {
        Mock<IHizmetDetailRepository> _hizmetDetailRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _hizmetDetailRepository = new Mock<IHizmetDetailRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task HizmetDetail_GetQuery_Success()
        {
            //Arrange
            var query = new GetHizmetDetailQuery();

            _hizmetDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HizmetDetail, bool>>>())).ReturnsAsync(new HizmetDetail()
//propertyler buraya yazılacak
//{																		
//HizmetDetailId = 1,
//HizmetDetailName = "Test"
//}
);

            var handler = new GetHizmetDetailQueryHandler(_hizmetDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.HizmetDetailId.Should().Be(1);

        }

        [Test]
        public async Task HizmetDetail_GetQueries_Success()
        {
            //Arrange
            var query = new GetHizmetDetailsQuery();

            _hizmetDetailRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<HizmetDetail, bool>>>()))
                        .ReturnsAsync(new List<HizmetDetail> { new HizmetDetail() { /*TODO:propertyler buraya yazılacak HizmetDetailId = 1, HizmetDetailName = "test"*/ } });

            var handler = new GetHizmetDetailsQueryHandler(_hizmetDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<HizmetDetail>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task HizmetDetail_CreateCommand_Success()
        {
            HizmetDetail rt = null;
            //Arrange
            var command = new CreateHizmetDetailCommand();
            //propertyler buraya yazılacak
            //command.HizmetDetailName = "deneme";

            _hizmetDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HizmetDetail, bool>>>()))
                        .ReturnsAsync(rt);

            _hizmetDetailRepository.Setup(x => x.Add(It.IsAny<HizmetDetail>())).Returns(new HizmetDetail());

            var handler = new CreateHizmetDetailCommandHandler(_hizmetDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hizmetDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task HizmetDetail_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateHizmetDetailCommand();
            //propertyler buraya yazılacak 
            //command.HizmetDetailName = "test";

            _hizmetDetailRepository.Setup(x => x.Query())
                                           .Returns(new List<HizmetDetail> { new HizmetDetail() { /*TODO:propertyler buraya yazılacak HizmetDetailId = 1, HizmetDetailName = "test"*/ } }.AsQueryable());

            _hizmetDetailRepository.Setup(x => x.Add(It.IsAny<HizmetDetail>())).Returns(new HizmetDetail());

            var handler = new CreateHizmetDetailCommandHandler(_hizmetDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task HizmetDetail_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateHizmetDetailCommand();
            //command.HizmetDetailName = "test";

            _hizmetDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HizmetDetail, bool>>>()))
                        .ReturnsAsync(new HizmetDetail() { /*TODO:propertyler buraya yazılacak HizmetDetailId = 1, HizmetDetailName = "deneme"*/ });

            _hizmetDetailRepository.Setup(x => x.Update(It.IsAny<HizmetDetail>())).Returns(new HizmetDetail());

            var handler = new UpdateHizmetDetailCommandHandler(_hizmetDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hizmetDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task HizmetDetail_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteHizmetDetailCommand();

            _hizmetDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HizmetDetail, bool>>>()))
                        .ReturnsAsync(new HizmetDetail() { /*TODO:propertyler buraya yazılacak HizmetDetailId = 1, HizmetDetailName = "deneme"*/});

            _hizmetDetailRepository.Setup(x => x.Delete(It.IsAny<HizmetDetail>()));

            var handler = new DeleteHizmetDetailCommandHandler(_hizmetDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hizmetDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

