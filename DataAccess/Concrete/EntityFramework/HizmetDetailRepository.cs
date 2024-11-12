
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class HizmetDetailRepository : EfEntityRepositoryBase<HizmetDetail, ProjectDbContext>, IHizmetDetailRepository
    {
        public HizmetDetailRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
