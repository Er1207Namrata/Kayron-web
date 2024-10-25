using System.Data.SqlClient;
using System.Data;

namespace Karyon.Models
{
    public class DispatchedOrderDetails : Menu
    {
        public List<DispatchedOrderDetailsData>? OrdersList { get; set; }
    }
    public class DispatchedOrderDetailsRequest : Common
    {
        public string? OrderNo { get; set; }
        public string? Type { get; set; }
        public string? Fk_FranchiseId { get; set; }
        public DataSet GetFranchiseOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@Fk_ParentFranchiseId",Fk_FranchiseId),
                    new SqlParameter("@Type",Type)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchiseOrders, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    public class DispatchedOrderDetailsData
    {
        public string? OrderNo { get; set; }
        public string? ProductName { get; set; }
        public decimal? Amount { get; set; }
        public decimal? PV { get; set; }
        public int? Quantity { get; set; }
        public decimal? SubTotal { get; set; }
        public int? Fk_ProductId { get; set; }
        public int? Fk_OrderId { get; set; }
        public string? IsDispatched { get; set; }
        public int? PendingQty { get; set; }
        public string? CompanyName { get; set; }
        public int? DispatchQty { get; set; }
        public int? Stock { get; set; }
    }
    public class DispatchedOrderDetailsResponse
    {
        public DispatchedOrderDetails? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
}

