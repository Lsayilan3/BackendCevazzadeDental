
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class RandevuRepository : EfEntityRepositoryBase<Randevu, ProjectDbContext>, IRandevuRepository
    {
        public RandevuRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
