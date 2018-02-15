﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    class DapperProductRepository
    {
        private readonly string _connectionString;

        public DapperProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Reads all the products from the products table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "SELECT * FROM product ORDER BY Name LIMIT 25";
                var parameters = new { id = 1 };
                var products = conn.Query(sql, parameters);
                var firstProduct = products.First();

                /*
                 var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM product";
                */

                var dr = products.First();
                while (dr.Read())
                {
                    yield return new Product
                    {
                        Name = dr["Name"].ToString(),
                        Id = int.Parse(dr["ProductId"].ToString())
                    };
                }
            }
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = "DELETE FROM product WHERE ProductID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            //This is annoying and unnecessarily tedious for large objects.
            //More on this in the future...  Nothing to do here..
            using (var conn = new MySqlConnection(_connectionString))
            {
                var cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = "UPDATE product SET Name = @name WHERE ProductID = @id";
                cmd.Parameters.AddWithValue("@name", prod.Name);
                cmd.Parameters.AddWithValue("@id", prod.Id);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = "INSERT INTO product (Name) values(@name)";
                cmd.Parameters.AddWithValue("@name", prod.Name);
                cmd.Parameters.AddWithValue("@id", prod.Id);
                cmd.ExecuteNonQuery();
            }
        }


    }
}
