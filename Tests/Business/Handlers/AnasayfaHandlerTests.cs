
using Business.Handlers.Anasayfas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Anasayfas.Queries.GetAnasayfaQuery;
using Entities.Concrete;
using static Business.Handlers.Anasayfas.Queries.GetAnasayfasQuery;
using static Business.Handlers.Anasayfas.Commands.CreateAnasayfaCommand;
using Business.Handlers.Anasayfas.Commands;
using Business.Constants;
using static Business.Handlers.Anasayfas.Commands.UpdateAnasayfaCommand;
using static Business.Handlers.Anasayfas.Commands.DeleteAnasayfaCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class AnasayfaHandlerTests
    {
        Mock<IAnasayfaRepository> _anasayfaRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _anasayfaRepository = new Mock<IAnasayfaRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Anasayfa_GetQuery_Success()
        {
            //Arrange
            var query = new GetAnasayfaQuery();

            _anasayfaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Anasayfa, bool>>>())).ReturnsAsync(new Anasayfa()
//propertyler buraya yazılacak
//{																		
//AnasayfaId = 1,
//AnasayfaName = "Test"
//}
);

            var handler = new GetAnasayfaQueryHandler(_anasayfaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.AnasayfaId.Should().Be(1);

        }

        [Test]
        public async Task Anasayfa_GetQueries_Success()
        {
            //Arrange
            var query = new GetAnasayfasQuery();

            _anasayfaRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Anasayfa, bool>>>()))
                        .ReturnsAsync(new List<Anasayfa> { new Anasayfa() { /*TODO:propertyler buraya yazılacak AnasayfaId = 1, AnasayfaName = "test"*/ } });

            var handler = new GetAnasayfasQueryHandler(_anasayfaRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Anasayfa>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Anasayfa_CreateCommand_Success()
        {
            Anasayfa rt = null;
            //Arrange
            var command = new CreateAnasayfaCommand();
            //propertyler buraya yazılacak
            //command.AnasayfaName = "deneme";

            _anasayfaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Anasayfa, bool>>>()))
                        .ReturnsAsync(rt);

            _anasayfaRepository.Setup(x => x.Add(It.IsAny<Anasayfa>())).Returns(new Anasayfa());

            var handler = new CreateAnasayfaCommandHandler(_anasayfaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _anasayfaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Anasayfa_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateAnasayfaCommand();
            //propertyler buraya yazılacak 
            //command.AnasayfaName = "test";

            _anasayfaRepository.Setup(x => x.Query())
                                           .Returns(new List<Anasayfa> { new Anasayfa() { /*TODO:propertyler buraya yazılacak AnasayfaId = 1, AnasayfaName = "test"*/ } }.AsQueryable());

            _anasayfaRepository.Setup(x => x.Add(It.IsAny<Anasayfa>())).Returns(new Anasayfa());

            var handler = new CreateAnasayfaCommandHandler(_anasayfaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Anasayfa_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateAnasayfaCommand();
            //command.AnasayfaName = "test";

            _anasayfaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Anasayfa, bool>>>()))
                        .ReturnsAsync(new Anasayfa() { /*TODO:propertyler buraya yazılacak AnasayfaId = 1, AnasayfaName = "deneme"*/ });

            _anasayfaRepository.Setup(x => x.Update(It.IsAny<Anasayfa>())).Returns(new Anasayfa());

            var handler = new UpdateAnasayfaCommandHandler(_anasayfaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _anasayfaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Anasayfa_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteAnasayfaCommand();

            _anasayfaRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Anasayfa, bool>>>()))
                        .ReturnsAsync(new Anasayfa() { /*TODO:propertyler buraya yazılacak AnasayfaId = 1, AnasayfaName = "deneme"*/});

            _anasayfaRepository.Setup(x => x.Delete(It.IsAny<Anasayfa>()));

            var handler = new DeleteAnasayfaCommandHandler(_anasayfaRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _anasayfaRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

