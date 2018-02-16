using System;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=localhost; Database=adventureworks; Uid=root; Pwd=password;"; //get connectionString format from connectionstrings.com and change to match your database
            Product prod2 = null;
            var repo = new DapperProductRepository(connectionString);
            foreach (var prod in repo.GetProducts())
            {
                Console.WriteLine("Product Name: " + prod.Name);
            }


            repo.DeleteProduct(1040);

            prod2 = new Product
            {
                Id = 1000,
                Name = ""
            };
            if (prod2 != null)
            {
                prod2.Name = "This name was UPDATED!";
                prod2.Id = 1000;
                repo.UpdateProduct(prod2);
            }

            prod2.Name = "Here is a new product!";
            //prod2.Id = 1005;
            repo.InsertProduct(prod2);

            Console.WriteLine("\nThe Program has ended, press any key to exit...");
            Console.ReadLine();
        }


    }
}
