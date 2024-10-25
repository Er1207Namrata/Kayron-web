using Karyon.Models;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.Emit;

namespace Karyon.Models
{
    public class UploadDispatchDetails: Menu
    {
        public string? OrderNo { get; set; }
        public string? DocketNo { get; set; }
        public string? Transport { get; set; }
        public string? Date { get; set; }
        public string? Route { get; set; }
        public string? DispatchCount { get; set; }
        public string? JsonstringWallet { get; set; }
        public DataSet UpdateExcelData()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@Status",Status),
                    new SqlParameter("@Date",Date)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdateDispatchDetails, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet UploadExcelData()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@DocketNo",DocketNo),
                    new SqlParameter("@Transport",Transport),
                    new SqlParameter("@Route",Route),
                    new SqlParameter("@Date",Date),
                    new SqlParameter("@OpCode",OpCode)
                   
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.UploadDispatchDetails, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DispatchExcel()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@Fk_franchiseeId",Fk_EmpId),
                    new SqlParameter("@OpCode",OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchiseeDispatchList, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

