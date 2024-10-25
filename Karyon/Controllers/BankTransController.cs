using Karyon.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace Karyon.Controllers
{
    [ApiController]
    public class BankTransController : ControllerBase
    {
        [Route("api/BankTrans/UPICollection")]
        [HttpPost]
        public string addTransactionsInfo(BankTransaction context)
        {

            Common common = new Common();
            //common.EncryptData("{'PayerAccNumber':'520101036287357','UTR':'KARLLP2336368890','PayerPaymentDate':'09/21/2021','BankInternalTransactionNumber':'N206H80208259','Mode':'NEFT','Amount':'10','ClientCode':'KARLLP','ClientAccountNo':'180205001150','PayerName':'GANESH GODSE','VirtualAccountNumber':'KARLLP233758821503','SenderRemark':'Test Transaction','PayerBankIFSC':'ICIC0000106'}");

            string requestResult = string.Empty;

            try
            {

               
                string encryptedKey = context.encryptedKey;
                string encryptedData = context.encryptedData;
                
                byte[] _keyDecrypt = Convert.FromBase64String(encryptedKey);
                string _decryptKey = common.DecryptKey(_keyDecrypt);

                string _decryptData = common.DecryptData(encryptedData, _decryptKey);
                _decryptData = _decryptData.Replace("\\x{10}", string.Empty).Replace("\u000e", string.Empty).Replace("\u000f", string.Empty).Replace("\u0002", string.Empty).Replace("\u0006", string.Empty).Replace("\u0005", string.Empty).Replace("\u0003", string.Empty);

                char[] spearator = { '}' };
                string[] strlist = _decryptData.Split('}');
                _decryptData = strlist[0] + " }";

                var jsonResponce = JsonConvert.DeserializeObject<ECollResponse>(_decryptData);

                string _CustomerCode = string.Empty, _VirtualACCode = string.Empty, _MODE = string.Empty,
                    _UTR = string.Empty, _CustomerAccountNo = string.Empty, _AMT = string.Empty, _PayeeName = string.Empty,
                    _PayeeAccountNumber = string.Empty, _PayeeBankIFSC = string.Empty, _PayeePaymentDate = string.Empty, _BankInternalTransactionNumber = string.Empty;

                if (jsonResponce.UTR != null)
                    _UTR = jsonResponce.UTR;

                _CustomerCode = jsonResponce.ClientCode;
                _VirtualACCode = jsonResponce.VirtualAccountNumber;
                _MODE = jsonResponce.Mode;
                _CustomerAccountNo = jsonResponce.ClientAccountNo;
                _AMT = jsonResponce.Amount;
                _PayeeName = jsonResponce.PayerName;
                _PayeeAccountNumber = jsonResponce.PayerAccNumber;
                _PayeeBankIFSC = jsonResponce.PayerBankIFSC;
                _PayeePaymentDate = jsonResponce.PayerPaymentDate;
                _BankInternalTransactionNumber = jsonResponce.BankInternalTransactionNumber;

                //inserting data into our database
                MTransactionHistory _model = new MTransactionHistory();
                _model.ClientCode = jsonResponce.ClientCode; // whether it can be FMILYN or karyon both
                _model.VirtualAccountNumber = jsonResponce.VirtualAccountNumber;
                _model.MODE = jsonResponce.Mode;
                _model.ClientAccountNo = jsonResponce.ClientAccountNo;
                _model.Amount = jsonResponce.Amount;
                _model.PayerName = jsonResponce.PayerName;
                _model.PayerAccNumber = jsonResponce.PayerAccNumber;
                _model.PayerBankIFSC = jsonResponce.PayerBankIFSC;
                _model.PayerPaymentDate = jsonResponce.PayerPaymentDate;
                _model.BankInternalTransactionNumber = jsonResponce.BankInternalTransactionNumber;
                _model.SenderRemark = jsonResponce.SenderRemark;
                _model.UTR = jsonResponce.UTR;
                //added in our database
                BankTransaction bankTransaction = new BankTransaction();
                MTransactionHistoryResposne mTransactionHistoryResposne = new MTransactionHistoryResposne();
                DataSet dataSet = bankTransaction.DALICICITrasactionForAPI(_model);

                //MTransactionHistoryResposne _objResponse = CustomerDb.DALICICITrasactionForAPI(_model);
                if (dataSet.Tables[0].Rows[0]["CODE"].ToString() == "11") //successfull response
                {
                    var _parameterList2 = new Dictionary<string, object>();
                    _parameterList2.Add("Response", "Success");
                    _parameterList2.Add("Code", "11");
                    var encodejsonSuccess = JsonConvert.SerializeObject(_parameterList2);
                    string _responseDataSuccess = common.EncryptData(encodejsonSuccess.ToString());
                    return _responseDataSuccess;
                   
                }
                else  //In case of the failure response
                {
                    var _parameterList = new Dictionary<string, object>();
                    _parameterList.Add("Response", "Duplicate UTR");
                    _parameterList.Add("Code", "06");
                    var encodejson = JsonConvert.SerializeObject(_parameterList);
                    string _responseData = common.EncryptData(encodejson);
                    return _responseData;
                }


                // string _responseData = EncryptData(encodejsonSuccess);
                //var clean_response = JObject.Parse(_responseData);
                //var response = Request.CreateResponse(clean_response);
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //Response.AppendHeader("Content-type", "application/json");
                //response.StatusCode = HttpStatusCode.OK;
                //return response;
            }
            catch (Exception ex) // in case of exception 
            {
                
                var _parameterList = new Dictionary<string, object>();
                _parameterList.Add("Response", "Exception Occured");
                _parameterList.Add("Code", "12");
                var encodejson = JsonConvert.SerializeObject(_parameterList);
                string _responseData = common.EncryptData(encodejson.ToString());
                return _responseData;
            }
        }

        [Route("api/BankTrans/ECollectionAPI")]
        [HttpPost]
        public string ECollectionAPI(BankTransaction context)
        {


            //EncryptData("{'PayerAccNumber':'520101036287357','UTR':'KARLLP2336368890','PayerPaymentDate':'09/21/2021','BankInternalTransactionNumber':'N206H80208259','Mode':'NEFT','Amount':'10','ClientCode':'KARLLP','ClientAccountNo':'180205001150','PayerName':'GANESH GODSE','VirtualAccountNumber':'KARLLP233758821503','SenderRemark':'Test Transaction','PayerBankIFSC':'ICIC0000106'}");

            string requestResult = string.Empty;

            try
            {


                string encryptedKey = context.encryptedKey;
                string encryptedData = context.encryptedData;
                Common common = new Common();
                byte[] _keyDecrypt = Convert.FromBase64String(encryptedKey);
                string _decryptKey = common.DecryptKey(_keyDecrypt);

                string _decryptData = common.DecryptData(encryptedData, _decryptKey);
                _decryptData = _decryptData.Replace("\\x{10}", string.Empty).Replace("\u000e", string.Empty).Replace("\u000f", string.Empty).Replace("\u0002", string.Empty).Replace("\u0006", string.Empty).Replace("\u0005", string.Empty).Replace("\u0003", string.Empty);

                char[] spearator = { '}' };
                string[] strlist = _decryptData.Split('}');
                _decryptData = strlist[0] + " }";

                var jsonResponce = JsonConvert.DeserializeObject<ECollResponse>(_decryptData);

                string _CustomerCode = string.Empty, _VirtualACCode = string.Empty, _MODE = string.Empty,
                    _UTR = string.Empty, _CustomerAccountNo = string.Empty, _AMT = string.Empty, _PayeeName = string.Empty,
                    _PayeeAccountNumber = string.Empty, _PayeeBankIFSC = string.Empty, _PayeePaymentDate = string.Empty, _BankInternalTransactionNumber = string.Empty;

                if (jsonResponce.UTR != null)
                    _UTR = jsonResponce.UTR;

                _CustomerCode = jsonResponce.ClientCode;
                _VirtualACCode = jsonResponce.VirtualAccountNumber;
                _MODE = jsonResponce.Mode;
                _CustomerAccountNo = jsonResponce.ClientAccountNo;
                _AMT = jsonResponce.Amount;
                _PayeeName = jsonResponce.PayerName;
                _PayeeAccountNumber = jsonResponce.PayerAccNumber;
                _PayeeBankIFSC = jsonResponce.PayerBankIFSC;
                _PayeePaymentDate = jsonResponce.PayerPaymentDate;
                _BankInternalTransactionNumber = jsonResponce.BankInternalTransactionNumber;

                //inserting data into our database
                MTransactionHistory _model = new MTransactionHistory();
                _model.ClientCode = jsonResponce.ClientCode; // whether it can be FMILYN or karyon both
                _model.VirtualAccountNumber = jsonResponce.VirtualAccountNumber;
                _model.MODE = jsonResponce.Mode;
                _model.ClientAccountNo = jsonResponce.ClientAccountNo;
                _model.Amount = jsonResponce.Amount;
                _model.PayerName = jsonResponce.PayerName;
                _model.PayerAccNumber = jsonResponce.PayerAccNumber;
                _model.PayerBankIFSC = jsonResponce.PayerBankIFSC;
                _model.PayerPaymentDate = jsonResponce.PayerPaymentDate;
                _model.BankInternalTransactionNumber = jsonResponce.BankInternalTransactionNumber;
                _model.SenderRemark = jsonResponce.SenderRemark;
                _model.UTR = jsonResponce.UTR;
                //added in our database
                BankTransaction bankTransaction = new BankTransaction();
                MTransactionHistoryResposne mTransactionHistoryResposne = new MTransactionHistoryResposne();
                DataSet dataSet = bankTransaction.DALICICITrasactionForAPI(_model);

                //MTransactionHistoryResposne _objResponse = CustomerDb.DALICICITrasactionForAPI(_model);
                if (dataSet.Tables[0].Rows[0]["CODE"].ToString() == "11") //successfull response
                {
                    var _parameterList2 = new Dictionary<string, object>();
                    _parameterList2.Add("Response", "Success");
                    _parameterList2.Add("Code", "11");
                    var encodejsonSuccess = JsonConvert.SerializeObject(_parameterList2);
                    string _responseDataSuccess = common.EncryptData(encodejsonSuccess.ToString());
                    return _responseDataSuccess;
                }
                else  //In case of the failure response
                {
                    var _parameterList = new Dictionary<string, object>();
                    _parameterList.Add("Response", "Duplicate UTR");
                    _parameterList.Add("Code", "06");
                    var encodejson = JsonConvert.SerializeObject(_parameterList);
                    string _responseData = common.EncryptData(encodejson);
                    return _responseData;
                }


                // string _responseData = EncryptData(encodejsonSuccess);
                //var clean_response = JObject.Parse(_responseData);
                //var response = Request.CreateResponse(clean_response);
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //Response.AppendHeader("Content-type", "application/json");
                //response.StatusCode = HttpStatusCode.OK;
                //return response;
            }
            catch (Exception ex) // in case of exception 
            {
                Common common = new Common();
                var _parameterList = new Dictionary<string, object>();
                _parameterList.Add("Response", "Exception Occured");
                _parameterList.Add("Code", "12");
                var encodejson = JsonConvert.SerializeObject(_parameterList);
                string _responseData = common.EncryptData(encodejson.ToString());
                return _responseData;
            }
        }

        [Route("api/BankTrans/CIBPayment")]
        [HttpPost]
        public string CIBPayment(BankTransaction context)
        {


            //EncryptData("{'PayerAccNumber':'520101036287357','UTR':'KARLLP2336368890','PayerPaymentDate':'09/21/2021','BankInternalTransactionNumber':'N206H80208259','Mode':'NEFT','Amount':'10','ClientCode':'KARLLP','ClientAccountNo':'180205001150','PayerName':'GANESH GODSE','VirtualAccountNumber':'KARLLP233758821503','SenderRemark':'Test Transaction','PayerBankIFSC':'ICIC0000106'}");

            string requestResult = string.Empty;

            try
            {


                string encryptedKey = context.encryptedKey;
                string encryptedData = context.encryptedData;
                Common common = new Common();
                byte[] _keyDecrypt = Convert.FromBase64String(encryptedKey);
                string _decryptKey = common.DecryptKey(_keyDecrypt);

                string _decryptData = common.DecryptData(encryptedData, _decryptKey);
                _decryptData = _decryptData.Replace("\\x{10}", string.Empty).Replace("\u000e", string.Empty).Replace("\u000f", string.Empty).Replace("\u0002", string.Empty).Replace("\u0006", string.Empty).Replace("\u0005", string.Empty).Replace("\u0003", string.Empty);

                char[] spearator = { '}' };
                string[] strlist = _decryptData.Split('}');
                _decryptData = strlist[0] + " }";

                var jsonResponce = JsonConvert.DeserializeObject<ECollResponse>(_decryptData);

                string _CustomerCode = string.Empty, _VirtualACCode = string.Empty, _MODE = string.Empty,
                    _UTR = string.Empty, _CustomerAccountNo = string.Empty, _AMT = string.Empty, _PayeeName = string.Empty,
                    _PayeeAccountNumber = string.Empty, _PayeeBankIFSC = string.Empty, _PayeePaymentDate = string.Empty, _BankInternalTransactionNumber = string.Empty;

                if (jsonResponce.UTR != null)
                    _UTR = jsonResponce.UTR;

                _CustomerCode = jsonResponce.ClientCode;
                _VirtualACCode = jsonResponce.VirtualAccountNumber;
                _MODE = jsonResponce.Mode;
                _CustomerAccountNo = jsonResponce.ClientAccountNo;
                _AMT = jsonResponce.Amount;
                _PayeeName = jsonResponce.PayerName;
                _PayeeAccountNumber = jsonResponce.PayerAccNumber;
                _PayeeBankIFSC = jsonResponce.PayerBankIFSC;
                _PayeePaymentDate = jsonResponce.PayerPaymentDate;
                _BankInternalTransactionNumber = jsonResponce.BankInternalTransactionNumber;

                //inserting data into our database
                MTransactionHistory _model = new MTransactionHistory();
                _model.ClientCode = jsonResponce.ClientCode; // whether it can be FMILYN or karyon both
                _model.VirtualAccountNumber = jsonResponce.VirtualAccountNumber;
                _model.MODE = jsonResponce.Mode;
                _model.ClientAccountNo = jsonResponce.ClientAccountNo;
                _model.Amount = jsonResponce.Amount;
                _model.PayerName = jsonResponce.PayerName;
                _model.PayerAccNumber = jsonResponce.PayerAccNumber;
                _model.PayerBankIFSC = jsonResponce.PayerBankIFSC;
                _model.PayerPaymentDate = jsonResponce.PayerPaymentDate;
                _model.BankInternalTransactionNumber = jsonResponce.BankInternalTransactionNumber;
                _model.SenderRemark = jsonResponce.SenderRemark;
                _model.UTR = jsonResponce.UTR;
                //added in our database
                BankTransaction bankTransaction = new BankTransaction();
                MTransactionHistoryResposne mTransactionHistoryResposne = new MTransactionHistoryResposne();
                DataSet dataSet = bankTransaction.DALICICITrasactionForAPI(_model);

                //MTransactionHistoryResposne _objResponse = CustomerDb.DALICICITrasactionForAPI(_model);
                if (dataSet.Tables[0].Rows[0]["CODE"].ToString() == "11") //successfull response
                {
                    var _parameterList2 = new Dictionary<string, object>();
                    _parameterList2.Add("Response", "Success");
                    _parameterList2.Add("Code", "11");
                    var encodejsonSuccess = JsonConvert.SerializeObject(_parameterList2);
                    string _responseDataSuccess = common.EncryptData(encodejsonSuccess.ToString());
                    return _responseDataSuccess;
                }
                else  //In case of the failure response
                {
                    var _parameterList = new Dictionary<string, object>();
                    _parameterList.Add("Response", "Duplicate UTR");
                    _parameterList.Add("Code", "06");
                    var encodejson = JsonConvert.SerializeObject(_parameterList);
                    string _responseData = common.EncryptData(encodejson);
                    return _responseData;
                }


                // string _responseData = EncryptData(encodejsonSuccess);
                //var clean_response = JObject.Parse(_responseData);
                //var response = Request.CreateResponse(clean_response);
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //Response.AppendHeader("Content-type", "application/json");
                //response.StatusCode = HttpStatusCode.OK;
                //return response;
            }
            catch (Exception ex) // in case of exception 
            {
                Common common = new Common();
                var _parameterList = new Dictionary<string, object>();
                _parameterList.Add("Response", "Exception Occured");
                _parameterList.Add("Code", "12");
                var encodejson = JsonConvert.SerializeObject(_parameterList);
                string _responseData = common.EncryptData(encodejson.ToString());
                return _responseData;
            }
        }
    }
}
