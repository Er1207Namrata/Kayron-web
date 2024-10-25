using System.Data.SqlClient;
using System.Data;

namespace Karyon.Models
{
    public class ViewCustomerOrder
    {
        public List<ViewCustomerOrderData>? OrderList { get; set; }
    }
    public class ViewCustomerOrderRequest : Menu
    {
        public string? FranchiseId { get; set; }
        public string? OrderNo { get; set; }
     
       
        public DataSet ViewOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@FranchiseId",FranchiseId),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@OrderNo",OrderNo==""? null:OrderNo),
                    new SqlParameter("@Status",Status==""? null:Status)
                    
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.ViewCustomerOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class ViewCustomerOrderData
    {
        public string? OrderNo { get; set; }
        public string? OrderDate { get; set; }
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public decimal? TotalPV { get; set; }
        public decimal? OrderAmount { get; set; }
        public string? OrderStatus { get; set; }
        public List<CustomerOrderDetails>? OrderDetails { get; set; }
    }
    public class CustomerOrderDetails
    {
        public string? ProductName { get; set; }
        public decimal? MRP { get; set; }
        public decimal? PV { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Qty { get; set; }

    }

    public class ViewCustomerOrderResponse
    {
        public ViewCustomerOrder? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }


    }

}
