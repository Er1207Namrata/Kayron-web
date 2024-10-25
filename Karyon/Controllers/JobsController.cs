using Karyon.Models;
using Karyon.Models.EwayBilling;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Karyon.Controllers
{

    [ApiController]
    public class JobsController : ControllerBase
    {
        [HttpGet]
        [Route("GenerateEinvoice")]
        public string GenerateEinvoice()
        {
            string result1 = "";
            Common common = new Common();
            DataSet dataSet = common.GetDataForEinvoice();
            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int jj = 0; jj <= dataSet.Tables[0].Rows.Count - 1; jj++)
                        {
                            string OrderNo = dataSet.Tables[0].Rows[jj]["OrderNo"].ToString();
                            CreateOrder createOrder = new CreateOrder();
                            createOrder.OrderNo = OrderNo;
                            DataSet dataSet12 = createOrder.GetEinvoiceData();
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
                            buyerDetails.legal_name = dataSet12.Tables[0].Rows[0]["LegalName"].ToString();
                            buyerDetails.trade_name = dataSet12.Tables[0].Rows[0]["TradeName"].ToString();
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
                            result1 = Common.HITEWAYAPI(url, JsonConvert.SerializeObject(generateEinvoice));


                            #endregion HITEinvoiceAPI


                        }
                    }
                }
            }

            return result1;
        }

        [HttpGet]
        [Route("ValidateGST")]
        public string ValidateGST()
        {
            string Status = "";
            string result1 = "";
            Common common = new Common();
            DataSet dataSet = common.GetDataFOrGstVerification();
            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int jj = 0; jj <= dataSet.Tables[0].Rows.Count - 1; jj++)
                        {
                            #region ValidateGst
                            ValidateGSTCommonResponse objresGst = new ValidateGSTCommonResponse();
                            ValidateGST validateGST = new ValidateGST();
                            validateGST.FK_MemId = dataSet.Tables[0].Rows[0]["FK_MemId"].ToString();
                            validateGST.gstin = dataSet.Tables[0].Rows[0]["GstNo"].ToString();
                            validateGST.user_gstin = "27AAHCK4739Q1ZQ";
                            string gsturl = "https://karyon.organic/api/karyon/ValidateGST";
                            string resultGst = Common.HITEWAYAPI(gsturl, JsonConvert.SerializeObject(validateGST));
                            objresGst = JsonConvert.DeserializeObject<ValidateGSTCommonResponse>(resultGst);
                            if (objresGst.results.code == 200)
                            {
                                ValidateGSTCommonResponse objres1 = JsonConvert.DeserializeObject<ValidateGSTCommonResponse>(resultGst);
                                objresGst.resultsuccess = new ValidateGSTResults();
                                objresGst.resultsuccess.message = new ValidateGStMessage();
                                objresGst.resultsuccess.message.Gstin = objres1.resultsuccess.message.Gstin;
                                objresGst.resultsuccess.message.TradeName = objres1.resultsuccess.message.TradeName;
                                objresGst.resultsuccess.message.LegalName = objres1.resultsuccess.message.LegalName;
                                objresGst.resultsuccess.message.AddrBnm = objres1.resultsuccess.message.AddrBnm;
                                objresGst.resultsuccess.message.AddrBno = objres1.resultsuccess.message.AddrBno;
                                objresGst.resultsuccess.message.AddrFlno = objres1.resultsuccess.message.AddrFlno;
                                objresGst.resultsuccess.message.AddrSt = objres1.resultsuccess.message.AddrSt;
                                objresGst.resultsuccess.message.AddrLoc = objres1.resultsuccess.message.AddrLoc;
                                objresGst.resultsuccess.message.StateCode = objres1.resultsuccess.message.StateCode;
                                objresGst.resultsuccess.message.AddrPncd = objres1.resultsuccess.message.AddrPncd;
                                objresGst.resultsuccess.message.TxpType = objres1.resultsuccess.message.TxpType;
                                objresGst.resultsuccess.message.Status = objres1.resultsuccess.message.Status;
                                objresGst.resultsuccess.message.BlkStatus = objres1.resultsuccess.message.BlkStatus;
                                objresGst.resultsuccess.message.DtReg = objres1.resultsuccess.message.DtReg;
                                objresGst.resultsuccess.message.DtDReg = objres1.resultsuccess.message.DtDReg;
                                DataSet dataSet12 = common.SaveAPILog(validateGST.FK_MemId, JsonConvert.SerializeObject(validateGST), resultGst, "Validate GST", validateGST.gstin, objres1.results.status);

                            }
                            else
                            {


                                ValidateGSTResponseError objres1 = JsonConvert.DeserializeObject<ValidateGSTResponseError>(resultGst);
                                objresGst.resultsError = new ValidateGSTResultsError();
                                objresGst.resultsError.code = objres1.results.code;
                                objresGst.resultsError.status = objres1.results.status;
                                objresGst.resultsError.requestId = objres1.results.requestId;
                                objresGst.resultsError.message = objres1.results.message;
                                DataSet dataSet12 = common.SaveAPILog(validateGST.FK_MemId, JsonConvert.SerializeObject(validateGST), resultGst, "Validate GST", validateGST.gstin, objres1.results.status);


                            }
                            #endregion ValidateGst
                        }
                    }
                }
            }
            return Status;
        }

        [HttpGet]
        [Route("UpdateFranchiseOrderStatus")]
        public string UpdateFranchiseOrderStatus()
        {
            
            Common common = new Common();
            DataSet dataSet = common.UpdateFranchiseOrderStatus();
            
            return null;
        }
    }
}
