using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing.Construction;
using Karyon.Models;
using Karyon.Models.EwayBilling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Nancy.Json;
using Newtonsoft.Json;
using paytm;
using System.Data;
using static Karyon.Models.Common;

namespace Karyon.Controllers
{
    public class FranchiseeController : FranchiseeBaseController
    {
        public IActionResult DashBoard(DashBoardRequest dashBoard)
        {
            try
            {
                dashBoard.CustomerId = int.Parse(HttpContext.Session.GetString("FranchiseId").ToString());
                dashBoard.FranchiseTypeId = HttpContext.Session.GetString("FK_FranchiseTypeId").ToString();
                DataSet dataSet = dashBoard.GetFranchiseeDashBoard();
                if (dataSet.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    ViewBag.WalletBalance = decimal.Parse(dataSet.Tables[0].Rows[0]["WalletBalance"].ToString());

                    HttpContext.Session.SetString("ProfilePic", "https://www.karyon.organic" + dataSet.Tables[0].Rows[0]["ProfilePic"].ToString());
                    ViewBag.CrAmount = decimal.Parse(dataSet.Tables[1].Rows[0]["BillingAmount"].ToString());
                    ViewBag.TotalDisPatchAmt = decimal.Parse(dataSet.Tables[1].Rows[0]["TotalDisPatchAmt"].ToString());
                    if (decimal.Parse(dataSet.Tables[1].Rows[0]["TotalDisPatchAmt"].ToString()) >= 50000)
                    {
                        if (dataSet.Tables[1].Rows[0]["StateName"].ToString() == "Maharashtra")
                        {
                            ViewBag.Msg = "3";
                            //return RedirectToAction("EwayGeneration");
                        }
                        else
                        {
                            if (decimal.Parse(dataSet.Tables[1].Rows[0]["TotalDisPatchAmt"].ToString()) >= 100000)
                            {
                                ViewBag.Msg = "3";
                                //return RedirectToAction("EwayGeneration");
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "1";
                    }

                    ViewBag.FK_FranchiseTypeId = HttpContext.Session.GetString("FK_FranchiseTypeId").ToString();
                    if (dataSet.Tables[2].Rows.Count > 0)
                    {
                        TempData["Message"] = dataSet.Tables[2].Rows[0]["Message"].ToString();
                    }
                    DataSet dataset = dashBoard.Eway();
                    if (dataset == null)
                    {

                        ViewBag.Amount = dataset.Tables[0].Rows[0]["Amount"].ToString();
                        ViewBag.Message = dataset.Tables[1].Rows[0]["Message"].ToString();
                    }

                }
                else
                {
                    ViewBag.Msg = dataSet.Tables[0].Rows[0]["Msg"].ToString();
                }
                return View(dashBoard);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult EwayGeneration(EwayBillingRequest ewayBillingRequest, string GenerateEway)
        {
            #region BindTransportorId
            Common common = new Common();
            List<SelectListItem> ddlTransportId = new List<SelectListItem>();
            common.OpCode = 49;
            DataSet dataSetC = common.GetAllMasters();
            ddlTransportId.Add(new SelectListItem { Text = "Select Transportor", Value = "" });
            if (dataSetC != null && dataSetC.Tables.Count > 0 && dataSetC.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dataSetC.Tables[0].Rows)
                {
                    ddlTransportId.Add(new SelectListItem { Text = r["Text"].ToString(), Value = r["Id"].ToString() });
                }
            }
            ViewBag.ddlTransportId = ddlTransportId;
            #endregion BindTransfortorId

            DataSet dataSet12 = new DataSet();
            if (!string.IsNullOrEmpty(GenerateEway))
            {
                // string urlauth = "https://clientbasic.mastersindia.co/oauth/access_token";
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

                    CreateOrder createOrder = new CreateOrder();
                    createOrder.OrderNo = ewayBillingRequest.OrderNo;
                    createOrder.DispatchCount = int.Parse(ewayBillingRequest.DispatchCount);
                    createOrder.AddedBy = int.Parse(HttpContext.Session.GetString("FranchiseId"));
                    if (ewayBillingRequest.BillType == "Customer")
                    {
                        dataSet12 = createOrder.PrintDispatchFranchiseOrder();
                        ewayBillingRequest.sub_supply_description = "";
                        ewayBillingRequest.sub_supply_type = "Supply";
                        ewayBillingRequest.document_type = "Tax Invoice";
                    }
                    else
                    {
                        dataSet12 = createOrder.PrintDispatchParentFranchiseOrder();
                        ewayBillingRequest.sub_supply_description = "Transfer invoice";
                        ewayBillingRequest.sub_supply_type = "Others";
                        ewayBillingRequest.document_type = "others";

                    }
                    //string url = "https://localhost:7095/api/Karyon/GenerateEwayBills";
                    string url = "https://karyon.organic/api/karyon/GenerateEwayBills";
                    ewayBillingRequest.access_token = objresauth.access_token;
                    ewayBillingRequest.userGstin = CommonEwayBIll.userGstin;
                    ewayBillingRequest.supply_type = "outward";
                    ewayBillingRequest.document_date = dataSet12.Tables[0].Rows[0]["InvoiceDate"].ToString();
                    ewayBillingRequest.gstin_of_consignor = CommonEwayBIll.userGstin;
                    ewayBillingRequest.legal_name_of_consignor = CommonEwayBIll.legal_name_of_consignor;

                    ewayBillingRequest.address1_of_consignor = dataSet12.Tables[0].Rows[0]["CEwayAddress"].ToString();
                    ewayBillingRequest.address2_of_consignor = "";
                    ewayBillingRequest.place_of_consignor = dataSet12.Tables[0].Rows[0]["CEwayPlace"].ToString();
                    ewayBillingRequest.pincode_of_consignor = int.Parse(dataSet12.Tables[0].Rows[0]["CWayPinCode"].ToString());
                    ewayBillingRequest.state_of_consignor = CommonEwayBIll.actual_from_state_name;
                    ewayBillingRequest.actual_from_state_name = dataSet12.Tables[0].Rows[0]["CEwayStateName"].ToString();
                    ewayBillingRequest.gstin_of_consignee = "URP";
                    //if (dataSet12.Tables[0].Rows[0]["GSTNo"].ToString() == "0" || dataSet12.Tables[0].Rows[0]["GSTNo"].ToString() == "")
                    //{
                    //    ewayBillingRequest.gstin_of_consignee = "URP";
                    //}
                    //else
                    //{
                    //    ewayBillingRequest.gstin_of_consignee = dataSet12.Tables[0].Rows[0]["GSTNo"].ToString();
                    //}
                    ewayBillingRequest.legal_name_of_consignee = dataSet12.Tables[0].Rows[0]["ContactPerson"].ToString();
                    ewayBillingRequest.address1_of_consignee = dataSet12.Tables[0].Rows[0]["ConEwayAddress"].ToString();
                    ewayBillingRequest.address2_of_consignee = "";
                    ewayBillingRequest.place_of_consignee = dataSet12.Tables[0].Rows[0]["ConsigneeEwayPlace"].ToString();
                    ewayBillingRequest.pincode_of_consignee = int.Parse(dataSet12.Tables[0].Rows[0]["ConsigneeWayPinCode"].ToString());
                    ewayBillingRequest.state_of_supply = dataSet12.Tables[0].Rows[0]["ConsigneeEwayStateName"].ToString();
                    ewayBillingRequest.actual_to_state_name = dataSet12.Tables[0].Rows[0]["ConsigneeEwayStateName"].ToString();


                    if (HttpContext.Session.GetString("FK_FranchiseTypeId").ToString() == "9")
                    {
                        ewayBillingRequest.transaction_type = 1;
                    }
                    else
                    {
                        ewayBillingRequest.transaction_type = 3;
                    }
                    ewayBillingRequest.other_value = 0;
                    createOrder.dtDetails = dataSet12.Tables[1];
                    createOrder.dtOrderDetails = dataSet12.Tables[3];
                    double TotalGross = 0;
                    double TotalCGST = 0;
                    double TotalSGST = 0;
                    double AmountAfterTax = 0;
                    double cgst_amount = 0;
                    double Gross = 0;
                    List<ItemList> lstItems = new List<ItemList>();
                    for (int j = 0; j <= createOrder.dtDetails.Rows.Count - 1; j++)
                    {
                        #region ItemDetails
                        ItemList itemList = new ItemList();
                        itemList.product_name = createOrder.dtDetails.Rows[j]["ProductName"].ToString();
                        itemList.product_description = createOrder.dtDetails.Rows[j]["ProductName"].ToString();
                        itemList.hsn_code = createOrder.dtDetails.Rows[j]["HSNCode"].ToString();
                        itemList.quantity = int.Parse(createOrder.dtDetails.Rows[j]["Box"].ToString());
                        if (ewayBillingRequest.BillType == "Customer")
                        {
                            itemList.unit_of_product = "BTL";
                        }
                        else
                        {
                            itemList.unit_of_product = "BOX";
                        }
                        if (dataSet12.Tables[0].Rows[0]["ConsigneeState"].ToString() == "MAHARASHTRA")
                        {
                            itemList.cgst_rate = double.Parse(createOrder.dtDetails.Rows[j]["CGSTPerc"].ToString());
                            itemList.sgst_rate = double.Parse(createOrder.dtDetails.Rows[j]["SGSTPerc"].ToString());
                            itemList.igst_rate = 0;
                        }
                        else
                        {
                            itemList.cgst_rate = 0;
                            itemList.sgst_rate = 0;
                            itemList.igst_rate = double.Parse(createOrder.dtDetails.Rows[j]["IGSTPerc"].ToString());
                        }

                        itemList.taxable_amount = double.Parse(createOrder.dtDetails.Rows[j]["Gross"].ToString());
                        itemList.cess_rate = 0;
                        itemList.cessNonAdvol = 0;
                        #endregion
                        lstItems.Add(itemList);

                        TotalGross = TotalGross + double.Parse(createOrder.dtDetails.Rows[j]["Gross"].ToString());
                        TotalCGST = TotalCGST + double.Parse(createOrder.dtDetails.Rows[j]["CGSTAmt"].ToString());
                        TotalSGST = TotalSGST + double.Parse(createOrder.dtDetails.Rows[j]["SGSTAmt"].ToString());
                        AmountAfterTax = TotalGross + TotalCGST + TotalSGST;
                    }
                    for (int j = 0; j <= createOrder.dtOrderDetails.Rows.Count - 1; j++)
                    {
                        Gross = Gross + double.Parse(createOrder.dtOrderDetails.Rows[j]["Gross"].ToString());
                        cgst_amount = cgst_amount + double.Parse(createOrder.dtOrderDetails.Rows[j]["CGSTAmt"].ToString());
                    }
                    if (dataSet12.Tables[0].Rows[0]["ConsigneeState"].ToString() == "MAHARASHTRA")
                    {
                        ewayBillingRequest.cgst_amount = cgst_amount;
                        ewayBillingRequest.sgst_amount = cgst_amount;
                        ewayBillingRequest.igst_amount = 0;
                    }
                    else
                    {
                        ewayBillingRequest.igst_amount = cgst_amount + cgst_amount;
                        ewayBillingRequest.cgst_amount = 0;
                        ewayBillingRequest.sgst_amount = 0;
                    }
                    ewayBillingRequest.itemList = lstItems;
                    ewayBillingRequest.cess_amount = 0;
                    ewayBillingRequest.cess_nonadvol_value = 0;
                    //ewayBillingRequest.transporter_id = "";
                    //ewayBillingRequest.transporter_name = "";
                    ewayBillingRequest.transportation_distance = "0";
                    ewayBillingRequest.total_invoice_value = AmountAfterTax;
                    ewayBillingRequest.taxable_amount = Gross;
                    ewayBillingRequest.generate_status = 1;
                    ewayBillingRequest.data_source = "erp";
                    ewayBillingRequest.user_ref = DateTime.Now.ToString("ddMMyyyyhhmmss");
                    ewayBillingRequest.eway_bill_status = "";
                    ewayBillingRequest.auto_print = "Y";
                    ewayBillingRequest.email = "info@karyon.organic";
                    if(!string.IsNullOrEmpty(ewayBillingRequest.transporter_id))
                    {
                        ewayBillingRequest.vehicle_number = "";
                        ewayBillingRequest.vehicle_type = "";
                        ewayBillingRequest.transportation_mode = "";

                    }
                    //ewayBillingRequest.vehicle_number = string.IsNullOrEmpty(ewayBillingRequest.vehicle_number) ? "" : ewayBillingRequest.vehicle_number;
                    ewayBillingRequest.Fk_MemId = HttpContext.Session.GetString("FranchiseId");
                    string result = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(ewayBillingRequest));
                    EwaybillingCommonResponse objres = JsonConvert.DeserializeObject<EwaybillingCommonResponse>(result);
                    if (objres.results.code == 200)
                    {
                        EwayBillingResponse objres1 = JsonConvert.DeserializeObject<EwayBillingResponse>(result);
                        objres.resultsuccess = new EwayBillingResults();
                        objres.resultsuccess.code = objres1.results.code;
                        objres.resultsuccess.status = objres1.results.status;
                        objres.resultsuccess.requestId = objres1.results.requestId;
                        objres.resultsuccess.message = objres1.results.message;
                        TempData["EwayMesage"] = objres.resultsuccess.message;
                    }
                    else
                    {
                        EwayBillingResponseError objres1 = JsonConvert.DeserializeObject<EwayBillingResponseError>(result);
                        objres.resultError = new EwayBillingResultsError();
                        objres.resultError.code = objres1.results.code;
                        objres.resultError.status = objres1.results.status;
                        objres.resultError.requestId = objres1.results.requestId;
                        objres.resultError.message = objres1.results.message;
                        TempData["EwayMesage"] = objres.resultError.message;



                    }

                }
                else
                {
                    TempData["EwayMesage"] = objresauth.error_description;
                }
            }
            ewayBillingRequest.FromDate = string.IsNullOrEmpty(ewayBillingRequest.FromDate) ? null : Common.ConvertToSystemDate(ewayBillingRequest.FromDate, "dd/MM/yyyy");
            ewayBillingRequest.ToDate = string.IsNullOrEmpty(ewayBillingRequest.ToDate) ? null : Common.ConvertToSystemDate(ewayBillingRequest.ToDate, "dd/MM/yyyy");
            DataSet dataSet = ewayBillingRequest.GetDispatchOrders(HttpContext.Session.GetString("FranchiseId").ToString());
            ewayBillingRequest.dtDetails = dataSet.Tables[0];
            return View(ewayBillingRequest);
        }
        public ActionResult GetProductDetails(string ProductId)
        {
            string Body = "";
            string obj1 = "{\"ProductId\":\"" + ProductId + "\",\"Fk_FranchiseId\":\"" + HttpContext.Session.GetString("FranchiseId") + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetProductDetails, Body);
            ProductDetailsResponse productDetailsResponse = new ProductDetailsResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            productDetailsResponse = JsonConvert.DeserializeObject<ProductDetailsResponse>(dcdata);

            return Json(productDetailsResponse);
        }

