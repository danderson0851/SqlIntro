using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM product ORDER BY Name LIMIT 25";
                conn.Open();
                var dr = cmd.ExecuteReader();
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
    }
}
