
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class AnasayfaPhotoUrlRepository : EfEntityRepositoryBase<AnasayfaPhotoUrl, ProjectDbContext>, IAnasayfaPhotoUrlRepository
    {
        public AnasayfaPhotoUrlRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
