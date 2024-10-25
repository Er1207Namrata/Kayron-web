using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class UpdateMemberDetails : Menu
    {
        //public string? LoginId { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? PinCode { get; set; }
        //public string? FromDate { get; set; }
        //public string? ToDate { get; set; }
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public string? NewFirstName { get; set; }
        public string? NewMiddleName { get; set; }
        public string? NewLastName { get; set; }
        public string? NewMobileNo { get; set; }
        public string? SponsorLoginId { get; set; }
        public string? Fk_PerformanceLevelId { get; set; }
        //public DataTable? dtDetails { get; set; }
        public DataSet UpdateProfileByAdmin()
        {
            SqlParameter[] para = 
            {
                new SqlParameter("@NewFirstName",NewFirstName),
                new SqlParameter("@NewMiddleName",NewMiddleName),
                new SqlParameter("@NewLastName",NewLastName),
                new SqlParameter("@NewMobileNo",NewMobileNo),
                new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateProfileByAdmin,para);
            return ds;
        }
        public DataSet GetAssociateList()
        {
            SqlParameter[] para =
            {

                new SqlParameter("@Mobile",Mobile),
                new SqlParameter("@Email",Email),
                new SqlParameter("@PinCode",PinCode),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@Page",Page),
                new SqlParameter("@Size",Size)

            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAssociateDetails, para);
            return ds;
        }
        public DataSet BlockUnblockAssociate()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Addedby",AddedBy),
                new SqlParameter("@Fk_MemId",Fk_MemId),
                new SqlParameter("@Status",Status)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.BlockUnblockAssociate, para);
            return ds;
        }
        public DataSet UpdateRank()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@Fk_PerformanceLevelId",Fk_PerformanceLevelId)
        
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateRank, para);
            return ds;
        }
    }
}
