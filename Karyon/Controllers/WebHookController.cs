using iTextSharp.text.pdf;
using Karyon.Models;
using Karyon.Models.Razorpay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using static Karyon.Models.Common;

namespace Karyon.Controllers
{
    [ApiController]
    [Route("api/WebHook/")]
    public class WebHookController : ControllerBase
    {
        [HttpPost("Payment")]
        [Produces("application/json")]
        public async Task<CommonResponse> PaymentAsync()
        {
            string type = "";
            string Fk_MemId = "";
            string Status = "";
            int ResponseCode = 0;
            Webhook webhook = new Webhook();
            CommonResponse commonResponse = new CommonResponse();
             
            dynamic? objreq = null;
            string documentContents;
            using (Stream receiveStream = Request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents =await readStream.ReadToEndAsync();
                }
            }
            objreq = JsonConvert.DeserializeObject<Object>(documentContents);
            webhook.OpCode = 1;
            webhook.DocumentContent = documentContents;
            webhook.Fk_MemId = 0;
            webhook.PaymentId = objreq.payload.payment.entity.id.ToString();
            DataSet dataSet = webhook.SaveWebhookData();
            webhook.OrderId = objreq.payload.payment.entity.order_id.ToString();
            DataSet dataSet1 = webhook.GetPaymentDetails();
            if(dataSet1 != null)
            {
                if (dataSet1.Tables[0].Rows.Count>0)
                {
                    if (dataSet1.Tables[0].Rows[0]["Msg"].ToString()=="1")
                    {
                        type = dataSet1.Tables[0].Rows[0]["Type"].ToString();
                        Fk_MemId = dataSet1.Tables[0].Rows[0]["Fk_MemId"].ToString();

                        if (objreq != null && objreq.payload.payment.entity.status.ToString() == "captured")
                        {
                            Status = "TXN_SUCCESS";
                            ResponseCode = 1;
                        }
                        else
                        {
                            Status = objreq.payload.payment.entity.status.ToString();
                            ResponseCode = 0;
                        }
                        if (type == "PlaceOrder")
                        {
                            string Body = "";
                            string obj1 = "{\"Status\":\"" + Status + "\",\"OrderNo\":\"" + webhook.OrderId + "\",\"TxnId\":\"" + webhook.PaymentId + "\",\"BankTxnId\":\"" + webhook.PaymentId + "\",\"CheckSumHash\":\"" + webhook.PaymentId + "\",\"RespMsg\":\"" + Status + "\",\"OpCode\":\"1\"}";
                            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                            string result = Common.HITAPI(APIURL.PlaceOrder, Body);
                            PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();
                            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                            commonResponse.Message = dcdata;
                            commonResponse.ResponseCode = ResponseCode;
                        }
                        else if(type== "KaryonPointsAssociate")
                        {
                            KaryonPointsRequest karyonPointsRequest = new KaryonPointsRequest();
                            karyonPointsRequest.Status = Status;
                            karyonPointsRequest.TransactionNo = webhook.OrderId;
                            karyonPointsRequest.TXNID = webhook.PaymentId;
                            karyonPointsRequest.BANKTXNID = webhook.PaymentId;
                            karyonPointsRequest.CHECKSUMHASH = webhook.PaymentId;
                            karyonPointsRequest.RespMsg = Status;
                            karyonPointsRequest.OpCode = 2;
                            DataSet dataSet2 = karyonPointsRequest.RequestKaryonPoints();
                        }
                    }
                }
            }
            
            return commonResponse;
        }
    }
}
