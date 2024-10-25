using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class Transactions : Menu
    {
        //public string? LoginId { get; set; }
        public DataSet GetAssociateBankDetails()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    //new SqlParameter("@Page",Page),
                    //new SqlParameter("@Size",Size),
                };
                DataSet ds = Connection.ExecuteQuery("GetAssociateBankDetailsForPayout", para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
