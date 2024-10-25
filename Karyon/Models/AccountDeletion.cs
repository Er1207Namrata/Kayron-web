using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class AccountDeletionRequest:Menu
    {
        public string? CustomerId { get; set; }

        public DataSet DeleteAccount()
        {
            SqlParameter[] para = {
                new SqlParameter("@CustomerId",CustomerId)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteAccount, para);
            return ds;
        }
    }
    public class AccountDeletionResponse
    {
        public string? Message { get; set; }
        public int Status { get; set; }
    }
}
