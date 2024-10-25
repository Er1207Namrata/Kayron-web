using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class FranchiseTypeRequest : Menu
    {
        //public string? OpCode { get; set; }
        public DataSet GetFranchiseeType()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode)
                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class FranchiseType
    {
        public string? FranchiseTypeName { get; set; }
        public string? FranchiseTypeId { get; set; }

    }
    public class FranchiseTypeResponse
    {
        public string? Message { get; set; }
        public int Status { get; set; }
        public List<FranchiseType>? Response { get; set; }
    }
}
