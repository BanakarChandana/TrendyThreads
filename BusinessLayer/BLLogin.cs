using System.Data;
using Microsoft.Data.SqlClient;
using ThrendyThreads.Model;
using ThrendyThreads.DataLayer;

namespace ThrendyThreads.BusinessLayer
{
    public class BLLogin
    {
        SqlServerDB db = new SqlServerDB();

        public bool LoginUser(LoginModel login)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@Email", login.Email),
                new SqlParameter("@Password", login.Password)
            };

            DataTable dt = db.GetDataTable("sp_LoginUser", CommandType.StoredProcedure, param);

            return dt.Rows.Count > 0;
        }
    }
}