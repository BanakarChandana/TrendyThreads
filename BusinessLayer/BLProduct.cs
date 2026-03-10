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
            SqlParameter[] param =
            {
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@ProductImage", product.ProductImage ?? (object)DBNull.Value),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Category", product.Category),
                new SqlParameter("@DesignerId", product.DesignerId)
            };

            int result = db.ExecuteNonQuery("sp_InsertProduct", CommandType.StoredProcedure, param);

            return result > 0 ? "Product Added Successfully" : "Product Insert Failed";
        }

        // GET ALL PRODUCTS
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> products = new List<ProductModel>();

            DataTable dt = db.GetDataTable("sp_GetAllProducts", CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                products.Add(MapProduct(row));
            }

            return products;
        }

        // GET PRODUCT BY ID
        public ProductModel? GetProductById(int id)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@ProductId", id)
            };

            DataTable dt = db.GetDataTable("sp_GetProductById", CommandType.StoredProcedure, param);

            if (dt.Rows.Count > 0)
            {
                return MapProduct(dt.Rows[0]);
            }

            return null;
        }

        // GET PRODUCTS BY DESIGNER ID
        public List<ProductModel> GetProductsByDesignerId(int designerId)
        {
            List<ProductModel> products = new List<ProductModel>();

            SqlParameter[] param =
            {
                new SqlParameter("@DesignerId", designerId)
            };

            DataTable dt = db.GetDataTable("sp_GetProductsByDesignerId", CommandType.StoredProcedure, param);

            foreach (DataRow row in dt.Rows)
            {
                products.Add(MapProduct(row));
            }

            return products;
        }

        // GET RECENT PRODUCTS (TOP 4)
        public List<ProductModel> GetRecentProducts()
        {
            List<ProductModel> products = new List<ProductModel>();

            DataTable dt = db.GetDataTable("GetRecentProducts", CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                products.Add(MapProduct(row));
            }

            return products;
        }

        // DELETE PRODUCT
        public string DeleteProduct(int id)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@ProductId", id)
            };

            int result = db.ExecuteNonQuery("sp_DeleteProduct", CommandType.StoredProcedure, param);

            return result > 0 ? "Product Deleted Successfully" : "Product Delete Failed";
        }

        // COMMON METHOD (MAP DATA ROW TO PRODUCT MODEL)
        private ProductModel MapProduct(DataRow row)
        {
            return new ProductModel
            {
                ProductId = Convert.ToInt32(row["ProductId"]),
                ProductName = row["ProductName"]?.ToString(),
                ProductImage = row["ProductImage"] as byte[],
                Price = Convert.ToDecimal(row["Price"]),
                Category = row["Category"]?.ToString(),
                DesignerId = Convert.ToInt32(row["DesignerId"])
            };
        }
    }
}