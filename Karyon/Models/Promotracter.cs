using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class Promotracter: Menu
    {
        public string? TargetPVFrom { get; set; }
        public string? TargetPVTo { get; set; }
        public string? DirectLeg { get; set; }
        public string? ImageUrl { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? AchiveStatus { get; set; }
        public string? PK_LevelId { get; set; }
        public string? AchivedBusiness { get; set; }
        public string? TotalAchivedCount { get; set; }
        
        public DataSet GetPromotracker()
        {
            try
            {
                SqlParameter[] sp = new SqlParameter[]
                {
                    new SqlParameter("@FK_MemId",Fk_MemId)
                };
                DataSet ds = Connection.ExecuteQuery("PromoTracker", sp);
                return ds;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public DataSet GetPendingPromotracker()
        {
            try
            {
                SqlParameter[] sp = new SqlParameter[]
                {
                    new SqlParameter("@FK_MemId",Fk_MemId),
                    new SqlParameter("@Fk_PerformanceLevelId",PK_LevelId)
                };
                DataSet ds = Connection.ExecuteQuery("GetPromoTrackerDetails", sp);
                return ds;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
    public class PromoTracterResponse:Menu
    {
       public PromotackerLit?  Response {  get; set; }
       public string? message  {  get; set; }
    }

    public class PromotackerLit
    {
        public List<Promotracter>? list { get; set; }
    }
}
