﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;


namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var product in productManager.GetByUnitPrice(40,100))
            {
                Console.WriteLine(product.ProductName);
                Console.WriteLine(product.UnitPrice);
                Console.WriteLine("");
            }

            Console.Read();
        }
    }
}
