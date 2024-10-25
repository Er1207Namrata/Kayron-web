using System.Data.SqlClient;
using System.Data;

namespace Karyon.Models
{
    public class DispatchedOrders : Menu
    {
        public List<DispatchedOrdersData>? OrdersList { get; set; }
    }
    public class DispatchedOrdersRequest : Common
    {
        public string? Fk_FranchiseId { get; set; }
        public string? OrderNo { get; set; }
        public DataSet GetFranchiseOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@Fk_ParentFranchiseId",Fk_FranchiseId),
                    new SqlParameter("@Size",Size),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@OrderNo",OrderNo==""?null:OrderNo)
                   
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
    public class DispatchedOrdersData
    {
         
        public int? DispatchCount { get;set;}
        public string? OrderNo { get;set;}
        public string? Fk_ParentFranchiseId { get;set;}
        public string? Name { get;set;}
        public string? MobileNo { get;set;}
        public string? OrderDate { get;set;}
        public string? DispatchedDate { get;set;}
        public string? Status { get;set;}
        public string? ParentContactPerson { get;set;}
        public decimal? OrderAmount { get;set;}
        public decimal? TotalPV { get;set;}

    }
    public class DispatchedOrdersResponse
    {
        public DispatchedOrders? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
}
