using System.Data.SqlClient;
using System.Data;

namespace Karyon.Models
{
    public class PendingOrders
    {
        public List<PendingOrdersData>? OrdersList { get; set; }
    }
    public class PendingOrdersRequest : Menu
    {
        public string? Fk_FranchiseId { get; set; }
        public DataSet GetFranchiseOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@Fk_ParentFranchiseId",Fk_FranchiseId),
                    new SqlParameter("@Size",Size),
                    new SqlParameter("@Page",Page)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchiseOrders, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCustomerOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@Status",Status),
                    new SqlParameter("@Fk_DispatchId",Fk_FranchiseId),
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
    public class PendingOrdersData
    {
        //OrderNo DispatchCount   Name MobileNo    OrderDate OrderAmount TotalPV Status  TotalRecords Fk_DispachFranchiseId
        //Name MobileNo  Fk_DispachFranchiseId
        public string? Pk_OrderId { get; set; }
        public int? DispatchCount { get; set; }
        public string? Mobile { get; set; }
        public string? LoginId { get; set; }
        public string? CompanyName { get; set; }
        public string? Franchisee { get; set; }
        public string? OrderNo { get; set; }
        public string? OrderDate { get; set; }
        public decimal? OrderAmount { get; set; }
        public decimal? TotalPV { get; set; }
        public string? Status { get; set; }
        public string? ParentContactPerson { get; set; }
        public string? ParentCompanyName { get; set; }
        public string? ParentPinCode { get; set; }
        public string? Fk_ParentFranchiseId { get; set; }
    }
    public class PendingOrdersRersponse
    {
        public PendingOrders? Response { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
}
