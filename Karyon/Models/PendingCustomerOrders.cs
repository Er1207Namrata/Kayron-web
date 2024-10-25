using System.Data.SqlClient;
using System.Data;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Karyon.Models
{

    public class PendingCustomerOrders
    {
        public List<PendingCustomerOrdersData>? OrdersList { get; set; }
    }
    public class PendingCustomerOrdersRequest : Menu
    {
        public string? Fk_DispatchId { get; set; }
        public string? OrderNo { get; set; }
        public DataSet GetCustomerOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@Status",Status),
                    new SqlParameter("@Fk_DispatchId",Fk_DispatchId),
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@Size",Size),
                    new SqlParameter("@Page",Page)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCustomerOrders, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class PendingCustomerOrdersData
    {
        public int? DispatchCount { get; set; }
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public string? OrderNo { get; set; }
        public string? OrderDate { get; set; }
        public decimal? OrderAmount { get; set; }
        public decimal? TotalPV { get; set; }
        public string? Status { get; set; }
        public string? Fk_ParentFranchiseId { get; set; }
    }
    public class PendingCustomerOrdersResponse
    {
        public PendingCustomerOrders? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
}
