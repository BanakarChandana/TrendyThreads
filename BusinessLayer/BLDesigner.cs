using System.Data;
using Microsoft.Data.SqlClient;
using ThrendyThreads.Models;
using ThrendyThreads.DataLayer;

namespace ThrendyThreads.BusinessLayer
{
    public class BLDesigner
    {
        SqlServerDB db = new SqlServerDB();

        // INSERT DESIGNER
        public string InsertDesigner(DesignerModel designer)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DesignerName", designer.DesignerName),
                new SqlParameter("@DesignerEmail", designer.DesignerEmail),
                new SqlParameter("@DesignerImage", designer.DesignerImage ?? (object)DBNull.Value),
                new SqlParameter("@AboutDesigner", designer.AboutDesigner),
                new SqlParameter("@PhoneNumber", designer.PhoneNumber),
                new SqlParameter("@Address", designer.Address)
            };

            int result = db.ExecuteNonQuery("sp_InsertDesigner", CommandType.StoredProcedure, param);

            if (result > 0)
                return "Designer Added Successfully";
            else
                return "Designer Insert Failed";
        }

        // GET ALL DESIGNERS
        public List<DesignerModel> GetAllDesigners()
        {
            List<DesignerModel> designers = new List<DesignerModel>();

            DataTable dt = db.GetDataTable("sp_GetAllDesigners", CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                DesignerModel designer = new DesignerModel
                {
                    DesignerId = Convert.ToInt32(row["DesignerId"]),
                    DesignerName = row["DesignerName"].ToString(),
                    DesignerEmail = row["DesignerEmail"].ToString(),
                    DesignerImage = row["DesignerImage"] as byte[],
                    AboutDesigner = row["AboutDesigner"].ToString(),
                    PhoneNumber = row["PhoneNumber"].ToString(),
                    Address = row["Address"].ToString()
                };

                designers.Add(designer);
            }

            return designers;
        }

        // GET DESIGNER BY ID
        public DesignerModel GetDesignerById(int id)
        {
            DesignerModel designer = null;

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DesignerId", id)
            };

            DataTable dt = db.GetDataTable("sp_GetDesignerById", CommandType.StoredProcedure, param);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                designer = new DesignerModel
                {
                    DesignerId = Convert.ToInt32(row["DesignerId"]),
                    DesignerName = row["DesignerName"].ToString(),
                    DesignerEmail = row["DesignerEmail"].ToString(),
                    DesignerImage = row["DesignerImage"] as byte[],
                    AboutDesigner = row["AboutDesigner"].ToString(),
                    PhoneNumber = row["PhoneNumber"].ToString(),
                    Address = row["Address"].ToString()
                };
            }

            return designer;
        }

        // DELETE DESIGNER
        public string DeleteDesigner(int id)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DesignerId", id)
            };

            int result = db.ExecuteNonQuery("sp_DeleteDesigner", CommandType.StoredProcedure, param);

            if (result > 0)
                return "Designer Deleted Successfully";
            else
                return "Designer Delete Failed";
        }
    }
}