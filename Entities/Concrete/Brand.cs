using System;
using System.Collections.Generic;
using System.Text;
using Core.entity;

namespace Entities.Concrete
{
    public class Brand:IEntity
    {
        public int id { get; set; }
        public string BrandName { get; set; }
    }
}
