
using Business.Handlers.BlogDetails.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.BlogDetails.Queries.GetBlogDetailQuery;
using Entities.Concrete;
using static Business.Handlers.BlogDetails.Queries.GetBlogDetailsQuery;
using static Business.Handlers.BlogDetails.Commands.CreateBlogDetailCommand;
using Business.Handlers.BlogDetails.Commands;
using Business.Constants;
using static Business.Handlers.BlogDetails.Commands.UpdateBlogDetailCommand;
using static Business.Handlers.BlogDetails.Commands.DeleteBlogDetailCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BlogDetailHandlerTests
    {
        Mock<IBlogDetailRepository> _blogDetailRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _blogDetailRepository = new Mock<IBlogDetailRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task BlogDetail_GetQuery_Success()
        {
            //Arrange
            var query = new GetBlogDetailQuery();

            _blogDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BlogDetail, bool>>>())).ReturnsAsync(new BlogDetail()
//propertyler buraya yazılacak
//{																		
//BlogDetailId = 1,
//BlogDetailName = "Test"
//}
);

            var handler = new GetBlogDetailQueryHandler(_blogDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BlogDetailId.Should().Be(1);

        }

        [Test]
        public async Task BlogDetail_GetQueries_Success()
        {
            //Arrange
            var query = new GetBlogDetailsQuery();

            _blogDetailRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<BlogDetail, bool>>>()))
                        .ReturnsAsync(new List<BlogDetail> { new BlogDetail() { /*TODO:propertyler buraya yazılacak BlogDetailId = 1, BlogDetailName = "test"*/ } });

            var handler = new GetBlogDetailsQueryHandler(_blogDetailRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<BlogDetail>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task BlogDetail_CreateCommand_Success()
        {
            BlogDetail rt = null;
            //Arrange
            var command = new CreateBlogDetailCommand();
            //propertyler buraya yazılacak
            //command.BlogDetailName = "deneme";

            _blogDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BlogDetail, bool>>>()))
                        .ReturnsAsync(rt);

            _blogDetailRepository.Setup(x => x.Add(It.IsAny<BlogDetail>())).Returns(new BlogDetail());

            var handler = new CreateBlogDetailCommandHandler(_blogDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _blogDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task BlogDetail_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBlogDetailCommand();
            //propertyler buraya yazılacak 
            //command.BlogDetailName = "test";

            _blogDetailRepository.Setup(x => x.Query())
                                           .Returns(new List<BlogDetail> { new BlogDetail() { /*TODO:propertyler buraya yazılacak BlogDetailId = 1, BlogDetailName = "test"*/ } }.AsQueryable());

            _blogDetailRepository.Setup(x => x.Add(It.IsAny<BlogDetail>())).Returns(new BlogDetail());

            var handler = new CreateBlogDetailCommandHandler(_blogDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task BlogDetail_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBlogDetailCommand();
            //command.BlogDetailName = "test";

            _blogDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BlogDetail, bool>>>()))
                        .ReturnsAsync(new BlogDetail() { /*TODO:propertyler buraya yazılacak BlogDetailId = 1, BlogDetailName = "deneme"*/ });

            _blogDetailRepository.Setup(x => x.Update(It.IsAny<BlogDetail>())).Returns(new BlogDetail());

            var handler = new UpdateBlogDetailCommandHandler(_blogDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _blogDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task BlogDetail_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBlogDetailCommand();

            _blogDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<BlogDetail, bool>>>()))
                        .ReturnsAsync(new BlogDetail() { /*TODO:propertyler buraya yazılacak BlogDetailId = 1, BlogDetailName = "deneme"*/});

            _blogDetailRepository.Setup(x => x.Delete(It.IsAny<BlogDetail>()));

            var handler = new DeleteBlogDetailCommandHandler(_blogDetailRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _blogDetailRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

