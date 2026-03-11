using Microsoft.Data.SqlClient;
using System.Data;
using ThrendyThreads.DataLayer;
using ThrendyThreads.Model;
using System.Collections.Generic;

namespace ThrendyThreads.BusinessLayer
{
    public class BLOutfitChangeRequest
    {
        SqlServerDB db = new SqlServerDB();

        // INSERT OUTFIT CHANGE REQUEST
        public int InsertOutfitChangeRequest(OutfitChangeRequestModel model)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@YourName", model.YourName),
                new SqlParameter("@PhoneNumber", model.PhoneNumber),
                new SqlParameter("@Description", model.Description),
                new SqlParameter("@ProductId", model.ProductId),
                new SqlParameter("@DesignerId", model.DesignerId)
            };

            return db.ExecuteNonQuery(
                "sp_InsertOutfitChangeRequest",
                CommandType.StoredProcedure,
                param
            );
        }

        // GET ALL OUTFIT CHANGE REQUESTS
        public List<OutfitChangeRequestModel> GetAllRequests()
        {
            List<OutfitChangeRequestModel> requestList = new List<OutfitChangeRequestModel>();

            DataTable dt = db.GetDataTable(
                "sp_GetAllOutfitChangeRequests",
                CommandType.StoredProcedure
            );

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    OutfitChangeRequestModel request = new OutfitChangeRequestModel
                    {
                        RequestId = Convert.ToInt32(row["RequestId"]),
                        YourName = row["YourName"]?.ToString(),
                        PhoneNumber = row["PhoneNumber"]?.ToString(),
                        Description = row["Description"]?.ToString(),
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        DesignerId = Convert.ToInt32(row["DesignerId"])
                    };

                    requestList.Add(request);
                }
            }

            return requestList;
        }
        public List<OutfitChangeRequestModel> GetRequestsByDesignerId(int designerId)
        {
            List<OutfitChangeRequestModel> list = new List<OutfitChangeRequestModel>();

            SqlParameter[] param = new SqlParameter[]
            {
        new SqlParameter("@DesignerId", designerId)
            };

            DataTable dt = db.GetDataTable(
                "sp_GetRequestsByDesignerId",
                CommandType.StoredProcedure,
                param
            );

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    OutfitChangeRequestModel model = new OutfitChangeRequestModel
                    {
                        RequestId = Convert.ToInt32(row["RequestId"]),
                        YourName = row["YourName"].ToString(),
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        Description = row["Description"].ToString(),
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        DesignerId = Convert.ToInt32(row["DesignerId"])
                    };

                    list.Add(model);
                }
            }

            return list;
        }
    }
}