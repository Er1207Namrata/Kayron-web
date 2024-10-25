using System.Data.SqlClient;
using System.Data;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text;

namespace Karyon.Models
{
    public class CustomerDispatchedOrderDetails : Menu
    {
        public List<CustomerDispatchedOrderDetailsData>? OrderDetails { get; set; }
    }
    public class CustomerDispatchedOrderDetailsRequest : Common
    {
        public string? OrderNo { get; set; }
        public string? Type { get; set; }
        public string? Fk_DispatchId { get; set; }
        public DataSet GetCustomerOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@Fk_DispatchId",Fk_DispatchId),
                    new SqlParameter("@Type",Type)
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
    public class CustomerDispatchedOrderDetailsData
    {
        public string? OrderNo { get; set; }
        public string? Fk_OrderId { get; set; }
        public string? Fk_VarientId { get; set; }
        public string? ProductName { get; set; }
        public string? IsDispatched { get; set; }
        public string? CompanyName { get; set; }
        public string? CustomerName { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public string? PaymentMode { get; set; }
        public string? PurchseBy { get; set; }
        public decimal? MRP { get; set; }
        public decimal? PV { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Quantity { get; set; }
        public int? Stock { get; set; }
        public int? PendingQty { get; set; }
        public int? DispatchQty { get; set; }
    }
    public class CustomerDispatchedOrderDetailsResponse
    {
        public CustomerDispatchedOrderDetails? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }

    }
}
