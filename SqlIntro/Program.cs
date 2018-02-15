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
                Id = 843,
                Name = "Alpha Changed"
            };
            if (prod2 != null)
            {
                Console.WriteLine($"{prod2.Name} and {prod2.Id}");
                prod2.Name = "Chain Did Update for id 1";

                repo.UpdateProduct(prod2);
            }


            Console.WriteLine("\nThe Program has ended, press any key to exit...");
            Console.ReadLine();
        }


    }
}
