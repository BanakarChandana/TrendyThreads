using Microsoft.Data.SqlClient;
using System.Data;
using ThrendyThreads.DataLayer;
using ThrendyThreads.Model;
using System.Collections.Generic;

namespace ThrendyThreads.BusinessLayer
{
    public class BLRegistration
    {
        private readonly SqlServerDB db = new SqlServerDB();

        // INSERT USER
        public int InsertRegistration(RegisterModel model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName", model.UserName),
                new SqlParameter("@Password", model.Password),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@Image", model.Image ?? (object)DBNull.Value),
            };

            return db.ExecuteNonQuery(
                "sp_InsertRegistration",
                CommandType.StoredProcedure,
                param
            );
        }

        // GET ALL USERS WITHOUT ID AND PASSWORD
        public List<RegisterModel> GetAllUsers()
        {
            List<RegisterModel> userList = new List<RegisterModel>();

            DataTable dt = db.GetDataTable(
                "sp_GetAllUsers",
                CommandType.StoredProcedure
            );

            if (dt == null || dt.Rows.Count == 0)
                return userList; // Return empty list if no users found

            foreach (DataRow row in dt.Rows)
            {
                RegisterModel user = new RegisterModel
                {
                    UserName = row["userName"]?.ToString(),
                    Email = row["Email"]?.ToString(),
                    Image = row["image"] != DBNull.Value ? (byte[])row["image"] : null
                };

                userList.Add(user);
            }

            return userList;
        }
    }
}