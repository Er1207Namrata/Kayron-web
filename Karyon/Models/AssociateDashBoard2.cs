using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class AssociateDashBoard2Request : Menu
    {
        public int CustomerId { get; set; }
        public int? TotalMemberR { get; set; }
        public int? TotalActiveMemberR { get; set; }
        public int? TotalInactiveMemberR { get; set; }
        public int? TotalMember { get; set; }
        public int? TotalActiveMember { get; set; }
        public int? TotalInactiveMember { get; set; }
        public decimal? LeftBVFresh { get; set; }
        public decimal? RightBVFresh { get; set; }
        public string? FestivalPopup { get; set; }
        public string? FranchiseTypeId { get; set; }
        public decimal? SponsorLeftBVFresh { get; set; }
        public decimal? SponsorRightBVFresh { get; set; }

       
        public DataSet GetAssociateDashBoard()
        {
            try
            {
                SqlParameter[] para = {

                    new SqlParameter("@Fk_MemId",CustomerId),

                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAssociateDashboardNew, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
     
    }
  
    

    //public class DashBoard2Response
    //{
    //    public DashBoard? Response { get; set; }
    //    public int? Status { get; set; }
    //    public string? Message { get; set; }
    //}
    public class AssociateDashBoard2Response
    {
        public AssociateDashBoard2? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class AssociateDashBoard2ResponseWeb : Menu
    {
        public AssociateDashBoard2? Response { get; set; }
        //public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class AssociateDashBoard2
    {
       
        public List<TeamDetails>? lstAssociates { get; set; }
        public string? MemberStatus { get; set; }
        public string? BusinessStatus { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? LevelPercentage { get; set; }
        public string? NextLevel { get; set; }
        public string? TargetPVTo { get; set; }
        public string? AchivedPV { get; set; }
        public string? BalancePv { get; set; }
        public string? MaxLegId { get; set; }
        public string? MaxLegBusiness { get; set; }
        public string? TotalPV { get; set; }
        public string? SelfUIDPV { get; set; }
        public string? OtherPv { get; set; }
        public string? SelfBusiness { get; set; }
        public string? UIDBusiness { get; set; }
        public string? TeamBusiness { get; set; }
        public string? IsBid { get; set; }
        public string? MobileNo { get; set; }
        public string? UnderLevelCount { get; set; }
        public string? CurrentSelfBusiness { get;  set; }
        public string? CurrentUIDBusiness { get;  set; }
        public string? CurrentTeamBusiness { get;  set; }
        public string? CurrentTotalPv { get;  set; }
        public string? PreviousSelfBusiness { get;  set; }
        public string? PreviousUIDBusiness { get;  set; }
        public string? PreviousTeamBusiness { get;  set; }
        public string? Consideredbusiness { get;  set; }
        public string? Message { get;  set; }
        public string? UnderAchivedCount { get; set; }
        public string? OtherUnderLevelCount { get; set; }
        public string? OtherUnderAchivedCount { get; set; }
    }

   

    public class TeamDetails
    {
        public string? Name { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? Business { get; set; }
        public string? NextLevel { get; set; }
        public string? Target { get; set; }
    }


    
    public class BonusIncome
    {
        public string IncomeType { get; set; } 
        public string Target { get; set; } 
        public string Achieved { get; set; } 
        public string Balance { get; set; } 
        public string Status { get; set; } 
    }
    
}
