using System.Data;
using Microsoft.Data.SqlClient;
using ThrendyThreads.Models;
using ThrendyThreads.DataLayer;

namespace ThrendyThreads.BusinessLayer
{
    public class BLProduct
    {
        SqlServerDB db = new SqlServerDB();

        // INSERT PRODUCT
        public string InsertProduct(ProductModel product)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@ProductImage", product.ProductImage ?? (object)DBNull.Value),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Category", product.Category),
                new SqlParameter("@DesignerId", product.DesignerId)
            };

            int result = db.ExecuteNonQuery("sp_InsertProduct", CommandType.StoredProcedure, param);

            if (result > 0)
                return "Product Added Successfully";
            else
                return "Product Insert Failed";
        }

        // GET ALL PRODUCTS
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> products = new List<ProductModel>();

            DataTable dt = db.GetDataTable("sp_GetAllProducts", CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                ProductModel product = new ProductModel
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductName = row["ProductName"].ToString(),
                    ProductImage = row["ProductImage"] as byte[],
                    Price = Convert.ToDecimal(row["Price"]),
                    Category = row["Category"].ToString(),
                    DesignerId = Convert.ToInt32(row["DesignerId"])
                };

                products.Add(product);
            }

            return products;
        }

        // GET PRODUCT BY ID
        public ProductModel GetProductById(int id)
        {
            ProductModel product = null;

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProductId", id)
            };

            DataTable dt = db.GetDataTable("sp_GetProductById", CommandType.StoredProcedure, param);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                product = new ProductModel
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductName = row["ProductName"].ToString(),
                    ProductImage = row["ProductImage"] as byte[],
                    Price = Convert.ToDecimal(row["Price"]),
                    Category = row["Category"].ToString(),
                    DesignerId = Convert.ToInt32(row["DesignerId"])
                };
            }

            return product;
        }

        // GET RECENT PRODUCTS (TOP 4)
        public List<ProductModel> GetRecentProducts()
        {
            List<ProductModel> products = new List<ProductModel>();

            DataTable dt = db.GetDataTable("GetRecentProducts", CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                ProductModel product = new ProductModel
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductName = row["ProductName"].ToString(),
                    ProductImage = row["ProductImage"] as byte[],
                    Price = Convert.ToDecimal(row["Price"]),
                    Category = row["Category"].ToString(),
                    DesignerId = Convert.ToInt32(row["DesignerId"])
                };

                products.Add(product);
            }

            return products;
        }

        // DELETE PRODUCT
        public string DeleteProduct(int id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProductId", id)
            };

            int result = db.ExecuteNonQuery("sp_DeleteProduct", CommandType.StoredProcedure, param);

            if (result > 0)
                return "Product Deleted Successfully";
            else
                return "Product Delete Failed";
        }
    }
}