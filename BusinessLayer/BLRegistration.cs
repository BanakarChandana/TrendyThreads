using Microsoft.Data.SqlClient;
using System;
using System.Data;
using ThrendyThreads.DataLayer;
using ThrendyThreads.Model;
using System.Collections.Generic;

namespace ThrendyThreads.BusinessLayer
{
    public class BLRegistration
    {
        private readonly SqlServerDB db = new SqlServerDB();

        // -------------------------------------------------
        // INSERT USER (Registration Table)
        // -------------------------------------------------
        public int InsertRegistration(RegisterModel model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName", model.UserName),
                new SqlParameter("@Password", model.Password),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@Image", model.Image ?? (object)DBNull.Value),
                new SqlParameter("@Role", "user")
            };

            return db.ExecuteNonQuery(
                "sp_InsertRegistration",
                CommandType.StoredProcedure,
                param
            );
        }

        // -------------------------------------------------
        // GET ALL USERS
        // -------------------------------------------------
        public List<RegisterModel> GetAllUsers()
        {
            List<RegisterModel> userList = new List<RegisterModel>();

            DataTable dt = db.GetDataTable(
                "sp_GetAllUsers",
                CommandType.StoredProcedure
            );

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    RegisterModel user = new RegisterModel
                    {
                        UserName = row["UserName"]?.ToString(),
                        Email = row["Email"]?.ToString(),
                        Image = row["Image"] != DBNull.Value ? (byte[])row["Image"] : null
                    };

                    userList.Add(user);
                }
            }

            return userList;
        }

        // -------------------------------------------------
        // INSERT DESIGNER + REGISTRATION (Admin Dashboard)
        // -------------------------------------------------
        public string InsertDesignerWithRegistration(AdminDesignerModel model)
        {
            try
            {
                // Insert Registration
                SqlParameter[] regParam = new SqlParameter[]
                {
                    new SqlParameter("@UserName", model.UserName),
                    new SqlParameter("@Password", model.Password),
                    new SqlParameter("@Email", model.Email),
                    new SqlParameter("@Image", model.Image ?? (object)DBNull.Value),
                    new SqlParameter("@Role","designer")
                };

                int regResult = db.ExecuteNonQuery(
                    "sp_InsertRegistration",
                    CommandType.StoredProcedure,
                    regParam
                );

                // Insert Designer
                SqlParameter[] desParam = new SqlParameter[]
                {
                    new SqlParameter("@DesignerName", model.DesignerName),
                    new SqlParameter("@DesignerEmail", model.Email),
                    new SqlParameter("@DesignerImage", model.Image ?? (object)DBNull.Value),
                    new SqlParameter("@AboutDesigner", model.AboutDesigner),
                    new SqlParameter("@PhoneNumber", model.PhoneNumber),
                    new SqlParameter("@Address", model.Address) 
                };

                int desResult = db.ExecuteNonQuery(
                    "sp_InsertDesigner",
                    CommandType.StoredProcedure,
                    desParam
                );

                if (regResult > 0 && desResult > 0)
                    return "Designer and Registration Added Successfully";

                return "Insert Failed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        // -------------------------------------------------
        // GET USERS (ONLY ROLE = USER)
        // -------------------------------------------------
        public List<RegisterModel> GetUsers()
        {
            List<RegisterModel> list = new List<RegisterModel>();

            try
            {
                DataTable dt = db.GetDataTable(
                    "sp_GetUsersByRole",
                    CommandType.StoredProcedure
                );

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new RegisterModel
                        {
                            UserId = Convert.ToInt32(row["UserId"]),
                            UserName = row["UserName"]?.ToString(),
                            Email = row["Email"]?.ToString(),
                            Image = row["Image"] != DBNull.Value ? (byte[])row["Image"] : null,
                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching users: " + ex.Message);
            }

            return list;
        }
        // -------------------------------------------------
        // GET DESIGNERS (ONLY ROLE = DESIGNER)
        // -------------------------------------------------
        public List<RegisterModel> GetDesigners()
        {
            List<RegisterModel> list = new List<RegisterModel>();

            try
            {
                DataTable dt = db.GetDataTable(
                    "sp_GetDesigners",
                    CommandType.StoredProcedure
                );

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new RegisterModel
                        {
                            UserId = Convert.ToInt32(row["UserId"]),
                            UserName = row["UserName"]?.ToString(),
                            Email = row["Email"]?.ToString(),
                            Image = row["Image"] != DBNull.Value ? (byte[])row["Image"] : null,
                  
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching designers: " + ex.Message);
            }

            return list;
        }
    }
}