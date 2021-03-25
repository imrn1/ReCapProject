using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {

            // InMemoryTest();

            //EfCarDalTest();

            // EfColorDalTest();

            EfBrandDalTest();

        }

        private static void EfBrandDalTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            Console.WriteLine("---------------All Brands---------------\n");
            var result = brandManager.GetAll();
            if (result.Success)
            {
                foreach (Brand item in result.Data)
                {
                    Console.WriteLine("brandId: {0}, BrandName: {1}",item.id,item.BrandName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

        }


        private static void EfColorDalTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());

            Console.WriteLine("---------------AllColors---------------\n");
            List<Color> colors = colorManager.GetAll();
            showColorList(colors);

            Console.WriteLine("---------------  AddColor colorname=blackk  ---------------\n");
            Color c = new Color {ColorName = "blackk" };
            colorManager.Add(c);
            colors = colorManager.GetAll();
            showColorList(colors);

            Console.WriteLine("--------------- UpdateColor colorname=blackk to black ---------------\n");
            c = colorManager.GetByColorName("blackk");
            c.ColorName = "black";
            colorManager.Update(c);
            colors = colorManager.GetAll();
            showColorList(colors);

        }

        private static void EfCarDalTest()
        {
            Console.WriteLine("---------------AllCars---------------\n");

            CarManager carManager2 = new CarManager(new EfCarDal());
            List<Car> cars = carManager2.GetAll();
            showCarList(cars);

            cars = carManager2.GetCarsByBrandId(5);
            Console.WriteLine("\n------------GroupByBrandId = 5 ------------\n");
            showCarList(cars);

            cars = carManager2.GetCarsByColorId(1);
            Console.WriteLine("\n------------GroupByColorId = 1 ------------\n");
            showCarList(cars);

            cars = carManager2.GetByPrice(1700, 3000);
            Console.WriteLine("\n------------GroupByPrice min= 1700 max=3000 ------------\n");
            showCarList(cars);

            Car c = new Car()
            {
                BrandId = 1,
                ColorId = 3,
                ModelYear = "2020",
                Description = "Knockout styling - Beautifully finished interior",
                CarName = "Exclusive E200",
                DailyPrice = 2500
            };
            carManager2.Add(c);
            Console.WriteLine("\n----------  add car  ------------\n");
            cars = carManager2.GetAll();
            showCarList(cars);

            Console.WriteLine("\n----------  CarDetails  ------------\n");
            List<CarDetailsDto> list = carManager2.GetCarDetails();
            foreach (CarDetailsDto item in list)
            {
                // CarName, BrandName, ColorName, DailyPrice.
                Console.WriteLine("model: {0} barnd: {1} color: {2} price: {3}",
                    item.CarName.PadRight(20),item.BrandName.PadRight(10),
                    item.ColorName.PadRight(15),item.DailyPrice.ToString().PadRight(5));
            }
            

        }

        static void showCarList(List<Car> carList)
        {
        
            foreach (Car car in carList)
            {
                Console.WriteLine("id: {0},model: {6}, ModelYear: {1}, DailyPrice: {2}, ColorId: {3}," +
                    " BrandId: {4}, description: {5} ", car.id, car.ModelYear, car.DailyPrice,
                    car.ColorId,car.BrandId, car.Description,car.CarName);
            }
        }

        static void showColorList(List<Color> list)
        {

            foreach (Color c in list)
            {
                Console.WriteLine("id: {0}, colorName: {1} ", c.id, c.ColorName);
            }
        }

        private static void InMemoryTest()
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            Console.WriteLine("---------------- GetAll cars ---------------------\n");
            List<Car> cars = carManager.GetAll();
            showCarList(cars);

            Console.WriteLine("\n---------------- Add car ---------------------\n");

            carManager.Add(new Car() { 
                id = 4, DailyPrice = 3000, BrandId = 3, Description = "qwe",
                ModelYear = "2001", ColorId = 5 
            });
            cars = carManager.GetAll();
            showCarList(cars);

            Console.WriteLine("\n-------------- delete car id=1 --------------------\n");

            carManager.Delete(1);
            cars = carManager.GetAll();
            showCarList(cars);

            Console.WriteLine("\n-------------- Update car id=4 --------------------\n");
            carManager.Update(new Car() { id = 4, DailyPrice = 3800, BrandId = 3, Description = "updated 4", ModelYear = "2001", ColorId = 5 });
            cars = carManager.GetAll();
            showCarList(cars);

        }
    }
}
