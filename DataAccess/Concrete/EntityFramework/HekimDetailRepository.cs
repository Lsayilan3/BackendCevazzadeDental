
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class HekimDetailRepository : EfEntityRepositoryBase<HekimDetail, ProjectDbContext>, IHekimDetailRepository
    {
        public HekimDetailRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
