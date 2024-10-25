using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class CustomerOrder : Menu
    {
        public string? Fk_VarientId { get; set; }
        public string? Fk_OrderId { get; set; }
        public string? OrderNo { get; set; }
        public string? OrderType { get; set; }
        public string? OrderDate { get; set; }
        public decimal? OrderAmount { get; set; }
        //public string? Status { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? CounterOTP { get; set; }
        public string? ShippingCharges { get; set; }
        public List<CustomerOrderData>? OrderDetailsList { get; set; }
    }
    public class CustomerOrderRequest
    {
        public string? OrderNo { get; set; }
        public DataSet GetOrderDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@OrderNo",OrderNo)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAssociateOrderAPI, para);
            return ds;
        }

    }

    public class CustomerOrderData
    {
       
        public string? Fk_VarientId { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal? PV { get; set; }
        public decimal? MRP { get; set; }
        public decimal? GST { get; set; }
        public string? Image { get; set; }       
        public string? Fk_DispachFranchiseId { get; set; }
        public List<RatingList>? RatingList { get; set; }
    }
    public class RatingList
    {
        public long? Star { get; set; }
        public string? Rating { get; set; }
        public string? AddedOn { get; set; }
    }
    public class CustomerOrderResponse
    {
        public CustomerOrder? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
}
