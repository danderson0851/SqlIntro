using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    public class DapperProductRepository : IProductRepository
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
                return conn.Query<Product>("SELECT * FROM product");
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
                conn.Open();
                conn.Execute("DELETE FROM product WHERE ProductID > @id", new { id });
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
                conn.Open();
                conn.Execute("UPDATE product SET Name = @name WHERE ProductID = @id", new { name = prod.Name, id = prod.Id });
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
                conn.Open();
                conn.Execute("INSERT INTO product (name) values(@name)", new { name = prod.Name });
            }
        }

        public IEnumerable<Product> GetProductsAndReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>(
                    "SELECT product.Name, productreview.Rating FROM Product LEFT JOIN productreview ON productreview.productID = product.productID");
            }
        }
        public IEnumerable<Product> GetProductsWithReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("SELECT product.Name, productreview.Rating FROM Product INNER JOIN productreview ON productreview.productID = product.productID");
            }
        }





    }
}
