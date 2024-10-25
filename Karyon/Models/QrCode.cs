using System.Data.SqlClient;
using System.Data;
using ClosedXML.Excel;

namespace Karyon.Models
{
    public class QrCodeRequest : Menu
    {
        public int Fk_EventId { get; set; }

        public DataSet QrCodeStatus()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_MemId", Fk_MemId),
                new SqlParameter("@Fk_EventId", Fk_EventId)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.QrCodeStatus, para);
            return ds;
        }
    }
    public class QrCodeResponse
    {
        public string? QrStatus { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class ConfirmEventResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class ConfirmEventRequest
    {
        public int Fk_MemId { get; set; }
        public int Fk_EventId { get; set; }

        public DataSet ConfirmEventBooking()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_MemId", Fk_MemId),
                new SqlParameter("@FK_EventId", Fk_EventId)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.ConfirmEventBooking, para);
            return ds;
        }
    }
}
