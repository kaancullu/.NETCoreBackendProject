using Business.Concrete;
using Entities.Concrete;
using DataAccess.Concrete.InMemory;
using System;
using System.Collections.Generic;
using DataAccess.Concrete.EntityFramework;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {

            // DTO ---> Data Transfer Object

            //ProductTest();

            CategoryTest();

            //ProductDtoTest();

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var i in categoryManager.GetAll().Data)
            {
                System.Console.WriteLine(i.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));

            List<Product> liste = productManager.GetByUnitPrice(40, 60).Data;

            foreach (var product in liste)
            {
                System.Console.WriteLine(product.ProductName);
            }
        }
        //private static void ProductDtoTest()
        //{
        //    ProductManager productManager = new ProductManager(new EfProductDal());

        //    var liste = productManager.GetProductDetails().Data;

        //    foreach (var product in liste)
        //    {
        //        System.Console.WriteLine(product.ProductName + "/" + product.CategoryName);
        //    }
        //}
    }
}
