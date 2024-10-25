using System.Data.SqlClient;
using System.Data;

namespace Karyon.Models
{
    public class ViewFranchiseeOrders
    {
        public List<ViewOrdersData>? OrderList { get; set; }
    }
   
    public class ViewOrdersRequest : Menu
    {
        public string? FranchiseId { get; set; }
        public string? OrderNo { get; set; }
        public DataSet ViewOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@FranchiseId",FranchiseId),
                    new SqlParameter("@OrderNo",OrderNo==""?null:OrderNo),
                    new SqlParameter("@Status",Status==""?null:Status)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ViewFranchiseOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    public class ViewOrdersData
    {
        public string? OrderNo { get; set; }
        public string? OrderDate { get; set; }
        public decimal? TotalPV { get; set; }
        public decimal? OrderAmount { get; set; }
        public string? OrderStatus { get; set; }
        public string? Invoice { get; set; }
        public string? ParentFranchiseId { get; set; }
        public List<OrderDetailsData>? OrderDetails { get; set; }

    }
    public class OrderDetailsData
    {
        public string? ProductName { get; set; }
        public string? Fk_VarientId { get; set; }
        public string? DispatchStatus { get; set; }
        public string? Fk_OrderId { get; set; }
        public string? CompanyName { get; set; }
        public string? DispatchFranchisee { get; set; }
        public decimal? MRP { get; set; }
        public decimal? PV { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Qty { get; set; }
        public int? PendingQty { get; set; }
        public int? DispatchQty { get; set; }
        public int? Stock { get; set; }
        public int? Bottles { get; set; }
    }
    public class ViewOrdersResponse
    {
        public ViewFranchiseeOrders? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }

    }
 
}
