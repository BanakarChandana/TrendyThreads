using Microsoft.Data.SqlClient;
using System.Data;
using ThrendyThreads.DataLayer;
using ThrendyThreads.Model;

namespace ThrendyThreads.BusinessLayer
{
    public class BLRegistration
    {
        SqlServerDB db = new SqlServerDB();

        public int InsertRegistration(RegisterModel model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName", model.UserName),
                new SqlParameter("@Password", model.Password),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@Image", model.Image ?? (object)DBNull.Value),
                new SqlParameter("@Role", model.Role)
            };

            return db.ExecuteNonQuery(
                "sp_InsertRegistration",
                CommandType.StoredProcedure,
                param
            );
        }
    }
}