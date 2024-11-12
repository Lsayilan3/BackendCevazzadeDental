
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class HekimRepository : EfEntityRepositoryBase<Hekim, ProjectDbContext>, IHekimRepository
    {
        public HekimRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
