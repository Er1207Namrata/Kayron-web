using iTextSharp.text.pdf;
using Karyon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Karyon.Models.Common;
using System.Data;
using System.Runtime.Serialization.Json;
using Karyon.Models.EwayBilling;
using System.Web.Http.Results;

namespace Karyon.Controllers
{
    [Route("api/Karyon/")]
    [ApiController]
    public class EwayBillingController : ControllerBase
    {
        [HttpPost("GenerateAccessToken")]
        [Produces("application/json")]
        public AccessTokenResponse GenerateAccessToken(AccessTokenRequest accessTokenRequest)
        {
            //string url = "https://clientbasic.mastersindia.co/oauth/access_token";
            string url = "https://pro.mastersindia.co/oauth/access_token";
            string result = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(accessTokenRequest));
            AccessTokenResponse objres = JsonConvert.DeserializeObject<AccessTokenResponse>(result);
            return objres;
            //HITEWAYAPI
        }
        [HttpPost("GenerateEwayBills")]
        [Produces("application/json")]
        public EwaybillingCommonResponse GenerateEwayBills(EwayBillingRequest ewayBillingRequest)
        {
            //string url = "https://clientbasic.mastersindia.co/ewayBillsGenerate";
            string url = "https://pro.mastersindia.co/ewayBillsGenerate";
            string result = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(ewayBillingRequest));
            EwaybillingCommonResponse objres = JsonConvert.DeserializeObject<EwaybillingCommonResponse>(result);
           
