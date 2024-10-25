using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FranchiseePayout : Menu
    {
        public List<FranchiseePayoutData>? PayoutList { get; set; }
    }
    public class FranchiseePayoutDetails
    {
        public List<FranchiseePayoutDetailsData>? PayoutList { get; set; }
    }
    public class FranchiseePayoutRequest : Menu
    {
        public string? FranchiseId { get; set; }
        public string? PayoutNo { get; set; }
      
        public DataSet GetFranchiseePayoutDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_FranchiseId",FranchiseId),
                new SqlParameter("@PayoutNo",PayoutNo),
                new SqlParameter("@OpCode",OpCode),
                new SqlParameter("@Page",Page),
                new SqlParameter("@Size",Size)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchisePayoutDetails, para);
            return ds;
        }
    }
    public class FranchiseePayoutData
    {
        public int? PayoutNo { get; set; }
        public string? Month { get; set; }
        public decimal? TotalBusiness { get; set; }
        public decimal? Incentive { get; set; }
        public decimal? TDS { get; set; }
        public decimal? AdminCharges { get; set; }
        public decimal? Payable { get; set; }
        public int? TotalRecords { get; set; }
    }
    public class FranchiseePayoutResponse
    {
        public FranchiseePayout? Response { get; set; }
        public string? Message { get; set; }
        public int Status { get; set; }
    }
    public class FranchiseePayoutDetailsData
    {
        public string? IncomeType { get; set; }
        public string? OrderNo { get; set; }
        public string? FromLoginId { get; set; }
        public string? FromContactPerson { get; set; }
        public decimal? BusinessAmount { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? Amount { get; set; }
    }
    public class FranchiseePayoutDetailsResponse
    {
        public FranchiseePayoutDetails? Response { get; set; }
        public string? Message { get; set; }
        public int Status { get; set; }
    }
}