        public ActionResult CreateOrder(CreateOrder createOrder, string btnAdd, string btnSave)
        {
            #region WalletBalance

            string obj1 = "{\"Fk_FranchiseId\":\"" + HttpContext.Session.GetString("FranchiseId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.FranchiseeWalletBalance, Body);
            FranchiseeWalletResponse walletResponse = new FranchiseeWalletResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            walletResponse = JsonConvert.DeserializeObject<FranchiseeWalletResponse>(dcdata);
            if (walletResponse.Status == 1)
            {
                createOrder.WalletBalance = walletResponse.Response.WalletBalance.KaryonWallet.ToString();
                createOrder.FUPPoints = walletResponse.Response.WalletBalance.FUPWallet.ToString();
            }
            #endregion
            #region BindPaymentMode
            obj1 = "{\"OpCode\":\"" + 4 + "\",\"Value\":\"" + "" + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            ddlPaymentMode.Add(new SelectListItem { Text = "Payment Mode", Value = "0" });
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlPaymentMode = ddlPaymentMode;
            }
            #endregion

            #region BindProducts
            obj1 = "{\"OpCode\":\"" + 10 + "\",\"FK_FranchiseId\":\"" + HttpContext.Session.GetString("FranchiseId") + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.GetMasterData, Body);

            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlProducts = new List<SelectListItem>();
            ddlProducts.Add(new SelectListItem { Text = "Select Product", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlProducts.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }

            }
            ViewBag.ddlProducts = ddlProducts;
            #endregion BindProducts
            createOrder.AddedBy = Convert.ToInt32(HttpContext.Session.GetString("FranchiseId"));
            if (!string.IsNullOrEmpty(btnAdd))
            {

                DataSet dataSet = createOrder.CreateOrderTemp();
            }
            if (!string.IsNullOrEmpty(btnSave))
            {
                createOrder.OrderFrom = "Web";
                DataSet dataSet = createOrder.GenerateFranchiseOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            if (createOrder.WalletType == "Paytm")
                            {
                                HttpContext.Session.SetString("OrderNo", dataSet.Tables[1].Rows[0]["OrderNo"].ToString());
                                HttpContext.Session.SetString("TransactionNo", dataSet.Tables[1].Rows[0]["OrderNo"].ToString());
                                HttpContext.Session.SetString("OrderAmount", dataSet.Tables[1].Rows[0]["OrderAmount"].ToString());
                                return RedirectToAction("FranchisePaymentIntegration", new { PageName = "FranchiseOrder" });
                            }
                            TempData["CreateOrder"] = "Order Created Successfully";
                            return RedirectToAction("CreateOrder");
                        }
                        else
                        {
                            TempData["CreateOrder"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
            }


            DataSet dataSet1 = createOrder.GetTempOrder();
            createOrder.dtDetails = dataSet1.Tables[0];
            createOrder.OrderAmount = dataSet1.Tables[1].Rows[0]["OrderAmount"].ToString();
            return View(createOrder);
        }
        public ActionResult FranchisePaymentIntegration(AddressRequest addressRequest, string PageName)
        {
            PlaceOrderResponse placeOrderResponse = new PlaceOrderResponse();

            if (!string.IsNullOrEmpty(PageName))
            {
                placeOrderResponse.OrderNo = HttpContext.Session.GetString("TransactionNo");
                placeOrderResponse.OrderAmount = HttpContext.Session.GetString("OrderAmount");
                placeOrderResponse.TransactionType = PageName;
            }
            addressRequest.Type = "Franchise";
            addressRequest.CustomerId = int.Parse(HttpContext.Session.GetString("FranchiseId"));
            DataSet dataSet = addressRequest.ManageAddress();

            string emailId = dataSet.Tables[0].Rows[0]["Email"].ToString();
            string mobileNumber = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
            String merchantKey = Gateway.MerchantId;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", Gateway.MID);
            parameters.Add("CHANNEL_ID", Gateway.CHANNEL_ID);
            parameters.Add("INDUSTRY_TYPE_ID", Gateway.INDUSTRY_TYPE_ID);
            parameters.Add("WEBSITE", Gateway.WebSite);
            parameters.Add("EMAIL", emailId);
            parameters.Add("MOBILE_NO", mobileNumber);
            parameters.Add("CUST_ID", HttpContext.Session.GetString("FranchiseId"));
            parameters.Add("ORDER_ID", placeOrderResponse.OrderNo);
            parameters.Add("TXN_AMOUNT", placeOrderResponse.OrderAmount);

            parameters.Add("CALLBACK_URL", Gateway.CallBackURL + "?Id=" + placeOrderResponse.TransactionType);

            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);


            string paytmURL = Gateway.PaytmURL + "?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;
            //string paytmURL = "https://securegw.paytm.in/theia/processTransaction?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            ViewBag.htmlData = outputHTML;

