using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Durum :IEntity
    {
        public int DurumId { get; set; }
        public string Baslik { get; set; }
        public string ParagrafOne { get; set; }
        public string ParagrafTwo { get; set; }
        public string Photo { get; set; }
        public int Dil { get; set; }
    }
}
