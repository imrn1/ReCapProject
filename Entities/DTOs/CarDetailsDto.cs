using System;
using System.Collections.Generic;
using System.Text;
using Core.entity;


namespace Entities.DTOs
{
     public class CarDetailsDto:IDto
    {
        // CarName, BrandName, ColorName, DailyPrice. 

        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public int DailyPrice { get; set; }

    }
}
