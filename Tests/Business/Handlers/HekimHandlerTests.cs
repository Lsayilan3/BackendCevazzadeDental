
using Business.Handlers.Hekims.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Hekims.Queries.GetHekimQuery;
using Entities.Concrete;
using static Business.Handlers.Hekims.Queries.GetHekimsQuery;
using static Business.Handlers.Hekims.Commands.CreateHekimCommand;
using Business.Handlers.Hekims.Commands;
using Business.Constants;
using static Business.Handlers.Hekims.Commands.UpdateHekimCommand;
using static Business.Handlers.Hekims.Commands.DeleteHekimCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class HekimHandlerTests
    {
        Mock<IHekimRepository> _hekimRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _hekimRepository = new Mock<IHekimRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Hekim_GetQuery_Success()
        {
            //Arrange
            var query = new GetHekimQuery();

            _hekimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hekim, bool>>>())).ReturnsAsync(new Hekim()
//propertyler buraya yazılacak
//{																		
//HekimId = 1,
//HekimName = "Test"
//}
);

            var handler = new GetHekimQueryHandler(_hekimRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.HekimId.Should().Be(1);

        }

        [Test]
        public async Task Hekim_GetQueries_Success()
        {
            //Arrange
            var query = new GetHekimsQuery();

            _hekimRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Hekim, bool>>>()))
                        .ReturnsAsync(new List<Hekim> { new Hekim() { /*TODO:propertyler buraya yazılacak HekimId = 1, HekimName = "test"*/ } });

            var handler = new GetHekimsQueryHandler(_hekimRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Hekim>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Hekim_CreateCommand_Success()
        {
            Hekim rt = null;
            //Arrange
            var command = new CreateHekimCommand();
            //propertyler buraya yazılacak
            //command.HekimName = "deneme";

            _hekimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hekim, bool>>>()))
                        .ReturnsAsync(rt);

            _hekimRepository.Setup(x => x.Add(It.IsAny<Hekim>())).Returns(new Hekim());

            var handler = new CreateHekimCommandHandler(_hekimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hekimRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Hekim_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateHekimCommand();
            //propertyler buraya yazılacak 
            //command.HekimName = "test";

            _hekimRepository.Setup(x => x.Query())
                                           .Returns(new List<Hekim> { new Hekim() { /*TODO:propertyler buraya yazılacak HekimId = 1, HekimName = "test"*/ } }.AsQueryable());

            _hekimRepository.Setup(x => x.Add(It.IsAny<Hekim>())).Returns(new Hekim());

            var handler = new CreateHekimCommandHandler(_hekimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Hekim_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateHekimCommand();
            //command.HekimName = "test";

            _hekimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hekim, bool>>>()))
                        .ReturnsAsync(new Hekim() { /*TODO:propertyler buraya yazılacak HekimId = 1, HekimName = "deneme"*/ });

            _hekimRepository.Setup(x => x.Update(It.IsAny<Hekim>())).Returns(new Hekim());

            var handler = new UpdateHekimCommandHandler(_hekimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hekimRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Hekim_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteHekimCommand();

            _hekimRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hekim, bool>>>()))
                        .ReturnsAsync(new Hekim() { /*TODO:propertyler buraya yazılacak HekimId = 1, HekimName = "deneme"*/});

            _hekimRepository.Setup(x => x.Delete(It.IsAny<Hekim>()));

            var handler = new DeleteHekimCommandHandler(_hekimRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hekimRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

