
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class BlogDetailRepository : EfEntityRepositoryBase<BlogDetail, ProjectDbContext>, IBlogDetailRepository
    {
        public BlogDetailRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
