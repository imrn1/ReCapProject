using Business.Concrete;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemory());

            Console.WriteLine("---------------- GetAll cars ---------------------\n");
            List<Car> cars = carManager.GetAll();
            showCarList(cars);

            Console.WriteLine("\n---------------- Add car ---------------------\n");

            carManager.Add(new Car() { id = 4, DailyPrice = 3000, BrandId = 3, Description = "qwe", ModelYear = "2001", ColorId = 5 });

            showCarList(cars);

            Console.WriteLine("\n-------------- delete car id=1 --------------------\n");

            carManager.Delete(1);
            showCarList(cars);

            Console.WriteLine("\n-------------- Update car id=4 --------------------\n");
            carManager.Update(new Car() { id = 4, DailyPrice = 3800, BrandId = 3, Description = "updated 4", ModelYear = "2001", ColorId = 5 });
            showCarList(cars);

            Console.WriteLine("\n-------------- getById car id=2 --------------------\n");
            Car c = carManager.GetById(4);
            Console.WriteLine("id: {0}, description: {1}, ModelYear: {2}, DailyPrice: {3}", c.id, c.Description, c.ModelYear, c.DailyPrice);


            void showCarList(List<Car> carList)
            {

                carList = carManager.GetAll();
                foreach (var car in carList)
                {
                    Console.WriteLine("id: {0}, description: {1}, ModelYear: {2}, DailyPrice: {3}", car.id, car.Description, car.ModelYear, car.DailyPrice);
                }
            }

        }

       
    }
}
