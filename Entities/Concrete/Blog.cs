using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Blog :IEntity
    {
        public int BlogId { get; set; }
        public string Photo { get; set; }
        public string Aciklayan { get; set; }
        public string Tarih { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Dil { get; set; }
    }
}
