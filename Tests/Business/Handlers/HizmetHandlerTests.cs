
using Business.Handlers.Hizmets.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Hizmets.Queries.GetHizmetQuery;
using Entities.Concrete;
using static Business.Handlers.Hizmets.Queries.GetHizmetsQuery;
using static Business.Handlers.Hizmets.Commands.CreateHizmetCommand;
using Business.Handlers.Hizmets.Commands;
using Business.Constants;
using static Business.Handlers.Hizmets.Commands.UpdateHizmetCommand;
using static Business.Handlers.Hizmets.Commands.DeleteHizmetCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class HizmetHandlerTests
    {
        Mock<IHizmetRepository> _hizmetRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _hizmetRepository = new Mock<IHizmetRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Hizmet_GetQuery_Success()
        {
            //Arrange
            var query = new GetHizmetQuery();

            _hizmetRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hizmet, bool>>>())).ReturnsAsync(new Hizmet()
//propertyler buraya yazılacak
//{																		
//HizmetId = 1,
//HizmetName = "Test"
//}
);

            var handler = new GetHizmetQueryHandler(_hizmetRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.HizmetId.Should().Be(1);

        }

        [Test]
        public async Task Hizmet_GetQueries_Success()
        {
            //Arrange
            var query = new GetHizmetsQuery();

            _hizmetRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Hizmet, bool>>>()))
                        .ReturnsAsync(new List<Hizmet> { new Hizmet() { /*TODO:propertyler buraya yazılacak HizmetId = 1, HizmetName = "test"*/ } });

            var handler = new GetHizmetsQueryHandler(_hizmetRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Hizmet>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Hizmet_CreateCommand_Success()
        {
            Hizmet rt = null;
            //Arrange
            var command = new CreateHizmetCommand();
            //propertyler buraya yazılacak
            //command.HizmetName = "deneme";

            _hizmetRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hizmet, bool>>>()))
                        .ReturnsAsync(rt);

            _hizmetRepository.Setup(x => x.Add(It.IsAny<Hizmet>())).Returns(new Hizmet());

            var handler = new CreateHizmetCommandHandler(_hizmetRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hizmetRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Hizmet_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateHizmetCommand();
            //propertyler buraya yazılacak 
            //command.HizmetName = "test";

            _hizmetRepository.Setup(x => x.Query())
                                           .Returns(new List<Hizmet> { new Hizmet() { /*TODO:propertyler buraya yazılacak HizmetId = 1, HizmetName = "test"*/ } }.AsQueryable());

            _hizmetRepository.Setup(x => x.Add(It.IsAny<Hizmet>())).Returns(new Hizmet());

            var handler = new CreateHizmetCommandHandler(_hizmetRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Hizmet_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateHizmetCommand();
            //command.HizmetName = "test";

            _hizmetRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hizmet, bool>>>()))
                        .ReturnsAsync(new Hizmet() { /*TODO:propertyler buraya yazılacak HizmetId = 1, HizmetName = "deneme"*/ });

            _hizmetRepository.Setup(x => x.Update(It.IsAny<Hizmet>())).Returns(new Hizmet());

            var handler = new UpdateHizmetCommandHandler(_hizmetRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hizmetRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Hizmet_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteHizmetCommand();

            _hizmetRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Hizmet, bool>>>()))
                        .ReturnsAsync(new Hizmet() { /*TODO:propertyler buraya yazılacak HizmetId = 1, HizmetName = "deneme"*/});

            _hizmetRepository.Setup(x => x.Delete(It.IsAny<Hizmet>()));

            var handler = new DeleteHizmetCommandHandler(_hizmetRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hizmetRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