            if(objres.results.code==200)
            {
                EwayBillingResponse objres1 = JsonConvert.DeserializeObject<EwayBillingResponse>(result);
                objres.resultsuccess = new EwayBillingResults();
                objres.resultsuccess.code = objres1.results.code;
                objres.resultsuccess.status= objres1.results.status;
                objres.resultsuccess.requestId = objres1.results.requestId;
                objres.resultsuccess.message=objres1.results.message;
               
            }
            else
            {

                EwayBillingResponseError objres1 = JsonConvert.DeserializeObject<EwayBillingResponseError>(result);
                objres.resultError = new EwayBillingResultsError();
                objres.resultError.code = objres1.results.code;
                objres.resultError.status = objres1.results.status;
                objres.resultError.requestId = objres1.results.requestId;
                objres.resultError.message = objres1.results.message;
               

            }
            Common common = new Common();
            DataSet dataSet = common.SaveAPILog(ewayBillingRequest.Fk_MemId, JsonConvert.SerializeObject(ewayBillingRequest), result, "GenerateEwayBills", ewayBillingRequest.document_number, objres.results.status);
            return objres;
            //HITEWAYAPI
        }

        [HttpPost("ConsolidatedEwayBills")]
        [Produces("application/json")]
        public ConsolidatedEwaybillingCommonResponse ConsolidatedEwayBills(ConsolidatedEwayBills consolidatedEwayBills)
        {
            string InvoiceNo = "";
            for(int i = 0;i<= consolidatedEwayBills.list_of_eway_bills.Count-1;i++)
            {
                if(i==0)
                {
                    InvoiceNo = consolidatedEwayBills.list_of_eway_bills[i].eway_bill_number.ToString();
                }
                else
                {
                    InvoiceNo = consolidatedEwayBills.list_of_eway_bills[i].eway_bill_number.ToString() +","+ InvoiceNo;
                }
            }
            //string url = "https://client.mastersindia.co/consolidatedEwayBillsGenerate";
            string url = "https://pro.mastersindia.co/consolidatedEwayBillsGenerate";
            string result = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(consolidatedEwayBills));
            ConsolidatedEwaybillingCommonResponse objres = JsonConvert.DeserializeObject<ConsolidatedEwaybillingCommonResponse>(result);

            if (objres.results.code == 200)
            {
                ConsolidatedEwayBilsReponse objres1 = JsonConvert.DeserializeObject<ConsolidatedEwayBilsReponse>(result);
                objres.resultsuccess = new ConsolidatedResults();
                objres.resultsuccess.code = objres1.results.code;
                objres.resultsuccess.status = objres1.results.status;
                objres.resultsuccess.message = objres1.results.message;

            }
            else
            {

                ConsolidatedEwayBilsReponseError objres1 = JsonConvert.DeserializeObject<ConsolidatedEwayBilsReponseError>(result);
                objres.resultError = new ConsolidatedResultsError();
                objres.resultError.code = objres1.results.code;
                objres.resultError.status = objres1.results.status;
                objres.resultError.message = objres1.results.message;


            }
            Common common = new Common();
            DataSet dataSet = common.SaveAPILog(consolidatedEwayBills.Fk_MemId, JsonConvert.SerializeObject(consolidatedEwayBills), result, "ConsolidatedEwayBills", InvoiceNo, objres.results.status);
            return objres;
            //HITEWAYAPI
        }


        [HttpPost("UpdateVechileNo")]
        [Produces("application/json")]
        public UpdateVechileCommonResponse UpdateVechileNo(UpdateVechileNo updateVechileNo)
        {
            //string url = "https://clientbasic.mastersindia.co/updateVehicleNumber";
            string url = "https://pro.mastersindia.co/updateVehicleNumber";
            string result = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(updateVechileNo));
            UpdateVechileCommonResponse objres = JsonConvert.DeserializeObject<UpdateVechileCommonResponse>(result);
            if (objres.results.code == 200)
            {
                UpdateVechileResultsReponse objres1 = JsonConvert.DeserializeObject<UpdateVechileResultsReponse>(result);
                objres.resultsuccess = new UpdateVechileResults();
                objres.resultsuccess.code = objres1.results.code;
                objres.resultsuccess.status = objres1.results.status;
                objres.resultsuccess.message = objres1.results.message;

            }
            else
            {

                UpdateVechileResultsReponseError objres1 = JsonConvert.DeserializeObject<UpdateVechileResultsReponseError>(result);
                objres.resultError = new UpdateVechileResultsError();
                objres.resultError.code = objres1.results.code;
                objres.resultError.status = objres1.results.status;
                objres.resultError.message = objres1.results.message;


            }
            Common common = new Common();
            DataSet dataSet = common.SaveAPILog(updateVechileNo.Fk_MemId, JsonConvert.SerializeObject(updateVechileNo), result, "UpdateVechileNo", updateVechileNo.eway_bill_number.ToString(), objres.results.status);
            return objres;
            //HITEWAYAPI
        }

        [HttpPost("EwayBillCancel")]
        [Produces("application/json")]
        public UpdateVechileCommonResponse EwayBillCancel(CancelEway cancelEway)
        {
            UpdateVechileCommonResponse objres = new UpdateVechileCommonResponse();
            string urlauth = "https://pro.mastersindia.co/oauth/access_token";
            AccessTokenRequest accessTokenRequest = new AccessTokenRequest();
            accessTokenRequest.username = CommonEwayBIll.username;
            accessTokenRequest.password = CommonEwayBIll.password;
            accessTokenRequest.client_id = CommonEwayBIll.client_id;
            accessTokenRequest.client_secret = CommonEwayBIll.client_secret;
            accessTokenRequest.grant_type = CommonEwayBIll.grant_type;
            string resultauth = Common.HITEWAYAPI(urlauth, JsonConvert.SerializeObject(accessTokenRequest));
            AccessTokenResponse objresauth = JsonConvert.DeserializeObject<AccessTokenResponse>(resultauth);
            if (string.IsNullOrEmpty(objresauth.error))
            {
                //string url = "https://clientbasic.mastersindia.co/updateVehicleNumber";
                string url = "https://pro.mastersindia.co/updateVehicleNumber";
                string result = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(cancelEway));
                objres = JsonConvert.DeserializeObject<UpdateVechileCommonResponse>(result);

                Common common = new Common();
                DataSet dataSet = common.SaveAPILog(cancelEway.Fk_MemId, JsonConvert.SerializeObject(cancelEway), result, "CancelEwayBill", cancelEway.eway_bill_number.ToString(), objres.results.status);
            }
            else
            {
                objres.results.code = 0;
            }
            return objres;
            //HITEWAYAPI
        }

        [HttpPost("ValidateGST")]
        [Produces("application/json")]
        public ValidateGSTCommonResponse ValidateGST(ValidateGST validateGST)
        {
            ValidateGSTCommonResponse objres = new ValidateGSTCommonResponse();
            //string urlauth = "https://clientbasic.mastersindia.co/oauth/access_token";
            string urlauth = "https://pro.mastersindia.co/oauth/access_token";
            AccessTokenRequest accessTokenRequest = new AccessTokenRequest();
            accessTokenRequest.username = CommonEwayBIll.username;
            accessTokenRequest.password = CommonEwayBIll.password;
            accessTokenRequest.client_id = CommonEwayBIll.client_id;
            accessTokenRequest.client_secret = CommonEwayBIll.client_secret;
            accessTokenRequest.grant_type = CommonEwayBIll.grant_type;
            string resultauth = Common.HITEWAYAPI(urlauth, JsonConvert.SerializeObject(accessTokenRequest));
            AccessTokenResponse objresauth = JsonConvert.DeserializeObject<AccessTokenResponse>(resultauth);
            if (string.IsNullOrEmpty(objresauth.error))
            {
               // string url = "https://clientbasic.mastersindia.co/gstinDetails?access_token="+ objresauth.access_token+"&user_gstin="+validateGST.user_gstin+"&gstin="+validateGST.user_gstin;
                string url = "https://pro.mastersindia.co/gstinDetails?access_token=" + objresauth.access_token+"&user_gstin="+validateGST.user_gstin+"&gstin="+validateGST.gstin;
                string result = Common.HITAPI(url);
                objres = JsonConvert.DeserializeObject<ValidateGSTCommonResponse>(result);
                if (objres.results.code == 200)
                {
                    ValidateGSTResponse objres1 = JsonConvert.DeserializeObject<ValidateGSTResponse>(result);
                    objres.resultsuccess = new ValidateGSTResults();
                    objres.resultsuccess.message = new ValidateGStMessage();
                    objres.resultsuccess.message.Gstin = objres1.results.message.Gstin;
                    objres.resultsuccess.message.TradeName = objres1.results.message.TradeName;
                    objres.resultsuccess.message.LegalName = objres1.results.message.LegalName;
                    objres.resultsuccess.message.AddrBnm = objres1.results.message.AddrBnm;
                    objres.resultsuccess.message.AddrBno = objres1.results.message.AddrBno;
                    objres.resultsuccess.message.AddrFlno = objres1.results.message.AddrFlno;
                    objres.resultsuccess.message.AddrSt = objres1.results.message.AddrSt;
                    objres.resultsuccess.message.AddrLoc = objres1.results.message.AddrLoc;
                    objres.resultsuccess.message.StateCode = objres1.results.message.StateCode;
                    objres.resultsuccess.message.AddrPncd = objres1.results.message.AddrPncd;
                    objres.resultsuccess.message.TxpType = objres1.results.message.TxpType;
                    objres.resultsuccess.message.Status = objres1.results.message.Status;
                    objres.resultsuccess.message.BlkStatus = objres1.results.message.BlkStatus;
                    objres.resultsuccess.message.DtReg = objres1.results.message.DtReg;
                    objres.resultsuccess.message.DtDReg = objres1.results.message.DtDReg;


                }
                else
                {
               

                    ValidateGSTResponseError objres1 = JsonConvert.DeserializeObject<ValidateGSTResponseError>(result);
                    objres.resultsError = new ValidateGSTResultsError();
                    objres.resultsError.code = objres1.results.code;
                    objres.resultsError.status = objres1.results.status;
                    objres.resultsError.requestId = objres1.results.requestId;
                    objres.resultsError.message = objres1.results.message;


                }
                Common common = new Common();
                DataSet dataSet = common.SaveAPILog(validateGST.FK_MemId, url, result, "ValidateGST", "", objres.results.status);
            }
            else
            {
                objres.results.code = 0;
            }
            return objres;
            //HITEWAYAPI
        }

        [HttpPost("GenerateEinvoice")]
        [Produces("application/json")]
        public GenerateEInvoiceCommonResponse GenerateEinvoice(GenerateEinvoice generateEinvoice)
        {
            int ISValidGST = 1;
            
            GenerateEInvoiceCommonResponse objres = new GenerateEInvoiceCommonResponse();
            if (ISValidGST == 1)
            {
                
                string urlauth = "https://pro.mastersindia.co/oauth/access_token";
                //string urlauth = "https://pro.mastersindia.co/oauth/access_token";
                AccessTokenRequest accessTokenRequest = new AccessTokenRequest();
                accessTokenRequest.username = CommonEwayBIll.username;
                accessTokenRequest.password = CommonEwayBIll.password;
                accessTokenRequest.client_id = CommonEwayBIll.client_id;
                accessTokenRequest.client_secret = CommonEwayBIll.client_secret;
                accessTokenRequest.grant_type = CommonEwayBIll.grant_type;
                string resultauth = Common.HITEWAYAPI(urlauth, JsonConvert.SerializeObject(accessTokenRequest));
                AccessTokenResponse objresauth = JsonConvert.DeserializeObject<AccessTokenResponse>(resultauth);
                if (string.IsNullOrEmpty(objresauth.error))
                {
                    generateEinvoice.access_token = objresauth.access_token;
                    string url = "https://pro.mastersindia.co/generateEinvoice";
                    generateEinvoice.ship_details.legal_name = generateEinvoice.buyer_details.legal_name;
                    generateEinvoice.ship_details.trade_name = generateEinvoice.buyer_details.trade_name;


                    string result = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(generateEinvoice));
                    objres = JsonConvert.DeserializeObject<GenerateEInvoiceCommonResponse>(result);
                    if (objres.results.code == 200)
                    {
                        GenerateEInvoiceResponse objres1 = JsonConvert.DeserializeObject<GenerateEInvoiceResponse>(result);
                        objres.resultsuccess = new GenerateEInvoiceResults();
                        objres.resultsuccess.message = new GenerateEInvoiceMessage();
                        objres.resultsuccess.message.AckNo = objres1.results.message.AckNo;
                        objres.resultsuccess.message.AckDt = objres1.results.message.AckDt;
                        objres.resultsuccess.message.Irn = objres1.results.message.Irn;
                        objres.resultsuccess.message.SignedInvoice = objres1.results.message.SignedInvoice;
                        objres.resultsuccess.message.SignedQRCode = objres1.results.message.SignedQRCode;
                        objres.resultsuccess.message.EwbNo = objres1.results.message.EwbNo;
                        objres.resultsuccess.message.EwbDt = objres1.results.message.EwbDt;
                        objres.resultsuccess.message.EwbValidTill = objres1.results.message.EwbValidTill;
                        objres.resultsuccess.message.QRCodeUrl = objres1.results.message.QRCodeUrl;
                        objres.resultsuccess.message.EinvoicePdf = objres1.results.message.EinvoicePdf;
                        objres.resultsuccess.message.Status = objres1.results.message.Status;
                        objres.resultsuccess.message.Remarks = objres1.results.message.Remarks;
                        objres.resultsuccess.message.alert = objres1.results.message.alert;
                        objres.resultsuccess.message.error = objres1.results.message.error;

                    }
                    else
                    {


                        GenerateEInvoiceResponseError objres1 = JsonConvert.DeserializeObject<GenerateEInvoiceResponseError>(result);
                        objres.resultsError = new GenerateEInvoiceResultsError();
                        objres.resultsError.code = objres1.results.code;
                        objres.resultsError.status = objres1.results.status;
                        objres.resultsError.requestId = objres1.results.requestId;
                        objres.resultsError.message = objres1.results.message;
                        objres.resultsError.InfoDtls = objres1.results.InfoDtls;
                        objres.resultsError.errorMessage = objres1.results.errorMessage;


                    }
                    Common common = new Common();
                    DataSet dataSet = common.SaveAPILog(generateEinvoice.FK_MemId, JsonConvert.SerializeObject(generateEinvoice), result, "GenerateEInvoice", generateEinvoice.document_details.document_number, objres.results.status);
                }
                else
                {
                    objres.results.code = 0;
                }
            }
            
            return objres;
            //HITEWAYAPI
        }

        [HttpPost("CancelEinvoice")]
        [Produces("application/json")]
        public CancelEinvoiceCommonResponse CancelEinvoice(CancelEinvoice cancelEinvoice)
        {
            CancelEinvoiceCommonResponse objres = new CancelEinvoiceCommonResponse();
            
                string urlauth = "https://pro.mastersindia.co/oauth/access_token";
                //string urlauth = "https://pro.mastersindia.co/oauth/access_token";
                AccessTokenRequest accessTokenRequest = new AccessTokenRequest();
                accessTokenRequest.username = CommonEwayBIll.username;
                accessTokenRequest.password = CommonEwayBIll.password;
                accessTokenRequest.client_id = CommonEwayBIll.client_id;
                accessTokenRequest.client_secret = CommonEwayBIll.client_secret;
                accessTokenRequest.grant_type = CommonEwayBIll.grant_type;
                string resultauth = Common.HITEWAYAPI(urlauth, JsonConvert.SerializeObject(accessTokenRequest));
                AccessTokenResponse objresauth = JsonConvert.DeserializeObject<AccessTokenResponse>(resultauth);
            if (string.IsNullOrEmpty(objresauth.error))
            {
                cancelEinvoice.access_token = objresauth.access_token;
                string url = "https://pro.mastersindia.co/cancelEinvoice";

                string result = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(cancelEinvoice));
                objres = JsonConvert.DeserializeObject<CancelEinvoiceCommonResponse>(result);
                if (objres.results.code == 200)
                {
                    CancelEinvoiceResponse objres1 = JsonConvert.DeserializeObject<CancelEinvoiceResponse>(result);
                    objres.resultsuccess = new CancelEinvoiceResults();
                    objres.resultsuccess.message = new CancelEinvoiceMessage();
                    objres.resultsuccess.message.Irn = objres1.results.message.Irn;
                    objres.resultsuccess.message.CancelDate = objres1.results.message.CancelDate;


                }
                else
                {


                    CancelEinvoiceResponseError objres1 = JsonConvert.DeserializeObject<CancelEinvoiceResponseError>(result);
                    objres.resultsError = new CancelEinvoiceResultsError();
                    objres.resultsError.code = objres1.results.code;
                    objres.resultsError.status = objres1.results.status;
                    objres.resultsError.message = objres1.results.message;
                    objres.resultsError.InfoDtls = objres1.results.InfoDtls;
                    objres.resultsError.errorMessage = objres1.results.errorMessage;


                }
                Common common = new Common();
                DataSet dataSet = common.SaveAPILog(cancelEinvoice.FK_MemId, JsonConvert.SerializeObject(cancelEinvoice), result, "Cancel EInvoice", cancelEinvoice.OrderNo, objres.results.status);

            }
            return objres;
            //HITEWAYAPI
        }

    }
}
