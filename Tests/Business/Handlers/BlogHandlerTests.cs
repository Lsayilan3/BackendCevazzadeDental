
using Business.Handlers.Blogs.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Blogs.Queries.GetBlogQuery;
using Entities.Concrete;
using static Business.Handlers.Blogs.Queries.GetBlogsQuery;
using static Business.Handlers.Blogs.Commands.CreateBlogCommand;
using Business.Handlers.Blogs.Commands;
using Business.Constants;
using static Business.Handlers.Blogs.Commands.UpdateBlogCommand;
using static Business.Handlers.Blogs.Commands.DeleteBlogCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BlogHandlerTests
    {
        Mock<IBlogRepository> _blogRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _blogRepository = new Mock<IBlogRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Blog_GetQuery_Success()
        {
            //Arrange
            var query = new GetBlogQuery();

            _blogRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>())).ReturnsAsync(new Blog()
//propertyler buraya yazılacak
//{																		
//BlogId = 1,
//BlogName = "Test"
//}
);

            var handler = new GetBlogQueryHandler(_blogRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BlogId.Should().Be(1);

        }

        [Test]
        public async Task Blog_GetQueries_Success()
        {
            //Arrange
            var query = new GetBlogsQuery();

            _blogRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Blog, bool>>>()))
                        .ReturnsAsync(new List<Blog> { new Blog() { /*TODO:propertyler buraya yazılacak BlogId = 1, BlogName = "test"*/ } });

            var handler = new GetBlogsQueryHandler(_blogRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Blog>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Blog_CreateCommand_Success()
        {
            Blog rt = null;
            //Arrange
            var command = new CreateBlogCommand();
            //propertyler buraya yazılacak
            //command.BlogName = "deneme";

            _blogRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>()))
                        .ReturnsAsync(rt);

            _blogRepository.Setup(x => x.Add(It.IsAny<Blog>())).Returns(new Blog());

            var handler = new CreateBlogCommandHandler(_blogRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _blogRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Blog_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBlogCommand();
            //propertyler buraya yazılacak 
            //command.BlogName = "test";

            _blogRepository.Setup(x => x.Query())
                                           .Returns(new List<Blog> { new Blog() { /*TODO:propertyler buraya yazılacak BlogId = 1, BlogName = "test"*/ } }.AsQueryable());

            _blogRepository.Setup(x => x.Add(It.IsAny<Blog>())).Returns(new Blog());

            var handler = new CreateBlogCommandHandler(_blogRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Blog_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBlogCommand();
            //command.BlogName = "test";

            _blogRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>()))
                        .ReturnsAsync(new Blog() { /*TODO:propertyler buraya yazılacak BlogId = 1, BlogName = "deneme"*/ });

            _blogRepository.Setup(x => x.Update(It.IsAny<Blog>())).Returns(new Blog());

            var handler = new UpdateBlogCommandHandler(_blogRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _blogRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Blog_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBlogCommand();

            _blogRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>()))
                        .ReturnsAsync(new Blog() { /*TODO:propertyler buraya yazılacak BlogId = 1, BlogName = "deneme"*/});

            _blogRepository.Setup(x => x.Delete(It.IsAny<Blog>()));

            var handler = new DeleteBlogCommandHandler(_blogRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _blogRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

