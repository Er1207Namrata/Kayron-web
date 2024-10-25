using System.Data.SqlClient;
using System.Data;

namespace Karyon.Models
{
    public class CustomerDispatchedOrders : Menu
    {
        public List<CustomerDispatchedOrdersData>? OrderList { get; set; }

    }
    public class CustomerDispatchedOrdersRequest : Common
    {
        public string? Fk_DispatchId { get; set; }
        public DataSet GetCustomerOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@Fk_DispatchId",Fk_DispatchId),
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
    public class CustomerDispatchedOrdersData
    {

        public string? OrderNo { get; set; }
        public int? DispatchCount { get; set; }
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public string? OrderDate { get; set; }
        public decimal? OrderAmount { get; set; }
        public decimal? TotalPV { get; set; }
        public string? Status { get; set; }
        public string? ParentContactPerson { get; set; }
        public string? Fk_DispatchFranchiseId { get; set; }

    }
    public class CustomerDispatchedOrdersResponse
    {
        public CustomerDispatchedOrders? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
}
