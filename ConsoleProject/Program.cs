using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // InMemoryTest();

            EfCarDalTest();

        }

        private static void EfCarDalTest()
        {
            Console.WriteLine("\n---------------AllCars---------------\n");

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
                Name = "Exclusive E200",
                DailyPrice = 2500
            };
            carManager2.Add(c);
            Console.WriteLine("\n------------add car  ------------\n");
            cars = carManager2.GetAll();
            showCarList(cars);

        }

        static void showCarList(List<Car> carList)
        {
        
            foreach (var car in carList)
            {
                Console.WriteLine("id: {0},model: {6}, ModelYear: {1}, DailyPrice: {2}, ColorId: {3}, BrandId: {4}, description: {5} ", car.id, car.ModelYear, car.DailyPrice,car.ColorId,car.BrandId, car.Description,car.Name);
            }
        }

        private static void InMemoryTest()
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            List<Car> cars = carManager.GetAll();
            Console.WriteLine("---------------- GetAll cars ---------------------\n");
            showCarList(cars);

            Console.WriteLine("\n---------------- Add car ---------------------\n");

            carManager.Add(new Car() { id = 4, DailyPrice = 3000, BrandId = 3, Description = "qwe", ModelYear = "2001", ColorId = 5 });
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
