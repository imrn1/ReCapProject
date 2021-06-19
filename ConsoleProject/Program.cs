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

            // EfCarDalTest();

            // EfColorDalTest();

            // EfBrandDalTest();


            //Guid g = Guid.NewGuid();
            //Console.WriteLine("g: "+g);
            //Console.WriteLine(g.GetType());

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
            
            Console.WriteLine("\n--------------- GetById id=5---------------\n");
            var result2 = brandManager.GetById(5);
            if (result2.Success)
            {
                Console.WriteLine(result2.Data.id + " "+ result2.Data.BrandName);
            }
            else
            {
                Console.WriteLine(result2.Message);
            }

            Console.WriteLine("\n--------------- Add brandName=deneme---------------\n");
            Brand brand = new Brand { BrandName = "deneme" };            
            var result3 = brandManager.Add(brand);
            Console.WriteLine(result3.Message);

            showBrandList(brandManager.GetAll().Data);

            Console.WriteLine("\n--------------- Update brandName=deneme to DenemE---------------\n");
            brand = brandManager.GetByBrandName("deneme").Data;
            brand.BrandName = "DenemE";
            result3 = brandManager.Update(brand);
            Console.WriteLine(result3.Message);

            showBrandList(brandManager.GetAll().Data);

            Console.WriteLine("\n--------------- Delete brandName = DenemE ---------------\n");
            int id = brandManager.GetByBrandName("DenemE").Data.id;
            result3 = brandManager.Delete(id);
            Console.WriteLine(result3.Message);

            showBrandList(brandManager.GetAll().Data);

        }


        private static void EfColorDalTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());

            Console.WriteLine("---------------AllColors---------------\n");           
            showColorList(colorManager.GetAll().Data);

            Console.WriteLine("---------------  AddColor colorname=blackk  ---------------\n");
            Color c = new Color {ColorName = "blackk" };
            colorManager.Add(c);           
            showColorList(colorManager.GetAll().Data);

            Console.WriteLine("--------------- UpdateColor colorname=blackk to black ---------------\n");
            c = colorManager.GetByColorName("blackk").Data;
            c.ColorName = "black";
            colorManager.Update(c);           
            showColorList(colorManager.GetAll().Data);
        }

        private static void EfCarDalTest()
        {
            Console.WriteLine("---------------AllCars---------------\n");

            CarManager carManager2 = new CarManager(new EfCarDal(),new BrandManager(new EfBrandDal()));
            List<Car> cars = carManager2.GetAll().Data;
            ShowCarList(cars);

            cars = carManager2.GetCarsByBrandId(5).Data;
            Console.WriteLine("\n------------GroupByBrandId = 5 ------------\n");
            ShowCarList(cars);

            cars = carManager2.GetCarsByColorId(1).Data;
            Console.WriteLine("\n------------GroupByColorId = 1 ------------\n");
            ShowCarList(cars);

            cars = carManager2.GetByPrice(1700, 3000).Data;
            Console.WriteLine("\n------------GroupByPrice min= 1700 max=3000 ------------\n");
            ShowCarList(cars);

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
            cars = carManager2.GetAll().Data;
            ShowCarList(cars);

            Console.WriteLine("\n----------  CarDetails  ------------\n");
            List<CarDetailsDto> list = carManager2.GetCarDetails().Data;
            foreach (CarDetailsDto item in list)
            {
                // CarName, BrandName, ColorName, DailyPrice.
                Console.WriteLine("model: {0} barnd: {1} color: {2} price: {3}",
                    item.CarName.PadRight(20),item.BrandName.PadRight(10),
                    item.ColorName.PadRight(15),item.DailyPrice.ToString().PadRight(5));
            }
            

        }

        static void ShowCarList(List<Car> carList)
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

        static void showBrandList(List<Brand> list)
        {

            foreach (Brand c in list)
            {
                Console.WriteLine("id: {0}, BrandName: {1} ", c.id, c.BrandName);
            }
        }

        private static void InMemoryTest()
        {
            CarManager carManager = new CarManager(new InMemoryCarDal(),new BrandManager(new EfBrandDal())); 

            Console.WriteLine("---------------- GetAll cars ---------------------\n");
            List<Car> cars = carManager.GetAll().Data;
            ShowCarList(cars);

            Console.WriteLine("\n---------------- Add car ---------------------\n");

            carManager.Add(new Car() { 
                id = 4, DailyPrice = 3000, BrandId = 3, Description = "qwe",
                ModelYear = "2001", ColorId = 5 
            });
            cars = carManager.GetAll().Data;
            ShowCarList(cars);

            Console.WriteLine("\n-------------- delete car id=1 --------------------\n");

            carManager.Delete(1);
            cars = carManager.GetAll().Data;
            ShowCarList(cars);

            Console.WriteLine("\n-------------- Update car id=4 --------------------\n");
            carManager.Update(new Car() { id = 4, DailyPrice = 3800, BrandId = 3, Description = "updated 4", ModelYear = "2001", ColorId = 5 });
            cars = carManager.GetAll().Data;
            ShowCarList(cars);

        }
    }
}
