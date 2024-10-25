using System.Data;
using System.Data.SqlClient;
namespace Karyon.Models
{
    public class RewardDetails: Menu
    {
        public string? FkSetRewardId { get; set; }
        public long? CustomerId { get; set; }
        public string? Action { get; set; }
        public DataSet GetRewardDetails()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@FKMemId",CustomerId),
                                      new SqlParameter("@Fk_SetRewardId",FkSetRewardId),
                                      new SqlParameter("@Action",Action),
                                   

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.RewardDetails, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    public class Reward
    {
        public List<RewardData>? RewardList { get; set; }

    }
    public class RewardData
    {
        public decimal RewardAmount { get; set; }
        public decimal TargetPV { get; set; }
        public string? TargetPoint { get; set; }
        public decimal LeftBusiness { get; set; }
        public decimal RightBusiness { get; set; }
        public decimal CurrentBusiness { get; set; }
        public decimal BalanceBusiness { get; set; }
        public string? BalanceLeft { get; set; }
        public string? BalanceRight { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? Recognition { get; set; }
        public string? PaymentStatus { get; set; }
        public string? Status { get; set; }
        public string? AchievedOn { get; set; }
        public string? Rank { get; set; }
        public string? Remarks { get; set; }
        public string? Rcolor { get; set; }
        public string? ReferBy { get; set; }
        public bool? IsVisible { get; set; }
        public string? Action { get; set; }
        public string? Designation { get; set; }
        public long FkMemId { get; set; }
        public string? RewardImage { get; set; }

        public int FkSetRewardId { get; set; }
        public string? RewardName { get; set; }
        public decimal TargetAmount { get; set; }
        public int PK_RId { get; set; }
        public string? Msg { get; set; }
        public string? LoginId { get; set; }
        public string? RewardImg { get; set; }
        public string? ChequeDDBankName { get; set; }
        public string? BankBranch { get; set; }
        public string? ChequeDDNo { get; set; }
        public string? ChequeDDDate { get; set; }
        public string? PaidAmount { get; set; }
        public string? PaymentModeName { get; set; }
    }
    public class RewardReposne
    {

        public Reward? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
}
