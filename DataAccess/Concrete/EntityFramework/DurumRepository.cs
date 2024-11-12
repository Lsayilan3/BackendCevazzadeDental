
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class DurumRepository : EfEntityRepositoryBase<Durum, ProjectDbContext>, IDurumRepository
    {
        public DurumRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
