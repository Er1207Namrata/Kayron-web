using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class Income : Menu
    {
        public int? CustomerId { get; set; }
        public DataSet GetTodaysIncome()
        {
            SqlParameter[] para = {
                new SqlParameter("@CustomerId", CustomerId)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
            return ds;
        }
    }
    public class IncomeResponse
    {
        public IncomeData? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class IncomeData
    {
        public string? FromId { get; set; }
        public string? ToId { get; set; }
        public string? IncomeType { get; set; }
        public decimal? Amount { get; set; }
    }
}
