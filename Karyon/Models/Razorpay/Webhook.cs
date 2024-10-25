using iTextSharp.text.pdf.parser.clipper;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.Emit;

namespace Karyon.Models.Razorpay
{
    public class Webhook
    {
        public int Fk_MemId { get; set; }
        public string? DocumentContent { get; set; }
        public string? PaymentId { get; set; }
        public string? OrderId { get; set; }
        public int OpCode { get; set; }

        public DataSet SaveWebhookData()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_MemId", Fk_MemId),
                new SqlParameter("@DocumentContent", DocumentContent),
                new SqlParameter("@PaymentId", PaymentId),
                new SqlParameter("@OpCode", OpCode)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.SaveWebHookRazorpay, para);
            return ds;
        }
        public DataSet GetPaymentDetails()
        {
            SqlParameter[] para = {
               
                new SqlParameter("@OrderId", OrderId)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetPaymentDetailRaz, para);
            return ds;
        }
    }
}
