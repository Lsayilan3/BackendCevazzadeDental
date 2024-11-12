using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class HekimDetail :IEntity
    {
        public int HekimDetailId { get; set; }
        public int HekimId { get; set; }
        public string Photo { get; set; }
        public string Ad { get; set; }
        public string Uzmanlik { get; set; }
        public string Aciklama { get; set; }
        public string SosyalFace { get; set; }
        public string SosyalTwitter { get; set; }
        public string Sosyalingstagram { get; set; }
        public string SosyalMail { get; set; }
        public int Dil { get; set; }
    }
}
