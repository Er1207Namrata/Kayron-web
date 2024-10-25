using System.Data.SqlClient;
using System.Data;

namespace Karyon.Models
{
    public class MasterData:Menu
    {
        public List<GetMasterData>? RequestList { get; set; }
    }
    public class GetMasterData
    {
        public string? Pk_Id { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }
       
    }
    public class MasterDataRequest
    {
        public int? OpCode { get; set; }
        public string? Value { get; set; }
        public int? FK_FranchiseId { get; set; }
        public DataSet RequestMasterData()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@OpCode",OpCode),
                                      new SqlParameter("@Value",Value==""?null:Value),
                                      new SqlParameter("@FK_FranchiseId",FK_FranchiseId)

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
    public class MasterDataResponse
    {
        public MasterData? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
}