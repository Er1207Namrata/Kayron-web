
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Razorpay.Api;
using static QRCoder.PayloadGenerator.SwissQrCode.Contact;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.Emit;

namespace Karyon.Models.RazorPay
{
    public class RazCreateOrder
    {
        public int FK_MemId { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? PaymentMode { get; set; }
        public string? Type { get; set; }
        public string? OrderId { get; set; }
        public string? Request { get; set; }
        public DataSet CreateRazOrder()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@FK_MemId",FK_MemId),
                                      new SqlParameter("@TotalAmount",TotalAmount),
                                      new SqlParameter("@PaymentMode",PaymentMode),
                                      new SqlParameter("@Type",Type),
                                      new SqlParameter("@OrderId",OrderId),
                                      new SqlParameter("@Request",Request)
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.CreateRazOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class RazCreateOrderResponse
    {
        public RazCreateOrderRes? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class RazCreateOrderRes
    {
        public string? OrderId { get;set;}
    }

}
