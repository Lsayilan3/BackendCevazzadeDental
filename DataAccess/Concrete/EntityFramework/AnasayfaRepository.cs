
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class AnasayfaRepository : EfEntityRepositoryBase<Anasayfa, ProjectDbContext>, IAnasayfaRepository
    {
        public AnasayfaRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
