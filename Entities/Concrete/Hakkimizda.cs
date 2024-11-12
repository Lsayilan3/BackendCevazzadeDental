using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Hakkimizda : IEntity
    {
        public int HakkimizdaId { get; set; }
        public string Photo { get; set; }
        public string Editor { get; set; }
        public int Dil { get; set; }
    }
}
