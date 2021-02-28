using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;


namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //DTO : Data Transform Object

            ProductTest();

            //CategoryTest();

            
            Console.Read();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine("{0} => {1}", category.CategoryId, category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            /*foreach (var product in productManager.GetProductsDetails().Data)
            {
                Console.WriteLine(product.ProductName);
                Console.WriteLine(product.CategoryName);
                Console.WriteLine("--------------------------");
            }*/

            var result = productManager.GetAllByCategoryID(150);
            
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine(item.ProductName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}