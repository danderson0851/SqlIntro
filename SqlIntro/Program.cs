using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            Product prod2 = null;
            var repo = new DapperProductRepository(connectionString);
            
            

            prod2 = new Product
            {
                Id = 1000,
                Name = ""
            };
            if (prod2 != null)
            {
                prod2.Name = "update was successful";
                prod2.Id = 1028;
                repo.UpdateProduct(prod2);
            }
            prod2.Name = "Here is a new product!";
            prod2.Id = 1005;
            repo.InsertProduct(prod2);
            repo.DeleteProduct(1028);
            foreach (var prod in repo.GetProducts())
            {
                Console.WriteLine("Product Name: " + prod.Name);
            }
            foreach (var prod in repo.GetProductsAndReview())
            {
                if (prod2 == null) { prod2 = prod; };

                Console.WriteLine(prod.Name);
            }
            repo.GetProductsWithReview();
            repo.GetProductsAndReview();
            Console.WriteLine("\nThe Program has ended, press any key to exit...");
            Console.ReadLine();
        }
    }
}
