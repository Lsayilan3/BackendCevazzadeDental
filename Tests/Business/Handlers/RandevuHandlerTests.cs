
using Business.Handlers.Randevus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Randevus.Queries.GetRandevuQuery;
using Entities.Concrete;
using static Business.Handlers.Randevus.Queries.GetRandevusQuery;
using static Business.Handlers.Randevus.Commands.CreateRandevuCommand;
using Business.Handlers.Randevus.Commands;
using Business.Constants;
using static Business.Handlers.Randevus.Commands.UpdateRandevuCommand;
using static Business.Handlers.Randevus.Commands.DeleteRandevuCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RandevuHandlerTests
    {
        Mock<IRandevuRepository> _randevuRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _randevuRepository = new Mock<IRandevuRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Randevu_GetQuery_Success()
        {
            //Arrange
            var query = new GetRandevuQuery();

            _randevuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Randevu, bool>>>())).ReturnsAsync(new Randevu()
//propertyler buraya yazılacak
//{																		
//RandevuId = 1,
//RandevuName = "Test"
//}
);

            var handler = new GetRandevuQueryHandler(_randevuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RandevuId.Should().Be(1);

        }

        [Test]
        public async Task Randevu_GetQueries_Success()
        {
            //Arrange
            var query = new GetRandevusQuery();

            _randevuRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Randevu, bool>>>()))
                        .ReturnsAsync(new List<Randevu> { new Randevu() { /*TODO:propertyler buraya yazılacak RandevuId = 1, RandevuName = "test"*/ } });

            var handler = new GetRandevusQueryHandler(_randevuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Randevu>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Randevu_CreateCommand_Success()
        {
            Randevu rt = null;
            //Arrange
            var command = new CreateRandevuCommand();
            //propertyler buraya yazılacak
            //command.RandevuName = "deneme";

            _randevuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Randevu, bool>>>()))
                        .ReturnsAsync(rt);

            _randevuRepository.Setup(x => x.Add(It.IsAny<Randevu>())).Returns(new Randevu());

            var handler = new CreateRandevuCommandHandler(_randevuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _randevuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Randevu_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRandevuCommand();
            //propertyler buraya yazılacak 
            //command.RandevuName = "test";

            _randevuRepository.Setup(x => x.Query())
                                           .Returns(new List<Randevu> { new Randevu() { /*TODO:propertyler buraya yazılacak RandevuId = 1, RandevuName = "test"*/ } }.AsQueryable());

            _randevuRepository.Setup(x => x.Add(It.IsAny<Randevu>())).Returns(new Randevu());

            var handler = new CreateRandevuCommandHandler(_randevuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Randevu_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRandevuCommand();
            //command.RandevuName = "test";

            _randevuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Randevu, bool>>>()))
                        .ReturnsAsync(new Randevu() { /*TODO:propertyler buraya yazılacak RandevuId = 1, RandevuName = "deneme"*/ });

            _randevuRepository.Setup(x => x.Update(It.IsAny<Randevu>())).Returns(new Randevu());

            var handler = new UpdateRandevuCommandHandler(_randevuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _randevuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Randevu_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRandevuCommand();

            _randevuRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Randevu, bool>>>()))
                        .ReturnsAsync(new Randevu() { /*TODO:propertyler buraya yazılacak RandevuId = 1, RandevuName = "deneme"*/});

            _randevuRepository.Setup(x => x.Delete(It.IsAny<Randevu>()));

            var handler = new DeleteRandevuCommandHandler(_randevuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _randevuRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

