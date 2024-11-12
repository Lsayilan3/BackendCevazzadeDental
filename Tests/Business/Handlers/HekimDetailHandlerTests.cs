
using Business.Handlers.HekimDetails.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.HekimDetails.Queries.GetHekimDetailQuery;
using Entities.Concrete;
using static Business.Handlers.HekimDetails.Queries.GetHekimDetailsQuery;
using static Business.Handlers.HekimDetails.Commands.CreateHekimDetailCommand;
using Business.Handlers.HekimDetails.Commands;
using Business.Constants;
using static Business.Handlers.HekimDetails.Commands.UpdateHekimDetailCommand;
using static Business.Handlers.HekimDetails.Commands.DeleteHekimDetailCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class HekimDetailHandlerTests
    {
        Mock<IHekimDetailRepository> _hekimDetailRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _hekimDetailRepository = new Mock<IHekimDetailRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task HekimDetail_GetQuery_Success()
        {
            //Arrange
            var query = new GetHekimDetailQuery();

            _hekimDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HekimDetail, bool>>>())).ReturnsAsync(new HekimDetail()
//propertyler buraya yazılacak
//{																		
//HekimDetailId = 1,
//HekimDetailName = "Test"
//}
);

            var handler = new GetHekimDetailQueryHandler(_hekimDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.HekimDetailId.Should().Be(1);

        }

        [Test]
        public async Task HekimDetail_GetQueries_Success()
        {
            //Arrange
            var query = new GetHekimDetailsQuery();

            _hekimDetailRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<HekimDetail, bool>>>()))
                        .ReturnsAsync(new List<HekimDetail> { new HekimDetail() { /*TODO:propertyler buraya yazılacak HekimDetailId = 1, HekimDetailName = "test"*/ } });

            var handler = new GetHekimDetailsQueryHandler(_hekimDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<HekimDetail>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task HekimDetail_CreateCommand_Success()
        {
            HekimDetail rt = null;
            //Arrange
            var command = new CreateHekimDetailCommand();
            //propertyler buraya yazılacak
            //command.HekimDetailName = "deneme";

            _hekimDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HekimDetail, bool>>>()))
                        .ReturnsAsync(rt);

            _hekimDetailRepository.Setup(x => x.Add(It.IsAny<HekimDetail>())).Returns(new HekimDetail());

            var handler = new CreateHekimDetailCommandHandler(_hekimDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hekimDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task HekimDetail_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateHekimDetailCommand();
            //propertyler buraya yazılacak 
            //command.HekimDetailName = "test";

            _hekimDetailRepository.Setup(x => x.Query())
                                           .Returns(new List<HekimDetail> { new HekimDetail() { /*TODO:propertyler buraya yazılacak HekimDetailId = 1, HekimDetailName = "test"*/ } }.AsQueryable());

            _hekimDetailRepository.Setup(x => x.Add(It.IsAny<HekimDetail>())).Returns(new HekimDetail());

            var handler = new CreateHekimDetailCommandHandler(_hekimDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task HekimDetail_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateHekimDetailCommand();
            //command.HekimDetailName = "test";

            _hekimDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HekimDetail, bool>>>()))
                        .ReturnsAsync(new HekimDetail() { /*TODO:propertyler buraya yazılacak HekimDetailId = 1, HekimDetailName = "deneme"*/ });

            _hekimDetailRepository.Setup(x => x.Update(It.IsAny<HekimDetail>())).Returns(new HekimDetail());

            var handler = new UpdateHekimDetailCommandHandler(_hekimDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hekimDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task HekimDetail_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteHekimDetailCommand();

            _hekimDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HekimDetail, bool>>>()))
                        .ReturnsAsync(new HekimDetail() { /*TODO:propertyler buraya yazılacak HekimDetailId = 1, HekimDetailName = "deneme"*/});

            _hekimDetailRepository.Setup(x => x.Delete(It.IsAny<HekimDetail>()));

            var handler = new DeleteHekimDetailCommandHandler(_hekimDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hekimDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

