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
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ProductName", product.ProductName ?? (object)DBNull.Value),
                    new SqlParameter("@ProductImage", product.ProductImage ?? (object)DBNull.Value),
                    new SqlParameter("@Price", product.Price),
                    new SqlParameter("@Category", product.Category ?? (object)DBNull.Value),
                    new SqlParameter("@DesignerId", product.DesignerId)
                };

                int result = db.ExecuteNonQuery("sp_InsertProduct", CommandType.StoredProcedure, param);

                return result > 0 ? "Product Added Successfully" : "Product Insert Failed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET ALL PRODUCTS
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> products = new List<ProductModel>();

            try
            {
                DataTable dt = db.GetDataTable("sp_GetAllProducts", CommandType.StoredProcedure);

                foreach (DataRow row in dt.Rows)
                {
                    products.Add(MapProduct(row));
                }
            }
            catch
            {
            }

            return products;
        }

        // GET PRODUCT BY ID
        public ProductModel? GetProductById(int id)
        {
            try
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
            }
            catch
            {
            }

            return null;
        }

        // GET PRODUCTS BY DESIGNER ID
        public List<ProductModel> GetProductsByDesignerId(int designerId)
        {
            List<ProductModel> products = new List<ProductModel>();

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DesignerId", designerId)
                };

                DataTable dt = db.GetDataTable("sp_GetProductsByDesignerId", CommandType.StoredProcedure, param);

                foreach (DataRow row in dt.Rows)
                {
                    products.Add(MapProduct(row));
                }
            }
            catch
            {
            }

            return products;
        }

        // GET RECENT PRODUCTS
        public List<ProductModel> GetRecentProducts()
        {
            List<ProductModel> products = new List<ProductModel>();

            try
            {
                DataTable dt = db.GetDataTable("GetRecentProducts", CommandType.StoredProcedure);

                foreach (DataRow row in dt.Rows)
                {
                    products.Add(MapProduct(row));
                }
            }
            catch
            {
            }

            return products;
        }

        // DELETE PRODUCT
        public string DeleteProduct(int id)
        {
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ProductId", id)
                };

                int result = db.ExecuteNonQuery("sp_DeleteProduct", CommandType.StoredProcedure, param);

                return result > 0 ? "Product Deleted Successfully" : "Product Delete Failed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // UPDATE CART
        public string UpdateCart(int productId, int cartId)
        {
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ProductId", productId),
                    new SqlParameter("@CartId", cartId)
                };

                int result = db.ExecuteNonQuery("sp_UpdateCart", CommandType.StoredProcedure, param);

                return result > 0 ? "Cart Updated Successfully" : "Cart Update Failed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET CART PRODUCTS
        public List<ProductModel> GetCartProducts(int userId)
        {
            List<ProductModel> products = new List<ProductModel>();

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CartId", userId)
                };

                DataTable dt = db.GetDataTable("sp_GetCartProducts", CommandType.StoredProcedure, param);

                foreach (DataRow row in dt.Rows)
                {
                    products.Add(MapProduct(row));
                }
            }
            catch
            {
            }

            return products;
        }

        // MAP DATAROW TO PRODUCT MODEL
        private ProductModel MapProduct(DataRow row)
        {
            ProductModel product = new ProductModel();

            product.ProductId = Convert.ToInt32(row["ProductId"]);
            product.ProductName = row["ProductName"]?.ToString();
            product.ProductImage = row["ProductImage"] as byte[];
            product.Price = Convert.ToDecimal(row["Price"]);
            product.Category = row["Category"]?.ToString();
            product.DesignerId = Convert.ToInt32(row["DesignerId"]);

            if (row.Table.Columns.Contains("CartId") && row["CartId"] != DBNull.Value)
                product.CartId = Convert.ToInt32(row["CartId"]);
            else
                product.CartId = 0;

            return product;
        }
    }
}