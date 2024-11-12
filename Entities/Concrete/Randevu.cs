using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Randevu :IEntity
    {
        public int RandevuId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Tel { get; set; }
        public int HizmetlerimizId { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime CraeteDate { get; set; } = DateTime.UtcNow;
        public string Mesaj { get; set; }
    }
}
