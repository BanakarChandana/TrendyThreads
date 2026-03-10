using System.Data;
using Microsoft.Data.SqlClient;
using ThrendyThreads.Models;
using ThrendyThreads.DataLayer;

namespace ThrendyThreads.BusinessLayer
{
    public class BLContact
    {
        SqlServerDB db = new SqlServerDB();

        // INSERT CONTACT
        public string InsertContact(ContactModel contact)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@YourName", contact.YourName),
                new SqlParameter("@EmailAddress", contact.EmailAddress),
                new SqlParameter("@ReasonForContact", contact.ReasonForContact),
                new SqlParameter("@Subject", contact.Subject),
                new SqlParameter("@YourMessage", contact.YourMessage)
            };

            int result = db.ExecuteNonQuery("sp_InsertContact", CommandType.StoredProcedure, param);

            if (result > 0)
                return "Contact Submitted Successfully";
            else
                return "Contact Submission Failed";
        }
        // GET ALL CONTACTS
        public List<ContactModel> GetAllContacts()
        {
            List<ContactModel> contacts = new List<ContactModel>();

            DataTable dt = db.GetDataTable("sp_GetAllContacts", CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                ContactModel contact = new ContactModel
                {
                    ContactId = Convert.ToInt32(row["ContactId"]),
                    YourName = row["YourName"].ToString(),
                    EmailAddress = row["EmailAddress"].ToString(),
                    ReasonForContact = row["ReasonForContact"].ToString(),
                    Subject = row["Subject"].ToString(),
                    YourMessage = row["YourMessage"].ToString()
                };

                contacts.Add(contact);
            }

            return contacts;
        }

    }
}