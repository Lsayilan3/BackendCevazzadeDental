
using Business.Handlers.Hakkimizdas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Hakkimizdas.Queries.GetHakkimizdaQuery;
using Entities.Concrete;
using static Business.Handlers.Hakkimizdas.Queries.GetHakkimizdasQuery;
using static Business.Handlers.Hakkimizdas.Commands.CreateHakkimizdaCommand;
using Business.Handlers.Hakkimizdas.Commands;
using Business.Constants;
using static Business.Handlers.Hakkimizdas.Commands.UpdateHakkimizdaCommand;
using static Business.Handlers.Hakkimizdas.Commands.DeleteHakkimizdaCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class HakkimizdaHandlerTests
    {
        Mock<IHakkimizdaRepository> _hakkimizdaRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _hakkimizdaRepository = new Mock<IHakkimizdaRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Hakkimizda_GetQuery_Success()
        {
            //Arrange
            var query = new GetHakkimizdaQuery();

            _hakkimizdaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hakkimizda, bool>>>())).ReturnsAsync(new Hakkimizda()
//propertyler buraya yazılacak
//{																		
//HakkimizdaId = 1,
//HakkimizdaName = "Test"
//}
);

            var handler = new GetHakkimizdaQueryHandler(_hakkimizdaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.HakkimizdaId.Should().Be(1);

        }

        [Test]
        public async Task Hakkimizda_GetQueries_Success()
        {
            //Arrange
            var query = new GetHakkimizdasQuery();

            _hakkimizdaRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Hakkimizda, bool>>>()))
                        .ReturnsAsync(new List<Hakkimizda> { new Hakkimizda() { /*TODO:propertyler buraya yazılacak HakkimizdaId = 1, HakkimizdaName = "test"*/ } });

            var handler = new GetHakkimizdasQueryHandler(_hakkimizdaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Hakkimizda>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Hakkimizda_CreateCommand_Success()
        {
            Hakkimizda rt = null;
            //Arrange
            var command = new CreateHakkimizdaCommand();
            //propertyler buraya yazılacak
            //command.HakkimizdaName = "deneme";

            _hakkimizdaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hakkimizda, bool>>>()))
                        .ReturnsAsync(rt);

            _hakkimizdaRepository.Setup(x => x.Add(It.IsAny<Hakkimizda>())).Returns(new Hakkimizda());

            var handler = new CreateHakkimizdaCommandHandler(_hakkimizdaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hakkimizdaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Hakkimizda_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateHakkimizdaCommand();
            //propertyler buraya yazılacak 
            //command.HakkimizdaName = "test";

            _hakkimizdaRepository.Setup(x => x.Query())
                                           .Returns(new List<Hakkimizda> { new Hakkimizda() { /*TODO:propertyler buraya yazılacak HakkimizdaId = 1, HakkimizdaName = "test"*/ } }.AsQueryable());

            _hakkimizdaRepository.Setup(x => x.Add(It.IsAny<Hakkimizda>())).Returns(new Hakkimizda());

            var handler = new CreateHakkimizdaCommandHandler(_hakkimizdaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Hakkimizda_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateHakkimizdaCommand();
            //command.HakkimizdaName = "test";

            _hakkimizdaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hakkimizda, bool>>>()))
                        .ReturnsAsync(new Hakkimizda() { /*TODO:propertyler buraya yazılacak HakkimizdaId = 1, HakkimizdaName = "deneme"*/ });

            _hakkimizdaRepository.Setup(x => x.Update(It.IsAny<Hakkimizda>())).Returns(new Hakkimizda());

            var handler = new UpdateHakkimizdaCommandHandler(_hakkimizdaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hakkimizdaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Hakkimizda_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteHakkimizdaCommand();

            _hakkimizdaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hakkimizda, bool>>>()))
                        .ReturnsAsync(new Hakkimizda() { /*TODO:propertyler buraya yazılacak HakkimizdaId = 1, HakkimizdaName = "deneme"*/});

            _hakkimizdaRepository.Setup(x => x.Delete(It.IsAny<Hakkimizda>()));

            var handler = new DeleteHakkimizdaCommandHandler(_hakkimizdaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hakkimizdaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

