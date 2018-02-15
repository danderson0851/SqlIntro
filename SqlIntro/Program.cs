using System;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=localhost; Database=adventureworks; Uid=root; Pwd=password;"; //get connectionString format from connectionstrings.com and change to match your database
            Product prod2 = null;
            var repo = new ProductRepository(connectionString);
            foreach (var prod in repo.GetProducts())
            {
                //if (prod2 == null) { prod2 = prod; }

                Console.WriteLine("Product Name: " + prod.Name);
            }


            repo.DeleteProduct(879);

            prod2 = new Product
            {
                Id = 712,
                Name = ""
            };
            if (prod2 != null)
            {
                prod2.Name = "Original product name for ID 712";
                repo.UpdateProduct(prod2);
            }

            prod2.Name = "Blazers";
            prod2.Id = 1000;
            repo.InsertProduct(prod2);
            Console.WriteLine("\nThe Program has ended, press any key to exit...");
            Console.ReadLine();
        }


    }
}