            return View();
        }
        public ActionResult DeleteTempOrder(string Id, string WalletType)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.WalletType = "KaryonWallet";
            if (!string.IsNullOrEmpty(WalletType))
            {
                createOrder.WalletType = WalletType;
            }
            createOrder.Pk_Id = int.Parse(Id);
            DataSet dataSet1 = createOrder.DeleteTempOrder();
            return RedirectToAction("CreateOrder", createOrder);
        }
        public ActionResult DeleteCustomerTempOrder(string Id)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.Pk_Id = int.Parse(Id);
            DataSet dataSet1 = createOrder.DeleteCustomerTempOrder();
            return RedirectToAction("CreateCustomerOrder");
        }
        public ActionResult ViewOrders(CreateOrder createOrder)
        {
            string Body = "";
            string obj1 = "{\"OpCode\":\"" + 7 + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlStatus = new List<SelectListItem>();
            ddlStatus.Add(new SelectListItem { Text = "All Status", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlStatus.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlStatus = ddlStatus;
            }
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }

            createOrder.Size = SessionManager.Size;
            createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 1;
            createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
            createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
            createOrder.FranchiseId = HttpContext.Session.GetString("LoginId");
            DataSet dataSet = createOrder.GetFranchiseOrders();
            createOrder.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                createOrder.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(createOrder);
        }
        public ActionResult Stock(Stock stock)
        {
            #region BindProducts
            string Body = "";
            string obj1 = "{\"OpCode\":\"" + 6 + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlProducts = new List<SelectListItem>();
            ddlProducts.Add(new SelectListItem { Text = "All Products", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlProducts.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlProducts = ddlProducts;
            }
            #endregion

            if (stock.Page == null || stock.Page == 0)
            {
                stock.Page = 1;
            }

            stock.Size = SessionManager.Size;
            stock.FranchiseLoginId = HttpContext.Session.GetString("LoginId");
            DataSet dataSet = stock.GetFranchiseStock();
            stock.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, stock.Page, SessionManager.Size);
                stock.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(stock);
        }
        public async Task<ActionResult> WalletRequest(FranchiseWallet franchiseWallet, string Request)
        {
            franchiseWallet.LoginId = HttpContext.Session.GetString("LoginId").ToString();
            franchiseWallet.Name = HttpContext.Session.GetString("FirstName").ToString();

            #region BindPaymentMode
            string obj1 = "{\"OpCode\":\"" + 4 + "\",\"Value\":\"" + "" + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                ddlPaymentMode.Add(new SelectListItem { Text = "Payment Mode", Value = "0" });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        if(r["Name"].ToString()!= "UPI Id")
                        {
                            ddlPaymentMode.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                        }
                        
                    }
                }
                ViewBag.ddlPaymentMode = ddlPaymentMode;
            }
            #endregion

            #region Bank
            obj1 = "{\"OpCode\":\"" + 45 + "\",\"Value\":\"" + "" + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            result = Common.HITAPI(APIURL.GetMasterData, Body);

            deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlBankName = new List<SelectListItem>();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlBankName.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    }
                }
                ViewBag.ddlBank = ddlBankName;
            }
            #endregion
            
            if (!string.IsNullOrEmpty(Request))
            {
                if (franchiseWallet.ImageFile != null)
                {
                    string fileName = await FileManagement.WriteFiles(franchiseWallet.ImageFile, "FranchiseWalletRequest");
                    franchiseWallet.PaymentSlip = fileName;
                }
                if (franchiseWallet.PaymentMode == "UPI/NET Banking/Debit Card/Credit Card")
                {
                    franchiseWallet.TransactionNo = DateTime.Now.ToString("ddMMyyyyhhMMss");
                }
                if (franchiseWallet.TransactionDate != null)
                {
                    franchiseWallet.TransactionDate = string.IsNullOrEmpty(franchiseWallet.TransactionDate) ? null : Common.ConvertToSystemDate(franchiseWallet.TransactionDate, "dd/MM/yyyy");
                }
                franchiseWallet.LoginId = HttpContext.Session.GetString("LoginId");
                DataSet dataSet = franchiseWallet.FranchisewalletRequest();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            if (franchiseWallet.PaymentMode == "UPI/NET Banking/Debit Card/Credit Card")
                            {
                                HttpContext.Session.SetString("TransactionNo", franchiseWallet.TransactionNo);
                                HttpContext.Session.SetString("OrderAmount", franchiseWallet.Amount.ToString());
                                return RedirectToAction("FranchisePaymentIntegration", new { PageName = "FwalletRequest" });
                            }
                            TempData["WalletRequest"] = Messages.WalletSuccess;
                            return RedirectToAction("WalletRequest");
                        }
                        else
                        {
                            TempData["WalletRequest"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }

            }
            return View(franchiseWallet);
        }
        public ActionResult RequestList(FranchiseWallet franchiseWallet)
        {

            if (franchiseWallet.Page == null || franchiseWallet.Page == 0)
            {
                franchiseWallet.Page = 1;
            }

            franchiseWallet.Size = SessionManager.Size;
            franchiseWallet.LoginId = HttpContext.Session.GetString("LoginId");
            DataSet dataSet = franchiseWallet.FranchiseWalletList();
            franchiseWallet.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, franchiseWallet.Page, SessionManager.Size);
                franchiseWallet.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(franchiseWallet);
        }
        public ActionResult Ledger(FranchiseWallet franchiseWallet)
        {
            if (franchiseWallet.Page == null || franchiseWallet.Page == 0)
            {
                franchiseWallet.Page = 1;
            }

            franchiseWallet.Size = SessionManager.Size;
            franchiseWallet.LoginId = HttpContext.Session.GetString("LoginId");
            DataSet dataSet = franchiseWallet.GetFranchiseLedger();
            franchiseWallet.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, franchiseWallet.Page, SessionManager.Size);
                franchiseWallet.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(franchiseWallet);
        }
        public async Task<ActionResult> CreateCustomerOrder(CreateOrder createOrder, string btnAdd, string btnSave)
        {

            #region BindPaymentMode
            string obj1 = "{\"OpCode\":\"" + 25 + "\",\"Value\":\"" + "" + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                ddlPaymentMode.Add(new SelectListItem { Text = "Payment Mode", Value = "0" });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlPaymentMode = ddlPaymentMode;
            }
            #endregion

            #region BindProducts
            Stock stock = new Stock();
            stock.Page = 1;
            //stock.Size = SessionManager.Size;
            stock.Size = 9999999;
            stock.Fk_VarientId = 0;
            stock.FranchiseLoginId = HttpContext.Session.GetString("LoginId");
            DataSet dataSet = stock.GetFranchiseStock();
            List<SelectListItem> ddlProducts = new List<SelectListItem>();
            ddlProducts.Add(new SelectListItem { Text = "Select Product", Value = "0" });
            if (dataSet != null)
            {

                if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dataSet.Tables[0].Rows)
                    {
                        ddlProducts.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Id"].ToString() });
                    }
                }
                ViewBag.ddlProducts = ddlProducts;
            }

            #endregion BindProducts
            createOrder.AddedBy = Convert.ToInt32(HttpContext.Session.GetString("FranchiseId"));
            if (!string.IsNullOrEmpty(btnAdd))
            {

                DataSet dataSet1 = createOrder.CreateCustomerOrderTemp();
                if (dataSet1 != null)
                {
                    if (dataSet1.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet1.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {

                        }
                        else
                        {
                            TempData["CreateOrder"] = dataSet1.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(btnSave))
            {
                string FileName = "";
                if (createOrder.ImageFile != null)
                {
                    createOrder.FileName = await FileManagement.WriteFiles(createOrder.ImageFile, "CustomerOrder");

                }
                createOrder.TransactionDate = string.IsNullOrEmpty(createOrder.TransactionDate) ? null : Common.ConvertToSystemDate(createOrder.TransactionDate, "dd/MM/yyyy");
                DataSet dataSet12 = createOrder.GenerateCustomerOrder();
                if (dataSet12 != null)
                {
                    if (dataSet12.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet12.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["CreateOrder"] = "Order Created Successfully";
                            
                                //#region HITEinvoiceAPI
                                //GenerateEinvoice generateEinvoice = new GenerateEinvoice();
                                //generateEinvoice.FK_MemId = dataSet12.Tables[0].Rows[0]["Fk_MemId"].ToString();
                                //generateEinvoice.user_gstin = CommonEwayBIll.userGstin;
                                //generateEinvoice.data_source = "erp";
                                //#region TransactionDetails
                                //TransactionDetails transactionDetails = new TransactionDetails();
                                //transactionDetails.supply_type = "B2B";
                                //transactionDetails.charge_type = "N";
                                //transactionDetails.igst_on_intra = "N";
                                //transactionDetails.ecommerce_gstin = "";
                                //generateEinvoice.transaction_details = transactionDetails;
                                //#endregion TransactionDetails
                                //#region document_details
                                //DocumentDetails documentDetails = new DocumentDetails();
                                //documentDetails.document_type = "INV";
                                //documentDetails.document_number = dataSet12.Tables[0].Rows[0]["OrderNo"].ToString();
                                //documentDetails.document_date = dataSet12.Tables[0].Rows[0]["OrderDate"].ToString();
                                //generateEinvoice.document_details = documentDetails;
                                //#endregion document_details
                                //#region seller_details
                                //SellerDetails sellerDetails = new SellerDetails();
                                //sellerDetails.gstin = CommonEwayBIll.userGstin;
                                //sellerDetails.legal_name = CommonEwayBIll.legal_name_of_consignor;
                                //sellerDetails.trade_name = CommonEwayBIll.legal_name_of_consignor;
                                //sellerDetails.address1 = CommonEwayBIll.address1_of_consignor;
                                //sellerDetails.address2 = CommonEwayBIll.address2_of_consignor;
                                //sellerDetails.location = CommonEwayBIll.place_of_consignor;
                                //sellerDetails.pincode = CommonEwayBIll.pincode_of_consignor;
                                //sellerDetails.state_code = CommonEwayBIll.state_of_consignor;
                                //sellerDetails.phone_number = CommonEwayBIll.phone_number;
                                //sellerDetails.email = CommonEwayBIll.email;
                                //generateEinvoice.seller_details = sellerDetails;
                                //#endregion seller_details
                                //#region buyer_details
                                //BuyerDetails buyerDetails = new BuyerDetails();
                                //buyerDetails.gstin = dataSet12.Tables[0].Rows[0]["GstNo"].ToString();
                                //buyerDetails.phone_number = long.Parse(dataSet12.Tables[0].Rows[0]["MobileNo"].ToString());
                                //buyerDetails.email = "";
                                //buyerDetails.place_of_supply = dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
                                //buyerDetails.address1 = dataSet12.Tables[0].Rows[0]["Address"].ToString();
                                //buyerDetails.address2 = dataSet12.Tables[0].Rows[0]["Address1"].ToString();
                                //buyerDetails.location = dataSet12.Tables[0].Rows[0]["Location"].ToString();
                                //buyerDetails.pincode = int.Parse(dataSet12.Tables[0].Rows[0]["Pincode"].ToString());
                                //buyerDetails.state_code = dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
                                //generateEinvoice.buyer_details = buyerDetails;
                                //#endregion buyer_details
                                //#region dispatch_details
                                //DispatchDetails dispatchDetails = new DispatchDetails();
                                //dispatchDetails.company_name = CommonEwayBIll.legal_name_of_consignor;
                                //dispatchDetails.address1 = dataSet12.Tables[0].Rows[0]["FAddress"].ToString();
                                //dispatchDetails.address2 = "";
                                //dispatchDetails.location = dataSet12.Tables[0].Rows[0]["FLocation"].ToString();
                                //dispatchDetails.pincode = int.Parse(dataSet12.Tables[0].Rows[0]["FPincode"].ToString());
                                //dispatchDetails.state_code = dataSet12.Tables[0].Rows[0]["FState"].ToString();
                                //generateEinvoice.dispatch_details = dispatchDetails;
                                //#endregion dispatch_details
                                //#region ship_details
                                //ShipDetails shipDetails = new ShipDetails();
                                //shipDetails.gstin = dataSet12.Tables[0].Rows[0]["GstNo"].ToString();
                                //shipDetails.address1 = dataSet12.Tables[0].Rows[0]["Address"].ToString();
                                //shipDetails.address2 = dataSet12.Tables[0].Rows[0]["Address1"].ToString();
                                //shipDetails.location = dataSet12.Tables[0].Rows[0]["Location"].ToString();
                                //shipDetails.pincode = int.Parse(dataSet12.Tables[0].Rows[0]["Pincode"].ToString());
                                //shipDetails.state_code = dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
                                //generateEinvoice.ship_details = shipDetails;
                                //#endregion ship_details

                                //decimal TotalGross = 0, TotalCGST = 0, TotalSGST = 0, AmountAfterTax = 0, TotalIGST = 0;
                                //for (int i = 0; i <= dataSet12.Tables[1].Rows.Count - 1; i++)
                                //{

                                //    TotalGross = TotalGross + decimal.Parse(dataSet12.Tables[1].Rows[i]["Gross"].ToString());
                                //    if (dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString() == "MAHARASHTRA")
                                //    {
                                //        TotalCGST = TotalCGST + decimal.Parse(dataSet12.Tables[1].Rows[i]["CGSTAmt"].ToString());
                                //        TotalSGST = TotalSGST + decimal.Parse(dataSet12.Tables[1].Rows[i]["SGSTAmt"].ToString());
                                //        TotalIGST = 0;
                                //        AmountAfterTax = TotalGross + TotalCGST + TotalSGST;
                                //    }
                                //    else
                                //    {
                                //        TotalCGST = 0;
                                //        TotalSGST = 0;
                                //        TotalIGST = TotalIGST + decimal.Parse(dataSet12.Tables[1].Rows[i]["IGSTAmt"].ToString());
                                //        AmountAfterTax = TotalGross + TotalIGST;
                                //    }
                                //}

                                //#region value_details
                                //ValueDetails valueDetails = new ValueDetails();
                                //valueDetails.total_assessable_value = TotalGross;
                                //valueDetails.total_cgst_value = TotalCGST;
                                //valueDetails.total_sgst_value = TotalSGST;
                                //valueDetails.total_igst_value = TotalIGST;
                                //valueDetails.total_cess_value = 0;
                                //valueDetails.total_cess_value_of_state = 0;
                                //valueDetails.total_discount = 0;
                                //valueDetails.total_other_charge = 0;
                                //valueDetails.round_off_amount = 0;
                                //valueDetails.total_invoice_value_additional_currency = 0;
                                //valueDetails.total_invoice_value = AmountAfterTax;
                                //generateEinvoice.value_details = valueDetails;
                                //#endregion value_details
                                
                                //List<EItemList> lstItems = new List<EItemList>();
                                //int k = 1;
                                //#region item_list
                                //for (int j = 0; j <= dataSet12.Tables[1].Rows.Count - 1; j++)
                                //{
                                //    TotalCGST = 0; TotalSGST = 0; AmountAfterTax = 0; TotalIGST = 0;
                                //    EItemList eItemList = new EItemList();
                                //    eItemList.item_serial_number = k.ToString();
                                //    eItemList.product_description = dataSet12.Tables[1].Rows[j]["ProductName"].ToString();
                                //    eItemList.is_service = "N";
                                //    eItemList.hsn_code = dataSet12.Tables[1].Rows[j]["HSNCode"].ToString();
                                //    eItemList.bar_code = "";
                                //    eItemList.quantity = int.Parse(dataSet12.Tables[1].Rows[j]["DispatchQty"].ToString());
                                //    eItemList.unit = dataSet12.Tables[1].Rows[j]["UnitName"].ToString();
                                //    eItemList.unit_price = decimal.Parse(dataSet12.Tables[1].Rows[j]["Rate"].ToString());
                                //    eItemList.total_amount = eItemList.unit_price * eItemList.quantity;

                                //    eItemList.pre_tax_value = decimal.Parse(dataSet12.Tables[1].Rows[j]["Rate"].ToString());
                                //    eItemList.assessable_value = eItemList.total_amount;
                                //    TotalGross = decimal.Parse(dataSet12.Tables[1].Rows[j]["Gross"].ToString());
                                //    if (dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString() == "MAHARASHTRA")
                                //    {
                                //        TotalCGST =  decimal.Parse(dataSet12.Tables[1].Rows[j]["CGSTAmt"].ToString());
                                //        TotalSGST =  decimal.Parse(dataSet12.Tables[1].Rows[j]["SGSTAmt"].ToString());
                                //        TotalIGST = 0;
                                //        AmountAfterTax = TotalGross + TotalCGST + TotalSGST;
                                //        eItemList.gst_rate = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTPerc"].ToString());
                                //        eItemList.cgst_amount = decimal.Parse(dataSet12.Tables[1].Rows[j]["CGSTAmt"].ToString());
                                //        eItemList.sgst_amount = decimal.Parse(dataSet12.Tables[1].Rows[j]["SGSTAmt"].ToString());
                                //        eItemList.igst_amount = 0;
                                //        eItemList.total_item_value = AmountAfterTax;
                                //    }
                                //    else
                                //    {
                                //        TotalCGST = 0;
                                //        TotalSGST = 0;
                                //        TotalIGST =  decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTAmt"].ToString());
                                //        AmountAfterTax = TotalGross + TotalIGST;
                                //        eItemList.gst_rate = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTPerc"].ToString());
                                //        eItemList.cgst_amount = 0;
                                //        eItemList.sgst_amount = 0;
                                //        eItemList.igst_amount = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTAmt"].ToString());
                                //        eItemList.total_item_value = AmountAfterTax;
                                //    }



                                //    eItemList.free_quantity = 0;
                                //    eItemList.discount = 0;
                                //    eItemList.other_charge = 0;
                                //    eItemList.cess_rate = 0;
                                //    eItemList.cess_amount = 0;
                                //    eItemList.cess_nonadvol_amount = 0;
                                //    eItemList.state_cess_rate = 0;
                                //    eItemList.state_cess_amount = 0;
                                //    eItemList.state_cess_nonadvol_amount = 0;
                                //    eItemList.country_origin = "";
                                //    eItemList.order_line_reference = "";
                                //    eItemList.product_serial_number = "";
                                //    k++;
                                //    lstItems.Add(eItemList);
                                //}
                                //#endregion item_list

                                //generateEinvoice.item_list = lstItems;
                                //string url = "https://karyon.organic/api/karyon/GenerateEinvoice";
                                //string result1 = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(generateEinvoice));


                                //#endregion HITEinvoiceAPI
                            

                            return RedirectToAction("CreateCustomerOrder");
                        }
                        else
                        {
                            TempData["CreateOrder"] = dataSet12.Tables[0].Rows[0]["Message"].ToString();
                        }
                    }
                }
            }


            DataSet dataSet1123 = createOrder.GetCustomerTempOrder();
            createOrder.dtDetails = dataSet1123.Tables[0];
            return View(createOrder);
        }

        public ActionResult GetFranchiseStockDetails(string ProductId)
        {
            Stock stock = new Stock();
            stock.Page = 1;
            stock.Size = SessionManager.Size;
            stock.Fk_VarientId = int.Parse(ProductId);
            stock.FranchiseLoginId = HttpContext.Session.GetString("LoginId");
            DataSet dataSet = stock.GetFranchiseStock();
            List<Stock> lstStock = new List<Stock>();
            stock.MRP = dataSet.Tables[0].Rows[0]["MRP"].ToString();
            stock.PV = dataSet.Tables[0].Rows[0]["PV"].ToString();
            stock.OfferedPrice = dataSet.Tables[0].Rows[0]["OfferedPrice"].ToString();
            stock.AvailableQty = (int.Parse(dataSet.Tables[0].Rows[0]["CreditQty"].ToString()) - int.Parse(dataSet.Tables[0].Rows[0]["DebitQty"].ToString())).ToString();
            lstStock.Add(stock);
            return Json(lstStock);
        }

        public ActionResult OrderList(CreateOrder createOrder)
        {
            string Body = "";
            string obj1 = "{\"OpCode\":\"" + 9 + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            List<SelectListItem> ddlStatus = new List<SelectListItem>();
            ddlStatus.Add(new SelectListItem { Text = "All Status", Value = "0" });
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlStatus.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlStatus = ddlStatus;
            }
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }

            createOrder.Size = SessionManager.Size;
            createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 1;
            createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
            createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
            createOrder.FranchiseId = HttpContext.Session.GetString("LoginId");
            DataSet dataSet = createOrder.GetCustomerOrders();
            createOrder.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                createOrder.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(createOrder);
        }

        public ActionResult PrintFranchiseOrder(string OrderNo)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;
            DataSet dataSet = createOrder.PrintFranchiseOrder();
            ViewBag.OrderDate = dataSet.Tables[0].Rows[0]["OrderDate"].ToString();
            ViewBag.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
            ViewBag.ContactPerson = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
            ViewBag.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
            ViewBag.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
            ViewBag.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();
            ViewBag.Pincode = dataSet.Tables[0].Rows[0]["Pincode"].ToString();
            ViewBag.StateName = dataSet.Tables[0].Rows[0]["StateName"].ToString();
            ViewBag.DistrictName = dataSet.Tables[0].Rows[0]["DistrictName"].ToString();
            ViewBag.StateCode = dataSet.Tables[0].Rows[0]["StateCode"].ToString();
            ViewBag.TotalPVInWords = dataSet.Tables[2].Rows[0]["TotalPVInWords"].ToString();
            createOrder.dtDetails = dataSet.Tables[1];

            return View(createOrder);
        }

        public ActionResult PendingOrders(CreateOrder createOrder, string Dispatch, string Skip)
        {
            DataSet dataSet = new DataSet();
            #region BindDispatchDetails
            string obj1 = "{\"OpCode\":\"" + 17 + "\",\"Value\":\"" + "" + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            List<SelectListItem> ddlDispatchDetails = new List<SelectListItem>();
            ddlDispatchDetails.Add(new SelectListItem { Text = "Select Dispatch Details", Value = "0" });
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlDispatchDetails.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlDispatchDetails = ddlDispatchDetails;
            }
            #endregion

            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }
            if (!string.IsNullOrEmpty(Dispatch))
            {


                createOrder.DispatchCount = createOrder.DispatchCount == 0 ? 1 : createOrder.DispatchCount + 1;
                for (int i = 0; i < int.Parse(Request.Form["Count"].ToString()); i++)
                {
                    createOrder.FranchiseId = HttpContext.Session.GetString("FranchiseId");
                    // createOrder.DispatchCount = int.Parse(HttpContext.Session.GetString("DispatchCount"))+1;
                    createOrder.Fk_ProductId = int.Parse(Request.Form["Fk_VarientId" + i].ToString());
                    createOrder.DispatchQty = int.Parse(Request.Form["DispatchQty" + i].ToString());

                    if (Request.Form["IsDispatch" + i] == "on")
                    {

                        dataSet = createOrder.DispatchOrder();
                        createOrder.dtDetails = dataSet.Tables[0];
                        if (createOrder.dtDetails.Rows[0]["Flag"].ToString() == "0")
                        {
                            ViewBag.Message = createOrder.dtDetails.Rows[0]["Message"].ToString();
                        }
                        else
                        {
                            ViewBag.Message = createOrder.dtDetails.Rows[0]["Message"].ToString();
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(Skip))
            {
                createOrder.FranchiseId = HttpContext.Session.GetString("FranchiseId");
                for (int i = 0; i < int.Parse(Request.Form["Count"].ToString()); i++)
                {
                    createOrder.Fk_ProductId = int.Parse(Request.Form["Fk_VarientId" + i].ToString());
                    if (Request.Form["IsDispatch" + i] == "on")
                    {
                        dataSet = createOrder.SkipFranchiseOrders();
                    }
                }

            }
            createOrder.Size = SessionManager.Size;
            createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 3;
            createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
            createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
            createOrder.Fk_ParentFranchiseId = HttpContext.Session.GetString("FranchiseId");
            dataSet = createOrder.GetFranchiseOrders();
            createOrder.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                createOrder.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(createOrder);
        }
        public ActionResult DispatchOrder(string OrderNo)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;
            createOrder.AddedBy = int.Parse(HttpContext.Session.GetString("FranchiseId"));
            DataSet dataSet = createOrder.DispatchOrder();
            return RedirectToAction("PendingOrders");
        }

        public ActionResult PrintCustomerOrder(string OrderNo)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;

            DataSet dataSet = createOrder.PrintCustomerOrder();
            ViewBag.ContactPerson = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
            ViewBag.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
            ViewBag.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
            ViewBag.Mobile = dataSet.Tables[0].Rows[0]["Mobile"].ToString();
            ViewBag.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
            ViewBag.DispatchCount = dataSet.Tables[0].Rows[0]["DispatchCount"].ToString();
            ViewBag.OrderDate = dataSet.Tables[0].Rows[0]["OrderDate"].ToString();
            ViewBag.InvoiceDate = dataSet.Tables[0].Rows[0]["InvoiceDate"].ToString();
            ViewBag.PinCode = dataSet.Tables[0].Rows[0]["PinCode"].ToString();
            ViewBag.ConsignerPinCode = dataSet.Tables[0].Rows[0]["ConsignerPinCode"].ToString();
            ViewBag.InvoiceNo = dataSet.Tables[0].Rows[0]["InvoiceNo"].ToString();
            ViewBag.AmountInWords = dataSet.Tables[2].Rows[0]["AmountInWords"].ToString();
            ViewBag.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();
            ViewBag.State = dataSet.Tables[0].Rows[0]["State"].ToString();
            ViewBag.EQRCodeUrl = dataSet.Tables[0].Rows[0]["EQRCodeUrl"].ToString();
            ViewBag.irn = dataSet.Tables[0].Rows[0]["irn"].ToString();
            ViewBag.AckNo = dataSet.Tables[0].Rows[0]["AckNo"].ToString();
            ViewBag.AckDt = dataSet.Tables[0].Rows[0]["AckDt"].ToString();

            createOrder.dtDetails = dataSet.Tables[1];
            createOrder.dtOrderDetails = dataSet.Tables[3];
            return View(createOrder);
        }

        public ActionResult PendingCustomerOrders(CreateOrder createOrder, string Dispatch, string Skip)
        {
            #region BindDispatchDetails
            string obj1 = "{\"OpCode\":\"" + 17 + "\",\"Value\":\"" + "" + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            List<SelectListItem> ddlDispatchDetails = new List<SelectListItem>();
            ddlDispatchDetails.Add(new SelectListItem { Text = "Select Dispatch Details", Value = "0" });
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlDispatchDetails.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlDispatchDetails = ddlDispatchDetails;
            }
            #endregion
            DataSet dataSet = new DataSet();
            DataSet dataSet12 = new DataSet();
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }
            if (!string.IsNullOrEmpty(Dispatch))
            {
                createOrder.DispatchCount = createOrder.DispatchCount == 0 ? 1 : createOrder.DispatchCount + 1;
                for (int i = 0; i < int.Parse(Request.Form["Count"].ToString()); i++)
                {
                    //if (i== int.Parse(Request.Form["Count"].ToString())-1)
                    //{
                    //    createOrder.DispatchCount = 1;
                    //}
                    createOrder.FranchiseId = HttpContext.Session.GetString("FranchiseId");
                    // createOrder.DispatchCount = int.Parse(HttpContext.Session.GetString("DispatchCount"))+1;
                    createOrder.Fk_ProductId = int.Parse(Request.Form["Fk_VarientId" + i].ToString());
                    createOrder.DispatchQty = int.Parse(Request.Form["DispatchQty" + i].ToString());
                    if (Request.Form["IsDispatch" + i] == "on")
                    {

                        dataSet = createOrder.DispatchCustomerOrder();
                        createOrder.dtDetails = dataSet.Tables[0];
                        if (createOrder.dtDetails.Rows[0]["Flag"].ToString() == "0")
                        {
                            ViewBag.Message = createOrder.dtDetails.Rows[0]["Message"].ToString();
                        }
                        else
                        {
                            ViewBag.Message = createOrder.dtDetails.Rows[0]["Message"].ToString();
                        }
                    }
                }
                try
                {
                    #region HITEinvoiceAPI
                    GenerateEinvoice generateEinvoice = new GenerateEinvoice();
                    generateEinvoice.FK_MemId = dataSet12.Tables[0].Rows[0]["Fk_MemId"].ToString();
                    generateEinvoice.user_gstin = CommonEwayBIll.userGstin;
                    generateEinvoice.data_source = "erp";
                    #region TransactionDetails
                    TransactionDetails transactionDetails = new TransactionDetails();
                    transactionDetails.supply_type = "B2B";
                    transactionDetails.charge_type = "N";
                    transactionDetails.igst_on_intra = "N";
                    transactionDetails.ecommerce_gstin = "";
                    generateEinvoice.transaction_details = transactionDetails;
                    #endregion TransactionDetails
                    #region document_details
                    DocumentDetails documentDetails = new DocumentDetails();
                    documentDetails.document_type = "INV";
                    documentDetails.document_number = dataSet12.Tables[0].Rows[0]["OrderNo"].ToString();
                    documentDetails.document_date = dataSet12.Tables[0].Rows[0]["OrderDate"].ToString();
                    generateEinvoice.document_details = documentDetails;
                    #endregion document_details
                    #region seller_details
                    SellerDetails sellerDetails = new SellerDetails();
                    sellerDetails.gstin = CommonEwayBIll.userGstin;
                    sellerDetails.legal_name = CommonEwayBIll.legal_name_of_consignor;
                    sellerDetails.trade_name = CommonEwayBIll.legal_name_of_consignor;
                    sellerDetails.address1 = CommonEwayBIll.address1_of_consignor;
                    sellerDetails.address2 = CommonEwayBIll.address2_of_consignor;
                    sellerDetails.location = CommonEwayBIll.place_of_consignor;
                    sellerDetails.pincode = CommonEwayBIll.pincode_of_consignor;
                    sellerDetails.state_code = CommonEwayBIll.state_of_consignor;
                    sellerDetails.phone_number = CommonEwayBIll.phone_number;
                    sellerDetails.email = CommonEwayBIll.email;
                    generateEinvoice.seller_details = sellerDetails;
                    #endregion seller_details
                    #region buyer_details
                    BuyerDetails buyerDetails = new BuyerDetails();
                    buyerDetails.gstin = dataSet12.Tables[0].Rows[0]["GstNo"].ToString();
                    buyerDetails.phone_number = long.Parse(dataSet12.Tables[0].Rows[0]["MobileNo"].ToString());
                    buyerDetails.email = "";
                    buyerDetails.place_of_supply = dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
                    buyerDetails.address1 = dataSet12.Tables[0].Rows[0]["Address"].ToString();
                    buyerDetails.address2 = dataSet12.Tables[0].Rows[0]["Address1"].ToString();
                    buyerDetails.location = dataSet12.Tables[0].Rows[0]["Location"].ToString();
                    buyerDetails.pincode = int.Parse(dataSet12.Tables[0].Rows[0]["Pincode"].ToString());
                    buyerDetails.state_code = dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
                    generateEinvoice.buyer_details = buyerDetails;
                    #endregion buyer_details
                    #region dispatch_details
                    DispatchDetails dispatchDetails = new DispatchDetails();
                    dispatchDetails.company_name = CommonEwayBIll.legal_name_of_consignor;
                    dispatchDetails.address1 = dataSet12.Tables[0].Rows[0]["FAddress"].ToString();
                    dispatchDetails.address2 = "";
                    dispatchDetails.location = dataSet12.Tables[0].Rows[0]["FLocation"].ToString();
                    dispatchDetails.pincode = int.Parse(dataSet12.Tables[0].Rows[0]["FPincode"].ToString());
                    dispatchDetails.state_code = dataSet12.Tables[0].Rows[0]["FState"].ToString();
                    generateEinvoice.dispatch_details = dispatchDetails;
                    #endregion dispatch_details
                    #region ship_details
                    ShipDetails shipDetails = new ShipDetails();
                    shipDetails.gstin = dataSet12.Tables[0].Rows[0]["GstNo"].ToString();
                    shipDetails.address1 = dataSet12.Tables[0].Rows[0]["Address"].ToString();
                    shipDetails.address2 = dataSet12.Tables[0].Rows[0]["Address1"].ToString();
                    shipDetails.location = dataSet12.Tables[0].Rows[0]["Location"].ToString();
                    shipDetails.pincode = int.Parse(dataSet12.Tables[0].Rows[0]["Pincode"].ToString());
                    shipDetails.state_code = dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
                    generateEinvoice.ship_details = shipDetails;
                    #endregion ship_details

                    decimal TotalGross = 0, TotalCGST = 0, TotalSGST = 0, AmountAfterTax = 0, TotalIGST = 0;
                    for (int i = 0; i <= dataSet12.Tables[1].Rows.Count - 1; i++)
                    {

                        TotalGross = TotalGross + decimal.Parse(dataSet12.Tables[1].Rows[i]["Gross"].ToString());
                        if (dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString() == "MAHARASHTRA")
                        {
                            TotalCGST = TotalCGST + decimal.Parse(dataSet12.Tables[1].Rows[i]["CGSTAmt"].ToString());
                            TotalSGST = TotalSGST + decimal.Parse(dataSet12.Tables[1].Rows[i]["SGSTAmt"].ToString());
                            TotalIGST = 0;
                            AmountAfterTax = TotalGross + TotalCGST + TotalSGST;
                        }
                        else
                        {
                            TotalCGST = 0;
                            TotalSGST = 0;
                            TotalIGST = TotalIGST + decimal.Parse(dataSet12.Tables[1].Rows[i]["IGSTAmt"].ToString());
                            AmountAfterTax = TotalGross + TotalIGST;
                        }
                    }

                    #region value_details
                    ValueDetails valueDetails = new ValueDetails();
                    valueDetails.total_assessable_value = TotalGross;
                    valueDetails.total_cgst_value = TotalCGST;
                    valueDetails.total_sgst_value = TotalSGST;
                    valueDetails.total_igst_value = TotalIGST;
                    valueDetails.total_cess_value = 0;
                    valueDetails.total_cess_value_of_state = 0;
                    valueDetails.total_discount = 0;
                    valueDetails.total_other_charge = 0;
                    valueDetails.round_off_amount = 0;
                    valueDetails.total_invoice_value_additional_currency = 0;
                    valueDetails.total_invoice_value = AmountAfterTax;
                    generateEinvoice.value_details = valueDetails;
                    #endregion value_details

                    List<EItemList> lstItems = new List<EItemList>();
                    int k = 1;
                    #region item_list
                    for (int j = 0; j <= dataSet12.Tables[1].Rows.Count - 1; j++)
                    {
                        TotalCGST = 0; TotalSGST = 0; AmountAfterTax = 0; TotalIGST = 0;
                        EItemList eItemList = new EItemList();
                        eItemList.item_serial_number = k.ToString();
                        eItemList.product_description = dataSet12.Tables[1].Rows[j]["ProductName"].ToString();
                        eItemList.is_service = "N";
                        eItemList.hsn_code = dataSet12.Tables[1].Rows[j]["HSNCode"].ToString();
                        eItemList.bar_code = "";
                        eItemList.quantity = int.Parse(dataSet12.Tables[1].Rows[j]["DispatchQty"].ToString());
                        eItemList.unit = dataSet12.Tables[1].Rows[j]["UnitName"].ToString();
                        eItemList.unit_price = decimal.Parse(dataSet12.Tables[1].Rows[j]["Rate"].ToString());
                        eItemList.total_amount = eItemList.unit_price * eItemList.quantity;

                        eItemList.pre_tax_value = decimal.Parse(dataSet12.Tables[1].Rows[j]["Rate"].ToString());
                        eItemList.assessable_value = eItemList.total_amount;
                        TotalGross = decimal.Parse(dataSet12.Tables[1].Rows[j]["Gross"].ToString());
                        if (dataSet12.Tables[0].Rows[0]["PlaceOfSupply"].ToString() == "MAHARASHTRA")
                        {
                            TotalCGST = decimal.Parse(dataSet12.Tables[1].Rows[j]["CGSTAmt"].ToString());
                            TotalSGST = decimal.Parse(dataSet12.Tables[1].Rows[j]["SGSTAmt"].ToString());
                            TotalIGST = 0;
                            AmountAfterTax = TotalGross + TotalCGST + TotalSGST;
                            eItemList.gst_rate = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTPerc"].ToString());
                            eItemList.cgst_amount = decimal.Parse(dataSet12.Tables[1].Rows[j]["CGSTAmt"].ToString());
                            eItemList.sgst_amount = decimal.Parse(dataSet12.Tables[1].Rows[j]["SGSTAmt"].ToString());
                            eItemList.igst_amount = 0;
                            eItemList.total_item_value = AmountAfterTax;
                        }
                        else
                        {
                            TotalCGST = 0;
                            TotalSGST = 0;
                            TotalIGST = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTAmt"].ToString());
                            AmountAfterTax = TotalGross + TotalIGST;
                            eItemList.gst_rate = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTPerc"].ToString());
                            eItemList.cgst_amount = 0;
                            eItemList.sgst_amount = 0;
                            eItemList.igst_amount = decimal.Parse(dataSet12.Tables[1].Rows[j]["IGSTAmt"].ToString());
                            eItemList.total_item_value = AmountAfterTax;
                        }



                        eItemList.free_quantity = 0;
                        eItemList.discount = 0;
                        eItemList.other_charge = 0;
                        eItemList.cess_rate = 0;
                        eItemList.cess_amount = 0;
                        eItemList.cess_nonadvol_amount = 0;
                        eItemList.state_cess_rate = 0;
                        eItemList.state_cess_amount = 0;
                        eItemList.state_cess_nonadvol_amount = 0;
                        eItemList.country_origin = "";
                        eItemList.order_line_reference = "";
                        eItemList.product_serial_number = "";
                        k++;
                        lstItems.Add(eItemList);
                    }
                    #endregion item_list

                    generateEinvoice.item_list = lstItems;
                    string url = "https://karyon.organic/api/karyon/GenerateEinvoice";
                    string result1 = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(generateEinvoice));


                    #endregion HITEinvoiceAPI

                }
                catch { }


            }
            if (!string.IsNullOrEmpty(Skip))
            {
                for (int i = 0; i < int.Parse(Request.Form["Count"].ToString()); i++)
                {
                    createOrder.Fk_ProductId = int.Parse(Request.Form["Fk_VarientId" + i].ToString());
                    if (Request.Form["IsDispatch" + i] == "on")
                    {
                        dataSet = createOrder.SkipOrders();
                    }
                }

            }
            createOrder.Size = SessionManager.Size;
            //createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 3;
            createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
            createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
            createOrder.Fk_DispatchId = HttpContext.Session.GetString("FranchiseId");
            createOrder.Status = "Placed";
            dataSet = createOrder.GetCustomerOrders();
            createOrder.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                createOrder.Pager = pager;
                // ViewBag.pager = pager;
            }
            //HttpContext.Session.SetString("DispatchCount", dataSet.Tables[0].Rows[0]["DispatchCount"].ToString());
            return View(createOrder);
        }
        public ActionResult DispatchedCustomerOrders(CreateOrder createOrder, string Dispatch)
        {
            DataSet dataSet = new DataSet();
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }

            createOrder.Size = SessionManager.Size;
            //createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 4;
            createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
            createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
            createOrder.Fk_DispatchId = HttpContext.Session.GetString("FranchiseId");

            dataSet = createOrder.GetCustomerOrders();
            createOrder.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                createOrder.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(createOrder);
        }

        public ActionResult GetPincodeFranchise(string Pincode)
        {

            string Body = "";
            string obj1 = "{\"OpCode\":\"" + 18 + "\",\"Value\":\"" + Pincode + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetPincodeFranchise, Body);
            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);


            return Json(masterDataResponse);
        }
        public ActionResult UpdatePickupFranchisee(string FranchiseeId, string OrderNo)
        {

            string Body = "";
            string obj1 = "{\"AddedBy\":\"" + int.Parse(HttpContext.Session.GetString("FranchiseId")) + "\",\"OrderNo\":\"" + OrderNo + "\",\"FranchiseId\":\"" + FranchiseeId + "\"}";
            Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.UpdatePickupFranchiseeForFranchiseeOrder, Body);
            UpdatePickupFranchiseResponse updatePickupFranchiseResponse = new UpdatePickupFranchiseResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            updatePickupFranchiseResponse = JsonConvert.DeserializeObject<UpdatePickupFranchiseResponse>(dcdata);
            return Json(updatePickupFranchiseResponse);

        }

        public ActionResult DispatchedFranchiseOrders(CreateOrder createOrder, string Dispatch)
        {
            DataSet dataSet = new DataSet();
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }

            createOrder.Size = SessionManager.Size;
            //createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 4;
            createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
            createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
            createOrder.Fk_ParentFranchiseId = HttpContext.Session.GetString("FranchiseId");

            dataSet = createOrder.GetFranchiseOrders();
            createOrder.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                createOrder.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(createOrder);
        }

        public ActionResult PrintCustomerDispatchNote(string OrderNo, string DispatchCount)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;
            createOrder.AddedBy = int.Parse(HttpContext.Session.GetString("FranchiseId"));
            createOrder.DispatchCount = int.Parse(DispatchCount);
            DataSet dataSet = createOrder.PrintDispatchFranchiseOrder();
            ViewBag.ContactPerson = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
            ViewBag.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
            ViewBag.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
            ViewBag.Mobile = dataSet.Tables[0].Rows[0]["Mobile"].ToString();
            ViewBag.ConsignerName = dataSet.Tables[0].Rows[0]["ConsignerName"].ToString();
            ViewBag.ConsignerId = dataSet.Tables[0].Rows[0]["ConsignerId"].ToString();
            ViewBag.ConsignerAddress = dataSet.Tables[0].Rows[0]["ConsignerAddress"].ToString();
            ViewBag.ConsignerMobile = dataSet.Tables[0].Rows[0]["ConsignerMobile"].ToString();
            ViewBag.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
            ViewBag.DispatchCount = dataSet.Tables[0].Rows[0]["DispatchCount"].ToString();
            ViewBag.OrderDate = dataSet.Tables[0].Rows[0]["OrderDate"].ToString();
            ViewBag.InvoiceDate = dataSet.Tables[0].Rows[0]["InvoiceDate"].ToString();
            ViewBag.PinCode = dataSet.Tables[0].Rows[0]["PinCode"].ToString();
            ViewBag.ConsignerPinCode = dataSet.Tables[0].Rows[0]["ConsignerPinCode"].ToString();
            ViewBag.InvoiceNo = dataSet.Tables[0].Rows[0]["InvoiceNo"].ToString();
            ViewBag.AmountInWords = dataSet.Tables[2].Rows[0]["AmountInWords"].ToString();
            ViewBag.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();
            ViewBag.ShippingCharges = decimal.Parse(dataSet.Tables[0].Rows[0]["ShippingCharges"].ToString());

            createOrder.dtDetails = dataSet.Tables[1];
            createOrder.dtOrderDetails = dataSet.Tables[3];

            return View(createOrder);
        }
        public ActionResult PrintFranchiseeDispatchNote(string OrderNo, string DispatchCount)
        {
            CreateOrder createOrder = new CreateOrder();
            createOrder.OrderNo = OrderNo;
            createOrder.DispatchCount = int.Parse(DispatchCount);
            createOrder.AddedBy = int.Parse(HttpContext.Session.GetString("FranchiseId"));
            DataSet dataSet = createOrder.PrintDispatchParentFranchiseOrder();
            ViewBag.ContactPerson = dataSet.Tables[0].Rows[0]["ContactPerson"].ToString();
            ViewBag.LoginId = dataSet.Tables[0].Rows[0]["LoginId"].ToString();
            ViewBag.Address = dataSet.Tables[0].Rows[0]["Address"].ToString();
            ViewBag.ConsigneeState = dataSet.Tables[0].Rows[0]["ConsigneeState"].ToString();
            ViewBag.Mobile = dataSet.Tables[0].Rows[0]["Mobile"].ToString();
            ViewBag.ConsignerName = dataSet.Tables[0].Rows[0]["ConsignerName"].ToString();
            ViewBag.ConsignerId = dataSet.Tables[0].Rows[0]["ConsignerId"].ToString();
            ViewBag.ConsignerAddress = dataSet.Tables[0].Rows[0]["ConsignerAddress"].ToString();
            ViewBag.ConsignerMobile = dataSet.Tables[0].Rows[0]["ConsignerMobile"].ToString();
            ViewBag.OrderNo = dataSet.Tables[0].Rows[0]["OrderNo"].ToString();
            ViewBag.DispatchCount = dataSet.Tables[0].Rows[0]["DispatchCount"].ToString();
            ViewBag.OrderDate = dataSet.Tables[0].Rows[0]["OrderDate"].ToString();
            ViewBag.InvoiceDate = dataSet.Tables[0].Rows[0]["InvoiceDate"].ToString();
            ViewBag.PinCode = dataSet.Tables[0].Rows[0]["PinCode"].ToString();
            ViewBag.ConsignerPinCode = dataSet.Tables[0].Rows[0]["ConsignerPinCode"].ToString();
            ViewBag.InvoiceNo = dataSet.Tables[0].Rows[0]["InvoiceNo"].ToString();
            ViewBag.AmountInWords = dataSet.Tables[2].Rows[0]["AmountInWords"].ToString();
            ViewBag.GSTNo = dataSet.Tables[0].Rows[0]["GSTNo"].ToString();

            createOrder.dtDetails = dataSet.Tables[1];
            createOrder.dtOrderDetails = dataSet.Tables[3];
            return View(createOrder);
        }
        public async Task<ActionResult> FrachiseViewProfile(ProfileRequest profileRequest, string btnUpdate)
        {
            ViewBag.KYCTabStatus = "";
            ViewBag.Tab3Status = "";
            ViewBag.PersonalDetails = "active";
            ProfileReposne profileReposne = new ProfileReposne();
            if (profileRequest.ImageFile != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.ImageFile, "FranchisePanCard");
                profileRequest.PanImage = fileName;
                profileRequest.Pk_FranchiseId = HttpContext.Session.GetString("FranchiseId");
                DataSet dataSet = profileRequest.FranchiseUpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";

            }
            if (profileRequest.AddressProofFront != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.AddressProofFront, "FranchiseAddressProofFront");
                profileRequest.AddressProofImage = fileName;
                profileRequest.Pk_FranchiseId = HttpContext.Session.GetString("FranchiseId");
                DataSet dataSet = profileRequest.FranchiseUpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";
            }
            if (profileRequest.AddressProofBack != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.AddressProofBack, "FranchiseAddressProofBack");
                profileRequest.AddressProofBackImage = fileName;
                profileRequest.Pk_FranchiseId = HttpContext.Session.GetString("FranchiseId");
                DataSet dataSet = profileRequest.FranchiseUpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";
            }
            if (profileRequest.BankDoc != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.BankDoc, "FranchiseBankDoc");
                profileRequest.BankDocImage = fileName;
                profileRequest.Pk_FranchiseId = HttpContext.Session.GetString("FranchiseId");
                DataSet dataSet = profileRequest.FranchiseUpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";
            }
            if (profileRequest.ProfilePic != null)
            {
                string fileName = await FileManagement.WriteFiles(profileRequest.ProfilePic, "FranchiseBankDoc");
                profileRequest.ProfileImageUrl = fileName;
                profileRequest.Pk_FranchiseId = HttpContext.Session.GetString("FranchiseId");
                DataSet dataSet = profileRequest.FranchiseUpdateKYC();
                ViewBag.KYCTabStatus = "active";
                ViewBag.Tab3Status = "active";
                ViewBag.PersonalDetails = "";
            }
            if (!string.IsNullOrEmpty(btnUpdate))
            {


                string obj = "{\"Pk_FranchiseId\":\"" + HttpContext.Session.GetString("FranchiseId") + "\",\"Email\":\"" + profileRequest.Email + "\",\"Address\":\"" + profileRequest.Address + "\",\"Pincode\":\"" + profileRequest.Pincode + "\",\"State\":\"" + profileRequest.State + "\",\"City\":\"" + profileRequest.City + "\",\"AccNo\":\"" + profileRequest.AccNo + "\",\"BankName\":\"" + profileRequest.BankName + "\",\"Branch\":\"" + profileRequest.Branch + "\",\"IFSC\":\"" + profileRequest.IFSC + "\",\"PanCard\":\"" + profileRequest.PanCard + "\",\"HolderName\":\"" + profileRequest.HolderName + "\",\"GSTNo\":\"" + profileRequest.GSTNo + "\"}";
                string Body1 = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
                string result1 = Common.HITAPI(APIURL.UpdateFranchiseProfile, Body1);
                ShoppingWebResponse deserializedProduct1 = JsonConvert.DeserializeObject<ShoppingWebResponse>(result1);
                string dcdata1 = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct1.body);
                profileReposne = JsonConvert.DeserializeObject<ProfileReposne>(dcdata1);
                if (profileReposne.Status == "1")
                {
                    AdminRenderMenu(1);
                    // RedirectToAction("FrachiseViewProfile");

                }
                else
                {
                    TempData["Registraion"] = profileReposne.Message;
                }

            }
            string obj1 = "{\"Pk_FranchiseId\":\"" + HttpContext.Session.GetString("FranchiseId") + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.FranchiseViewProfile, Body);
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            profileReposne = JsonConvert.DeserializeObject<ProfileReposne>(dcdata);
            if (profileReposne.Status == "1")
            {
                profileRequest.Address = profileReposne.Response.Address;
                profileRequest.Pincode = profileReposne.Response.Pincode;
                profileRequest.City = profileReposne.Response.City;
                profileRequest.State = profileReposne.Response.State;
                profileRequest.Pincode = profileReposne.Response.Pincode;
                profileRequest.ContactPerson = profileReposne.Response.ContactPerson;
                profileRequest.CompanyName = profileReposne.Response.CompanyName;
                profileRequest.Email = profileReposne.Response.Email;
                profileRequest.Gender = profileReposne.Response.Gender;
                profileRequest.MobileNo = profileReposne.Response.MobileNo;
                profileRequest.AccNo = profileReposne.Response.AccNo;
                profileRequest.Branch = profileReposne.Response.Branch;
                profileRequest.IFSC = profileReposne.Response.IFSC;
                TempData["PanImage"] = profileReposne.Response.PanImage;
                profileRequest.PanCard = profileReposne.Response.PanCard;
                profileRequest.BankName = profileReposne.Response.BankName;
                profileRequest.HolderName = profileReposne.Response.HolderName;
                profileRequest.RankName = profileReposne.Response.RankName;
                profileRequest.TotalOrders = profileReposne.Response.TotalOrders;
                profileRequest.JoiningDate = profileReposne.Response.JoiningDate;
                //profileRequest.Status = profileReposne.Response.Status;
                //profileRequest.SponsorId = profileReposne.Response.SponsorId;
                //profileRequest.SponsorName = profileReposne.Response.SponsorName;
                profileRequest.PanStatus = profileReposne.Response.PanStatus;
                profileRequest.AddressFrontStatus = profileReposne.Response.AddressFrontStatus;
                profileRequest.AddressBackStatus = profileReposne.Response.AddressBackStatus;
                profileRequest.BankStatus = profileReposne.Response.BankStatus;
                profileRequest.GSTNo = profileReposne.Response.GSTNo;
                TempData["AddressProofFrontURL"] = profileReposne.Response.AddressProofFrontURL;
                TempData["AddressProofBackURL"] = profileReposne.Response.AddressProofBackURL;
                TempData["BankDocURL"] = profileReposne.Response.BankDocURL;
                TempData["ProfilePicURL"] = profileReposne.Response.ProfilePicURL;
                //TempData["ViewProfile"] = profileReposne.Message;
            }
            return View(profileRequest);
        }

        public ActionResult CounterCollect(CreateOrder createOrder, string Dispatch, string Skip, string Search)
        {
            #region BindDispatchDetails
            string obj1 = "{\"OpCode\":\"" + 17 + "\",\"Value\":\"" + "" + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            List<SelectListItem> ddlDispatchDetails = new List<SelectListItem>();
            ddlDispatchDetails.Add(new SelectListItem { Text = "Select Dispatch Details", Value = "0" });
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlDispatchDetails.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlDispatchDetails = ddlDispatchDetails;
            }
            #endregion
            DataSet dataSet = new DataSet();
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }
            if (!string.IsNullOrEmpty(Dispatch))
            {
                createOrder.DispatchCount = createOrder.DispatchCount == 0 ? 1 : createOrder.DispatchCount + 1;
                for (int i = 0; i < int.Parse(Request.Form["Count"].ToString()); i++)
                {
                    //if (i== int.Parse(Request.Form["Count"].ToString())-1)
                    //{
                    //    createOrder.DispatchCount = 1;
                    //}
                    createOrder.FranchiseId = HttpContext.Session.GetString("FranchiseId");
                    // createOrder.DispatchCount = int.Parse(HttpContext.Session.GetString("DispatchCount"))+1;
                    createOrder.Fk_ProductId = int.Parse(Request.Form["Fk_VarientId" + i].ToString());
                    createOrder.DispatchQty = int.Parse(Request.Form["DispatchQty" + i].ToString());
                    if (Request.Form["IsDispatch" + i] == "on")
                    {

                        dataSet = createOrder.DispatchCounterCollectOrder();
                        createOrder.dtDetails = dataSet.Tables[0];
                        if (createOrder.dtDetails.Rows[0]["Flag"].ToString() == "0")
                        {
                            TempData["CounterCollect"] = createOrder.dtDetails.Rows[0]["Message"].ToString();
                        }
                        else
                        {
                            TempData["CounterCollect"] = createOrder.dtDetails.Rows[0]["Message"].ToString();
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(Skip))
            {
                for (int i = 0; i < int.Parse(Request.Form["Count"].ToString()); i++)
                {
                    createOrder.Fk_ProductId = int.Parse(Request.Form["Fk_VarientId" + i].ToString());
                    if (Request.Form["IsDispatch" + i] == "on")
                    {
                        dataSet = createOrder.SkipOrders();
                    }
                }

            }
            if (!string.IsNullOrEmpty(Search))
            {
                createOrder.Size = SessionManager.Size;
                //createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
                createOrder.OpCode = 5;
                createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
                createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
                createOrder.Fk_DispatchId = HttpContext.Session.GetString("FranchiseId");
                createOrder.Status = "Placed";
                dataSet = createOrder.GetCustomerOrders();
                createOrder.dtDetails = dataSet.Tables[0];
                int? totalRecords = 0;
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                    createOrder.Pager = pager;
                    return View(createOrder);
                    // ViewBag.pager = pager;
                }
            }
            return View();
            //HttpContext.Session.SetString("DispatchCount", dataSet.Tables[0].Rows[0]["DispatchCount"].ToString());

        }
        public async Task<ActionResult> FamilynWalletRequest(FamilynFranchiseeWalletRequest franchiseWallet, string Request)
        {
            franchiseWallet.LoginId = HttpContext.Session.GetString("LoginId").ToString();
            franchiseWallet.Name = HttpContext.Session.GetString("FirstName").ToString();

            #region BindPaymentMode
            string obj1 = "{\"OpCode\":\"" + 4 + "\",\"Value\":\"" + "" + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.GetMasterData, Body);

            MasterDataResponse masterDataResponse = new MasterDataResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            masterDataResponse = JsonConvert.DeserializeObject<MasterDataResponse>(dcdata);
            if (masterDataResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(masterDataResponse.Response.RequestList);
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                ddlPaymentMode.Add(new SelectListItem { Text = "Payment Mode", Value = "0" });
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow r in dataTable.Rows)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Name"].ToString() });
                    }
                }
                ViewBag.ddlPaymentMode = ddlPaymentMode;
            }
            #endregion

            if (!string.IsNullOrEmpty(Request))
            {
                if (franchiseWallet.ImageFile != null)
                {
                    string fileName = await FileManagement.WriteFiles(franchiseWallet.ImageFile, "FamilynFranchiseeWalletRequest");
                    franchiseWallet.PaymentSlip = fileName;
                }
                obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"Amount\":\"" + franchiseWallet.Amount + "\",\"PaymentMode\":\"" + franchiseWallet.PaymentMode + "\",\"TransactionNo\":\"" + franchiseWallet.TransactionNo + "\",\"TransactionDate\":\"" + franchiseWallet.TransactionDate + "\",\"PaymentSlip\":\"" + franchiseWallet.PaymentSlip + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
                result = Common.HITAPI(APIURL.FranchiseeFamilynWalletRequest, Body);
                FamilynFranchiseeWalletResponse familynWalletResponse = new FamilynFranchiseeWalletResponse();
                deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                familynWalletResponse = JsonConvert.DeserializeObject<FamilynFranchiseeWalletResponse>(dcdata);
                if (familynWalletResponse.Status == 1)
                {
                    TempData["FamilynWalletRequest"] = Messages.WalletSuccess;
                    return RedirectToAction("FamilynWalletList");
                }
                TempData["FamilynWalletRequest"] = familynWalletResponse.Message;
            }
            return View(franchiseWallet);
        }
        public ActionResult FamilynWalletList(FamilynFranchiseeWalletListRequest familynFranchiseeWalletListRequest)
        {

            if (familynFranchiseeWalletListRequest.Page == 0 || familynFranchiseeWalletListRequest.Page == null)
            {
                familynFranchiseeWalletListRequest.Page = 1;
            }
            string obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"Page\":\"" + familynFranchiseeWalletListRequest.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.FranchiseeFamilynWalletList, Body);

            FamilynFranchiseeWalletListResponse familynFranchiseeWalletListResponse = new FamilynFranchiseeWalletListResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            familynFranchiseeWalletListResponse = JsonConvert.DeserializeObject<FamilynFranchiseeWalletListResponse>(dcdata);
            if (familynFranchiseeWalletListResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(familynFranchiseeWalletListResponse.Response.WalletList);
                familynFranchiseeWalletListRequest.dtDetails = dataTable;
                int totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataTable.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, familynFranchiseeWalletListRequest.Page, SessionManager.Size);
                    familynFranchiseeWalletListRequest.Pager = pager;
                }
            }
            return View(familynFranchiseeWalletListRequest);
        }
        public ActionResult FamilynWalletLedger(FamilynFranchiseeWalletLedgerRequest familynFranchiseeWalletLedgerRequest)
        {
            if (familynFranchiseeWalletLedgerRequest.Page == 0 || familynFranchiseeWalletLedgerRequest.Page == null)
            {
                familynFranchiseeWalletLedgerRequest.Page = 1;
            }
            string obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"Page\":\"" + familynFranchiseeWalletLedgerRequest.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.FranchiseeFamilynWalletLedger, Body);

            FamilynFranchiseeWalletLedgerResponse familynFranchiseeWalletLedgerResponse = new FamilynFranchiseeWalletLedgerResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            familynFranchiseeWalletLedgerResponse = JsonConvert.DeserializeObject<FamilynFranchiseeWalletLedgerResponse>(dcdata);
            if (familynFranchiseeWalletLedgerResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(familynFranchiseeWalletLedgerResponse.Response.WalletLedger);
                familynFranchiseeWalletLedgerRequest.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataTable.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, familynFranchiseeWalletLedgerRequest.Page, SessionManager.Size);
                    familynFranchiseeWalletLedgerRequest.Pager = pager;
                }
            }

            return View(familynFranchiseeWalletLedgerRequest);
        }
        public ActionResult FranchiseePayout(FranchiseePayoutRequest franchiseePayout)
        {
            if (franchiseePayout.Page == 0 || franchiseePayout.Page == null)
            {
                franchiseePayout.Page = 1;
            }
            string obj1 = "{\"FranchiseId\":\"" + HttpContext.Session.GetString("FranchiseId") + "\",\"Page\":\"" + franchiseePayout.Page + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.FranchiseePayout, Body);

            FranchiseePayoutResponse franchiseePayoutResponse = new FranchiseePayoutResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            franchiseePayoutResponse = JsonConvert.DeserializeObject<FranchiseePayoutResponse>(dcdata);
            if (franchiseePayoutResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(franchiseePayoutResponse.Response.PayoutList);
                franchiseePayout.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataTable.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, franchiseePayout.Page, SessionManager.Size);
                    franchiseePayout.Pager = pager;
                }
            }
            return View(franchiseePayout);
        }
        public ActionResult FranchiseePayoutDetails(string PayoutNo)
        {
            FranchiseePayoutDetails franchiseePayout = new FranchiseePayoutDetails();
            string obj = "{\"FranchiseId\":\"" + HttpContext.Session.GetString("FranchiseId") + "\",\"PayoutNo\":\"" + PayoutNo + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
            string result = Common.HITAPI(APIURL.FranchiseePayoutDetails, Body);

            FranchiseePayoutDetailsResponse franchiseePayoutResponse = new FranchiseePayoutDetailsResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            franchiseePayoutResponse = JsonConvert.DeserializeObject<FranchiseePayoutDetailsResponse>(dcdata);
            if (franchiseePayoutResponse.Status == 1)
            {
                franchiseePayout.PayoutList = franchiseePayoutResponse.Response.PayoutList;
            }
            return Json(franchiseePayout.PayoutList);
        }
        public ActionResult FranchiseePayoutLedger(FranchiseePayoutLedgerRequest franchiseePayoutLedgerRequest)
        {
            if (franchiseePayoutLedgerRequest.Page == 0 || franchiseePayoutLedgerRequest.Page == null)
            {
                franchiseePayoutLedgerRequest.Page = 1;
            }
            string obj1 = "{\"LoginId\":\"" + HttpContext.Session.GetString("LoginId") + "\",\"Page\":\"" + franchiseePayoutLedgerRequest.Page + "\",\"Size\":\"" + SessionManager.Size + "\"}";
            string Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj1);
            string result = Common.HITAPI(APIURL.FranchiseePayoutLedger, Body);

            FranchiseePayoutLedgerResponse FranchiseePayoutLedgerResponse = new FranchiseePayoutLedgerResponse();
            ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
            string dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
            FranchiseePayoutLedgerResponse = JsonConvert.DeserializeObject<FranchiseePayoutLedgerResponse>(dcdata);
            if (FranchiseePayoutLedgerResponse.Status == 1)
            {
                DataTable dataTable = Common.ToDataTable(FranchiseePayoutLedgerResponse.Response.payoutList);
                franchiseePayoutLedgerRequest.dtDetails = dataTable;
                int? totalRecords = 0;
                if (dataTable.Rows.Count > 0)
                {
                    totalRecords = int.Parse(dataTable.Rows[0]["TotalRecords"].ToString());
                    var pager = new Pager(totalRecords, franchiseePayoutLedgerRequest.Page, SessionManager.Size);
                    franchiseePayoutLedgerRequest.Pager = pager;
                }
            }

            return View(franchiseePayoutLedgerRequest);
        }
        public ActionResult OpenOrder(OpenOrderRequest openOrderRequest, string Add, string Save, string Pk_Id)
        {
            DataSet dataSet = new DataSet();
            #region BindProducts
            Stock stock = new Stock();
            stock.Page = 1;
            stock.Size = 100;
            stock.Fk_VarientId = 0;
            stock.FranchiseLoginId = HttpContext.Session.GetString("LoginId");
            dataSet = stock.GetFranchiseStock();
            List<SelectListItem> ddlProducts = new List<SelectListItem>();
            ddlProducts.Add(new SelectListItem { Text = "Select Product", Value = "0" });
            if (dataSet != null)
            {
                if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dataSet.Tables[0].Rows)
                    {
                        ddlProducts.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Id"].ToString() });
                    }
                }
                ViewBag.ddlProducts = ddlProducts;
            }
            #endregion BindProducts

            string obj = "";
            string Body = "";
            string result = "";
            string dcdata = "";
            openOrderRequest.OrderType = "OpenOrder";
            openOrderRequest.AddedBy = int.Parse(HttpContext.Session.GetString("FranchiseId"));
            if (!string.IsNullOrEmpty(Add))
            {
                obj = "{\"Qty\":\"" + openOrderRequest.Qty + "\",\"Amount\":\"" + openOrderRequest.Amount + "\",\"OrderType\":\"" + openOrderRequest.OrderType + "\",\"AddedBy\":\"" + openOrderRequest.AddedBy + "\",\"Fk_ProductId\":\"" + openOrderRequest.Fk_ProductId + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
                result = Common.HITAPI(APIURL.AddTempOpenOrder, Body);
                OpenOrderTempResponse openOrderTempResponse = new OpenOrderTempResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                openOrderTempResponse = JsonConvert.DeserializeObject<OpenOrderTempResponse>(dcdata);
                if (openOrderTempResponse.Status == 1)
                {
                    TempData["OpenOrder"] = openOrderTempResponse.Message;
                    DataTable dataTable = Common.ToDataTable(openOrderTempResponse.Response.TempOpenOrderList);
                    openOrderRequest.dtDetails = dataTable;
                }
                else
                {
                    TempData["OpenOrder"] = openOrderTempResponse.Message;
                }
            }
            else if (!string.IsNullOrEmpty(Save))
            {
                obj = "{\"AddedBy\":\"" + openOrderRequest.AddedBy + "\",\"BillAddress\":\"" + openOrderRequest.BillAddress + "\",\"BillTo\":\"" + openOrderRequest.BillTo + "\",\"BillContactNo\":\"" + openOrderRequest.BillContactNo + "\",\"LoginId\":\"" + openOrderRequest.LoginId + "\",\"OrderType\":\"" + openOrderRequest.OrderType + "\",\"CommissionPer\":\"" + openOrderRequest.CommissionPer + "\",\"BillGSTNo\":\"" + openOrderRequest.BillGSTNo + "\",\"Pincode\":\"" + openOrderRequest.Pincode + "\",\"State\":\"" + openOrderRequest.State + "\",\"City\":\"" + openOrderRequest.City + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
                result = Common.HITAPI(APIURL.GenerateOpenOrder, Body);
                OpenOrderResponse openOrderResponse = new OpenOrderResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                openOrderResponse = JsonConvert.DeserializeObject<OpenOrderResponse>(dcdata);
                if (openOrderResponse.Status == 1)
                {
                    TempData["OpenOrder"] = openOrderResponse.Message;
                }
                else
                {
                    TempData["OpenOrder"] = openOrderResponse.Message;
                }
            }
            else if (!string.IsNullOrEmpty(Pk_Id))
            {
                openOrderRequest.Pk_Id = Pk_Id;
                obj = "{\"Pk_Id\":\"" + openOrderRequest.Pk_Id + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
                result = Common.HITAPI(APIURL.DeleteTempOpenOrder, Body);
                OpenOrderTempResponse openOrderResponse = new OpenOrderTempResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                openOrderResponse = JsonConvert.DeserializeObject<OpenOrderTempResponse>(dcdata);
                if (openOrderResponse.Status == 1)
                {
                    DataTable dataTable = Common.ToDataTable(openOrderResponse.Response.TempOpenOrderList);
                    openOrderRequest.dtDetails = dataTable;
                    return View(openOrderRequest);
                }
                else
                {
                    TempData["OpenOrder"] = openOrderResponse.Message;
                }
            }
            else
            {
                obj = "{\"AddedBy\":\"" + openOrderRequest.AddedBy + "\"}";
                Body = ApiEncrypt_Decrypt.EncryptString(Aeskey, obj);
                result = Common.HITAPI(APIURL.GetTempOpenOrder, Body);
                OpenOrderTempResponse openOrderTempResponse = new OpenOrderTempResponse();
                ShoppingWebResponse deserializedProduct = JsonConvert.DeserializeObject<ShoppingWebResponse>(result);
                dcdata = ApiEncrypt_Decrypt.DecryptString(Aeskey, deserializedProduct.body);
                openOrderTempResponse = JsonConvert.DeserializeObject<OpenOrderTempResponse>(dcdata);
                if (openOrderTempResponse.Status == 1)
                {
                    DataTable dataTable = Common.ToDataTable(openOrderTempResponse.Response.TempOpenOrderList);
                    openOrderRequest.dtDetails = dataTable;
                }
                else
                {
                    TempData["OpenOrder"] = openOrderTempResponse.Message;
                }
            }
            return View(openOrderRequest);
        }


        public ActionResult UploadCustomerDispatchDetailExcel(UploadDispatchDetails obj, string btnsave, string ExportToExcel)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnsave))
                {
                    DataTable dataTable = new DataTable();
                    dataTable = (DataTable)JsonConvert.DeserializeObject(obj.JsonstringWallet, (typeof(DataTable)));
                    obj.dtDetails = dataTable;
                    for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
                    {
                        obj.OpCode = 2;
                        obj.OrderNo = dataTable.Rows[i]["OrderNo"].ToString();
                        obj.DocketNo = dataTable.Rows[i]["DocketNo"].ToString();
                        obj.Transport = dataTable.Rows[i]["Transport"].ToString();
                        obj.DispatchCount = dataTable.Rows[i]["DispatchCount"].ToString();
                        obj.Route = dataTable.Rows[i]["Route"].ToString();
                        //obj.Date = Common.ConvertToSystemDate(dataTable.Rows[i]["Date"].ToString(), "dd/MM/yyyy");
                        DataSet dataSet = obj.UploadExcelData();

                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["UploadCustomerDispatchDetailExcel"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else
                            {
                                TempData["UploadCustomerDispatchDetailExcel"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                return View(obj);
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ExportToExcel))
                {
                    obj.Fk_EmpId = HttpContext.Session.GetString("FranchiseId").ToString();
                    obj.OpCode = 2;
                    DataSet dataSet = obj.DispatchExcel();
                    obj.dtDetails = dataSet.Tables[0];
                    try
                    {
                        DataTable dt = obj.dtDetails;
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DispatchList.xlsx");
                            }
                        }
                        return View(obj);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UpdateCustomerDispatchDetailExcel(UploadDispatchDetails obj, string btnsave)
        {
            if (!string.IsNullOrEmpty(btnsave))
            {
                DataTable dataTable = new DataTable();
                dataTable = (DataTable)JsonConvert.DeserializeObject(obj.JsonstringWallet, (typeof(DataTable)));
                obj.dtDetails = dataTable;

                for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
                {
                    obj.OrderNo = dataTable.Rows[i]["OrderNo"].ToString();
                    obj.Status = dataTable.Rows[i]["Status"].ToString();
                    obj.Date = Common.ConvertToSystemDate(dataTable.Rows[i]["Date"].ToString(), "dd/MM/yyyy");
                    DataSet dataSet = obj.UpdateExcelData();
                    if (dataSet != null && dataSet.Tables.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["UploadCustomerDispatchDetailExcel"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                        }
                        else
                        {
                            TempData["UploadCustomerDispatchDetailExcel"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            return View(obj);
                        }
                    }
                }
            }
            return View(obj);
        }
        public ActionResult AdminRenderMenu(int OpCode)
        {
            Menu model = new Menu();

            List<Menu> list = new List<Menu>();
            List<Menu> list1 = new List<Menu>();
            DataSet dataset = new DataSet();
            model.Fk_MemId = HttpContext.Session.GetString("FK_FranchiseTypeId");
            model.Fk_FranchiseId = HttpContext.Session.GetString("FranchiseId");
            model.OpCode = OpCode;
            dataset = model.GetMenuDetails();
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                for (var i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    Menu ListData = new Menu();
                    ListData.MenuName = dataset.Tables[0].Rows[i]["MenuName"].ToString();
                    ListData.MenuId = dataset.Tables[0].Rows[i]["MenuId"].ToString();
                    ListData.Url = dataset.Tables[0].Rows[i]["MenuURL"].ToString();
                    //ListData.IsDropdown = dataset.Tables[0].Rows[i]["IsDropdown"].ToString();
                    ListData.Icon = dataset.Tables[0].Rows[i]["MenuIcon"].ToString();
                    list.Add(ListData);
                }
                for (int j = 0; j <= dataset.Tables[1].Rows.Count - 1; j++)
                {
                    Menu SubList = new Menu();
                    SubList.SubMenuName = dataset.Tables[1].Rows[j]["SubMenuName"].ToString();
                    //SubList.MenuName = dataset.Tables[1].Rows[j]["MenuName"].ToString();
                    SubList.Fk_MenuId = dataset.Tables[1].Rows[j]["Fk_MenuId"].ToString();
                    SubList.SubMenuId = int.Parse(dataset.Tables[1].Rows[j]["SubMenuId"].ToString());
                    SubList.Url = dataset.Tables[1].Rows[j]["SubMenuUrl"].ToString();

                    list1.Add(SubList);
                }

            }
            model.menuList = list;
            model.subMenuList = list1;
            HttpContext.Session.SetObjectAsJson("_menu", model);
            HttpContext.Session.SetObjectAsJson("_submenu", model);
            return PartialView("_FranchaiseePartialLayout");
        }
        public ActionResult UploadDispatchDetailExcel(UploadDispatchDetails obj, string btnsave, string ExportToExcel)
        {
            try
            {
                if (!string.IsNullOrEmpty(btnsave))
                {
                    DataTable dataTable = new DataTable();
                    dataTable = (DataTable)JsonConvert.DeserializeObject(obj.JsonstringWallet, (typeof(DataTable)));
                    obj.dtDetails = dataTable;
                    for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
                    {
                        obj.OpCode = 1;
                        obj.OrderNo = dataTable.Rows[i]["OrderNo"].ToString();
                        obj.DocketNo = dataTable.Rows[i]["DocketNo"].ToString();
                        obj.Transport = dataTable.Rows[i]["Transport"].ToString();
                        obj.DispatchCount = dataTable.Rows[i]["DispatchCount"].ToString();
                        obj.Route = dataTable.Rows[i]["Route"].ToString();
                        //obj.Date = Common.ConvertToSystemDate(dataTable.Rows[i]["Date"].ToString(), "dd/MM/yyyy");
                        DataSet dataSet = obj.UploadExcelData();

                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["Flag"].ToString() == "1")
                            {
                                TempData["UploadCustomerDispatchDetailExcel"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else
                            {
                                TempData["UploadCustomerDispatchDetailExcel"] = dataSet.Tables[0].Rows[0]["Message"].ToString();
                                return View(obj);
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ExportToExcel))
                {
                    obj.Fk_EmpId = HttpContext.Session.GetString("FranchiseId").ToString();
                    obj.OpCode = 1;
                    DataSet dataSet = obj.DispatchExcel();
                    obj.dtDetails = dataSet.Tables[0];
                    try
                    {
                        DataTable dt = obj.dtDetails;
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DispatchList.xlsx");
                            }
                        }
                        return View(obj);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GeneratedEwayBill(CreateOrder createOrder)
        {
            DataSet dataSet = new DataSet();
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }

            createOrder.Size = SessionManager.Size;
            //createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 6;
            createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
            createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
            createOrder.Fk_ParentFranchiseId = HttpContext.Session.GetString("FranchiseId");

            dataSet = createOrder.GetFranchiseOrders();
            createOrder.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                createOrder.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(createOrder);
        }

        public ActionResult GeneratedEwayBillCustomer(CreateOrder createOrder)
        {
            DataSet dataSet = new DataSet();
            if (createOrder.Page == null || createOrder.Page == 0)
            {
                createOrder.Page = 1;
            }

            createOrder.Size = SessionManager.Size;
            //createOrder.Status = createOrder.Status == "0" ? null : createOrder.Status;
            createOrder.OpCode = 9;
            createOrder.FromDate = string.IsNullOrEmpty(createOrder.FromDate) ? null : Common.ConvertToSystemDate(createOrder.FromDate, "dd/MM/yyyy");
            createOrder.ToDate = string.IsNullOrEmpty(createOrder.ToDate) ? null : Common.ConvertToSystemDate(createOrder.ToDate, "dd/MM/yyyy");
            createOrder.FranchiseId = HttpContext.Session.GetString("FranchiseId");

            dataSet = createOrder.GetCustomerOrders();
            createOrder.dtDetails = dataSet.Tables[0];
            int? totalRecords = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                totalRecords = int.Parse(dataSet.Tables[0].Rows[0]["TotalRecords"].ToString());
                var pager = new Pager(totalRecords, createOrder.Page, SessionManager.Size);
                createOrder.Pager = pager;
                // ViewBag.pager = pager;
            }
            return View(createOrder);
        }

    }

}
