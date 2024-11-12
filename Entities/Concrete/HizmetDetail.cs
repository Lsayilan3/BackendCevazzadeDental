using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class HizmetDetail : IEntity
    {
        public int HizmetDetailId { get; set; }
        public int HizmetId { get; set; }
        public string PhotoService { get; set; }
        public string Photo { get; set; }
        public string Editor { get; set; }
        public int Dil { get; set; }
    }
}
