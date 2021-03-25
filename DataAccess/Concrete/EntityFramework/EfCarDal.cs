
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.dataAcces.EntityFramework;
using Entities.DTOs;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, NorthwindContext>, ICarDal
    {
        public List<CarDetailsDto> GetCarDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.id
                             join brand in context.Brands
                             on car.BrandId equals brand.id
                             select new CarDetailsDto
                             {
                                 BrandName = brand.BrandName,
                                 CarName = car.CarName,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice
                             };

                return result.ToList();

            }
        }
    }

}
