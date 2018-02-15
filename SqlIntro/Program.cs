﻿using System;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=localhost; Database=adventureworks; Uid=root; Pwd=password;"; //get connectionString format from connectionstrings.com and change to match your database

            var repo = new ProductRepository(connectionString);
            foreach (var prod in repo.GetProducts())
            {
                Console.WriteLine("Product Name: " + prod.Name);
            }
            

            //repo.DeleteProduct(316);
            //repo.UpdateProduct();

            Console.WriteLine("\nThe Program has ended, press any key to exit...");
            Console.ReadLine();
        }


    }
}
