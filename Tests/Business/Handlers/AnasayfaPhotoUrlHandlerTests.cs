
using Business.Handlers.AnasayfaPhotoUrls.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.AnasayfaPhotoUrls.Queries.GetAnasayfaPhotoUrlQuery;
using Entities.Concrete;
using static Business.Handlers.AnasayfaPhotoUrls.Queries.GetAnasayfaPhotoUrlsQuery;
using static Business.Handlers.AnasayfaPhotoUrls.Commands.CreateAnasayfaPhotoUrlCommand;
using Business.Handlers.AnasayfaPhotoUrls.Commands;
using Business.Constants;
using static Business.Handlers.AnasayfaPhotoUrls.Commands.UpdateAnasayfaPhotoUrlCommand;
using static Business.Handlers.AnasayfaPhotoUrls.Commands.DeleteAnasayfaPhotoUrlCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class AnasayfaPhotoUrlHandlerTests
    {
        Mock<IAnasayfaPhotoUrlRepository> _anasayfaPhotoUrlRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _anasayfaPhotoUrlRepository = new Mock<IAnasayfaPhotoUrlRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task AnasayfaPhotoUrl_GetQuery_Success()
        {
            //Arrange
            var query = new GetAnasayfaPhotoUrlQuery();

            _anasayfaPhotoUrlRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AnasayfaPhotoUrl, bool>>>())).ReturnsAsync(new AnasayfaPhotoUrl()
//propertyler buraya yazılacak
//{																		
//AnasayfaPhotoUrlId = 1,
//AnasayfaPhotoUrlName = "Test"
//}
);

            var handler = new GetAnasayfaPhotoUrlQueryHandler(_anasayfaPhotoUrlRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.AnasayfaPhotoUrlId.Should().Be(1);

        }

        [Test]
        public async Task AnasayfaPhotoUrl_GetQueries_Success()
        {
            //Arrange
            var query = new GetAnasayfaPhotoUrlsQuery();

            _anasayfaPhotoUrlRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AnasayfaPhotoUrl, bool>>>()))
                        .ReturnsAsync(new List<AnasayfaPhotoUrl> { new AnasayfaPhotoUrl() { /*TODO:propertyler buraya yazılacak AnasayfaPhotoUrlId = 1, AnasayfaPhotoUrlName = "test"*/ } });

            var handler = new GetAnasayfaPhotoUrlsQueryHandler(_anasayfaPhotoUrlRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<AnasayfaPhotoUrl>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task AnasayfaPhotoUrl_CreateCommand_Success()
        {
            AnasayfaPhotoUrl rt = null;
            //Arrange
            var command = new CreateAnasayfaPhotoUrlCommand();
            //propertyler buraya yazılacak
            //command.AnasayfaPhotoUrlName = "deneme";

            _anasayfaPhotoUrlRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AnasayfaPhotoUrl, bool>>>()))
                        .ReturnsAsync(rt);

            _anasayfaPhotoUrlRepository.Setup(x => x.Add(It.IsAny<AnasayfaPhotoUrl>())).Returns(new AnasayfaPhotoUrl());

            var handler = new CreateAnasayfaPhotoUrlCommandHandler(_anasayfaPhotoUrlRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _anasayfaPhotoUrlRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task AnasayfaPhotoUrl_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateAnasayfaPhotoUrlCommand();
            //propertyler buraya yazılacak 
            //command.AnasayfaPhotoUrlName = "test";

            _anasayfaPhotoUrlRepository.Setup(x => x.Query())
                                           .Returns(new List<AnasayfaPhotoUrl> { new AnasayfaPhotoUrl() { /*TODO:propertyler buraya yazılacak AnasayfaPhotoUrlId = 1, AnasayfaPhotoUrlName = "test"*/ } }.AsQueryable());

            _anasayfaPhotoUrlRepository.Setup(x => x.Add(It.IsAny<AnasayfaPhotoUrl>())).Returns(new AnasayfaPhotoUrl());

            var handler = new CreateAnasayfaPhotoUrlCommandHandler(_anasayfaPhotoUrlRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task AnasayfaPhotoUrl_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateAnasayfaPhotoUrlCommand();
            //command.AnasayfaPhotoUrlName = "test";

            _anasayfaPhotoUrlRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AnasayfaPhotoUrl, bool>>>()))
                        .ReturnsAsync(new AnasayfaPhotoUrl() { /*TODO:propertyler buraya yazılacak AnasayfaPhotoUrlId = 1, AnasayfaPhotoUrlName = "deneme"*/ });

            _anasayfaPhotoUrlRepository.Setup(x => x.Update(It.IsAny<AnasayfaPhotoUrl>())).Returns(new AnasayfaPhotoUrl());

            var handler = new UpdateAnasayfaPhotoUrlCommandHandler(_anasayfaPhotoUrlRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _anasayfaPhotoUrlRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task AnasayfaPhotoUrl_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteAnasayfaPhotoUrlCommand();

            _anasayfaPhotoUrlRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AnasayfaPhotoUrl, bool>>>()))
                        .ReturnsAsync(new AnasayfaPhotoUrl() { /*TODO:propertyler buraya yazılacak AnasayfaPhotoUrlId = 1, AnasayfaPhotoUrlName = "deneme"*/});

            _anasayfaPhotoUrlRepository.Setup(x => x.Delete(It.IsAny<AnasayfaPhotoUrl>()));

            var handler = new DeleteAnasayfaPhotoUrlCommandHandler(_anasayfaPhotoUrlRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _anasayfaPhotoUrlRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

