using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AnasayfaPhotoUrl :IEntity
    {
        public int AnasayfaPhotoUrlId { get; set; }
        public string Photo { get; set; }
    }
}
 