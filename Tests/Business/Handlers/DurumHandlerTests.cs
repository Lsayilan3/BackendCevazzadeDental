
using Business.Handlers.Durums.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Durums.Queries.GetDurumQuery;
using Entities.Concrete;
using static Business.Handlers.Durums.Queries.GetDurumsQuery;
using static Business.Handlers.Durums.Commands.CreateDurumCommand;
using Business.Handlers.Durums.Commands;
using Business.Constants;
using static Business.Handlers.Durums.Commands.UpdateDurumCommand;
using static Business.Handlers.Durums.Commands.DeleteDurumCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DurumHandlerTests
    {
        Mock<IDurumRepository> _durumRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _durumRepository = new Mock<IDurumRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Durum_GetQuery_Success()
        {
            //Arrange
            var query = new GetDurumQuery();

            _durumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Durum, bool>>>())).ReturnsAsync(new Durum()
//propertyler buraya yazılacak
//{																		
//DurumId = 1,
//DurumName = "Test"
//}
);

            var handler = new GetDurumQueryHandler(_durumRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DurumId.Should().Be(1);

        }

        [Test]
        public async Task Durum_GetQueries_Success()
        {
            //Arrange
            var query = new GetDurumsQuery();

            _durumRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Durum, bool>>>()))
                        .ReturnsAsync(new List<Durum> { new Durum() { /*TODO:propertyler buraya yazılacak DurumId = 1, DurumName = "test"*/ } });

            var handler = new GetDurumsQueryHandler(_durumRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Durum>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Durum_CreateCommand_Success()
        {
            Durum rt = null;
            //Arrange
            var command = new CreateDurumCommand();
            //propertyler buraya yazılacak
            //command.DurumName = "deneme";

            _durumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Durum, bool>>>()))
                        .ReturnsAsync(rt);

            _durumRepository.Setup(x => x.Add(It.IsAny<Durum>())).Returns(new Durum());

            var handler = new CreateDurumCommandHandler(_durumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _durumRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Durum_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDurumCommand();
            //propertyler buraya yazılacak 
            //command.DurumName = "test";

            _durumRepository.Setup(x => x.Query())
                                           .Returns(new List<Durum> { new Durum() { /*TODO:propertyler buraya yazılacak DurumId = 1, DurumName = "test"*/ } }.AsQueryable());

            _durumRepository.Setup(x => x.Add(It.IsAny<Durum>())).Returns(new Durum());

            var handler = new CreateDurumCommandHandler(_durumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Durum_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDurumCommand();
            //command.DurumName = "test";

            _durumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Durum, bool>>>()))
                        .ReturnsAsync(new Durum() { /*TODO:propertyler buraya yazılacak DurumId = 1, DurumName = "deneme"*/ });

            _durumRepository.Setup(x => x.Update(It.IsAny<Durum>())).Returns(new Durum());

            var handler = new UpdateDurumCommandHandler(_durumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _durumRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Durum_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDurumCommand();

            _durumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Durum, bool>>>()))
                        .ReturnsAsync(new Durum() { /*TODO:propertyler buraya yazılacak DurumId = 1, DurumName = "deneme"*/});

            _durumRepository.Setup(x => x.Delete(It.IsAny<Durum>()));

            var handler = new DeleteDurumCommandHandler(_durumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _durumRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

