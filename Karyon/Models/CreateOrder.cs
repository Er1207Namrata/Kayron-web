using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class CreateOrder : Menu
    {
        public int Fk_ProductId { get; set; }
        public string? Fk_ParentFranchiseId { get; set; }
        public int Pk_Id { get; set; }
        public int AvailableQty { get; set; }
        public decimal? Amount { get; set; }
        public decimal? PV { get; set; }
        public int? Qty { get; set; }
        public int? BoxQty { get; set; }
        public decimal? SubTotal { get; set; }
        public string? OrderNo { get; set; }
        public string? ProductName { get; set; }
        public string? FranchiseId { get; set; }
        public string? Fk_DispatchId { get; set; }
        //public string? LoginId { get; set; }
        public string? PaymentMode { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionDate { get; set; }
        public string? FileName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? WalletBalance { get; set; }
        public string? BeforeTax { get; set; }
        public string? FUPPoints { get; set; }
        public string? OFPPoints { get; set; }
        public string? OrderAmount { get; set; }
        public string? TotalPV { get; set; }
        public string? Balance { get; set; }
        public string? IsDispatched { get; set; }
        public string? PendingQty { get; set; }
        public string? Type { get; set; }
        public string? DispatchDetails { get; set; }
        public string? OTP { get; set; }
        public string? CourierNo { get; set; }
        public string? CompanyName { get; set; }
        public string? OrderDate { get; set; }
        public int? Fk_OrderId { get; set; }
        public int? DispatchQty { get; set; }
        public int? DispatchCount { get; set; }
        public string? MobileNo { get; set; }
        public string? CustomerName { get; set; }
        public int? Stock { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string? Address { get; set; }
        public string? PurchaseBy { get; set; }
        public string? TxnId { get; set; }
        public string? BankTxnId { get; set; }
        public string? CheckSumHash { get; set; }
        public string? RespMsg { get; set; }
        public string? ReviewMsg { get; set; }
        public string? StarRating { get; set; }
        public string? GatewayAmount { get; set; }
        public string? OrderFrom { get; set; }
        public string? QRCODEIMG { get; set; }
        public string? merchantTranId { get; set; }
        public int Fk_VarientId { get; set; }
        public int CustomerId { get; set; }
        public string? PromoCode { get; set; }
        public string? OfferedPrice { get; set; }
        public string? SGST { get; set; }
        public string? CGST { get; set; }
        public string? IGST { get; set; }
        public string? Name { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public List<CreateOrder>? Orderdetails { get; set; }
        public List<ProductReviewData>? ReviewList { get; set; }
        public DataSet CreateOrderTemp()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_ProductId",Fk_ProductId),
                                      new SqlParameter("@Qty",Qty),
                                      new SqlParameter("@AddedBy",AddedBy),

                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.CreateOrderTemp, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet CreateCustomerOrderTemp()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Fk_ProductId",Fk_ProductId),
                                      new SqlParameter("@Qty",Qty),
                                      new SqlParameter("@AddedBy",AddedBy)
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CreateCustomerOrderTemp, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetTempOrder()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@AddedBy",AddedBy),
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetTempOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetOfferPointTempOrder()
        {
            try
            {
                SqlParameter[] para = {
                                      new SqlParameter("@AddedBy",AddedBy),
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetOfferPointTempOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCustomerTempOrder()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@AddedBy",AddedBy),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCustomerTempOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DeleteTempOrder()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@Pk_Id",Pk_Id),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteOrderTemp, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DeleteOfferPointTemp()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Pk_Id",Pk_Id),
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteOfferPointTemp, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DeleteCustomerTempOrder()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@Pk_Id",Pk_Id),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.DeleteCustomerTempOrder, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GenerateFranchiseOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@PaymentMode",  PaymentMode),
                    new SqlParameter("@TransactionDate",  TransactionDate),
                    new SqlParameter("@TransactionNo",  TransactionNo),
                    new SqlParameter("@FileName",  FileName),
                    new SqlParameter("@WalletType",  WalletType),
                    new SqlParameter("@Status",Status),
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@TxnId",TxnId),
                    new SqlParameter("@BankTxnId",BankTxnId),
                    new SqlParameter("@CheckSumHash",CheckSumHash),
                    new SqlParameter("@RespMsg",RespMsg),
                    new SqlParameter("@OpCode",OpCode)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GenerateFranchiseOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GenerateCustomerOrder()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@PaymentMode",  PaymentMode),
                                      new SqlParameter("@TransactionDate",  TransactionDate),
                                      new SqlParameter("@TransactionNo",  TransactionNo),
                                      new SqlParameter("@FileName",  FileName),
                                      new SqlParameter("@LoginId",  LoginId),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GenerateCustomerOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetRedeemofferpoint()
        {
            try
            {
                SqlParameter[] para = {


                                      new SqlParameter("@AddedBy",AddedBy),                                      
                                      new SqlParameter("@Name",Name),                                      
                                      new SqlParameter("@MobileNo",MobileNo),                                      
                                      new SqlParameter("@PinCode",Pincode),                                      
                                      new SqlParameter("@State",State),                                      
                                      new SqlParameter("@City",City),                                      
                                      new SqlParameter("@Address",Address),                                      
                                  };
                

                DataSet ds = Connection.ExecuteQuery(ProcedureName.Redeemofferpoint, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFranchiseOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@Status",Status), 
                    new SqlParameter("@FranchiseId",FranchiseId),
                    new SqlParameter("@Fk_ParentFranchiseId",Fk_ParentFranchiseId),
                    new SqlParameter("@DispatchCount",DispatchCount),
                    new SqlParameter("@Type",Type),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetFranchiseOrders, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCustomerOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@FromDate",FromDate),
                    new SqlParameter("@ToDate",ToDate),
                    new SqlParameter("@Status",Status),
                    new SqlParameter("@FranchiseId",FranchiseId),
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Type",Type),
                    new SqlParameter("@Page",Page),
                    new SqlParameter("@Size",Size),
                    new SqlParameter("@Fk_DispatchId",Fk_DispatchId),
                    new SqlParameter("@DispatchCount",DispatchCount)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCustomerOrders, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet PrintFranchiseOrder()
        {
            try
            {
                SqlParameter[] para = {



                                      new SqlParameter("@OrderNo",OrderNo)


                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.PrintFranchiseOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet PrintDispatchFranchiseOrder()
        {
            try
            {
                SqlParameter[] para = {



                                      new SqlParameter("@OrderNo",OrderNo),
                                      new SqlParameter("@DispatchCount",DispatchCount),
                                      new SqlParameter("@Fk_DispatchBy",AddedBy)


                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.PrintDispatchFranchiseOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet PrintDispatchParentFranchiseOrder()
        {
            try
            {
                SqlParameter[] para = {



                                      new SqlParameter("@OrderNo",OrderNo),
                                      new SqlParameter("@DispatchCount",DispatchCount),
                                      new SqlParameter("@Fk_DispatchBy",AddedBy)


                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.PrintDispatchParentFranchiseOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DispatchOrder()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_FranchiseeId",FranchiseId),
                                      new SqlParameter("@Fk_OrderId",Fk_OrderId),
                                      new SqlParameter("@Fk_VarientId",Fk_ProductId),
                                      new SqlParameter("@DispatchQty",DispatchQty),
                                      new SqlParameter("@DispatchCount",DispatchCount),
                                      new SqlParameter("@DispatchDetails",DispatchDetails),
                                      new SqlParameter("@CourierNo",CourierNo)
                                  };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DispatchOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet PrintCustomerOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OrderNo",OrderNo)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.PrintCustomerOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        } 
        public DataSet GetTodaysDispatch()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_FranchiseId",Fk_FranchiseId),
                    new SqlParameter("@Type",Type)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetTodaysDispatch, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet UpdatePickupFranchise()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_FranchiseeId",FranchiseId),
                                      new SqlParameter("@OrderNo",OrderNo),
                                      new SqlParameter("@AddedBy",AddedBy),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdatePickupFranchise, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet UpdatePickupFranchiseForFranchiseOrder()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_FranchiseeId",FranchiseId),
                                      new SqlParameter("@OrderNo",OrderNo),
                                      new SqlParameter("@AddedBy",AddedBy),

                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.UpdatePickupFranchiseForFranchiseOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DispatchCustomerOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_FranchiseeId",FranchiseId),
                    new SqlParameter("@Fk_OrderId",Fk_OrderId),
                    new SqlParameter("@Fk_VarientId",Fk_ProductId),
                    new SqlParameter("@DispatchQty",DispatchQty),
                    new SqlParameter("@DispatchCount",DispatchCount),
                    new SqlParameter("@DispatchDetails",DispatchDetails),
                    new SqlParameter("@CourierNo",CourierNo)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DispatchCustomerOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet DispatchCounterCollectOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_FranchiseeId",FranchiseId),
                    new SqlParameter("@Fk_OrderId",Fk_OrderId),
                    new SqlParameter("@Fk_VarientId",Fk_ProductId),
                    new SqlParameter("@DispatchQty",DispatchQty),
                    new SqlParameter("@DispatchCount",DispatchCount),
                    new SqlParameter("@DispatchDetails",DispatchDetails),
                    new SqlParameter("@CourierNo",CourierNo),
                    new SqlParameter("@OTP",OTP)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.DispatchCounterCollectOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SkipOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_OrderId",Fk_OrderId),
                    new SqlParameter("@Fk_VarientId",Fk_ProductId)
                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.SkipOrders, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SkipFranchiseOrders()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_OrderId",Fk_OrderId),
                    new SqlParameter("@Fk_VarientId",Fk_ProductId),
                    new SqlParameter("@AddedBy",FranchiseId)
                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.SkipFranchiseOrder, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet CounterPickup()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@AddedBy",AddedBy)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CounterPickup, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetStockDetails()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_FranchiseId",FranchiseId),
                    new SqlParameter("@Fk_VarientId",Fk_ProductId)
                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetStockDetails, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet CancelOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Addedby",Fk_MemId),
                    new SqlParameter("@Fk_OrderId",Fk_OrderId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CancelOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet CancelFranchiseeOrder()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Pk_OrderId",Fk_OrderId),
                    new SqlParameter("@AddedBy",FranchiseId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.CancelFranchiseeOrder, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCartAmount()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@Fk_MemId",Fk_MemId)
                };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetCartAmount, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet ApplyPromoCode()
        {
            DataSet ds = new DataSet();
            SqlParameter[] para = {
                new SqlParameter("@Fk_MemId",Fk_MemId),
                new SqlParameter("@Amount",Amount),
                new SqlParameter("@PromoCode",PromoCode)
            };
            ds = Connection.ExecuteQuery(ProcedureName.CheckPromoCode, para);
            return ds;
        }


        public DataSet GetOrderDetails()
        {
            try
            {
                SqlParameter[] para = {
                    new SqlParameter("@OrderNo",OrderNo)
                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetOrderDetails, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetEinvoiceData()
        {
            try
            {
                SqlParameter[] para = {


                                     
                                      new SqlParameter("@OrderNo",  OrderNo),

                                  };

                DataSet ds = Connection.ExecuteQuery("GetEinvoiceData", para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet CreateOfferPointTemp()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_ProductId",Fk_ProductId),
                                      new SqlParameter("@Qty",Qty),
                                      new SqlParameter("@AddedBy",AddedBy),

                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.CreateOfferPointTemp, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public DataSet GetAddressDetail()
        {
            try
            {
                SqlParameter[] para = {

                                      
                                      new SqlParameter("@FK_MemId",AddedBy),

                };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetAddress, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }

    public class UpdatePickupFranchiseResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }

    public class CreateOrderResponse
    {
        public OrderListResponse? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class OrderListResponse
    {
        public List<OrderData>? OrderList { get; set; }
    }
    public class OrderData
    {
        public string? OrderDate { get; set; }
        public string? OrderNo { get; set; }
        public decimal? OrderAmount { get; set; }
        public decimal? TotalPV { get; set; }
        public string? OrderType { get; set; }
        public string? Status { get; set; }
        public int? Fk_DispachFranchiseId { get; set; }
        public bool? IsCancel { get; set; }
        public string? Fk_OrderId { get; set; }
    }
    public class CounterCollectResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class SkipOrdersResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class DispatchOrdersReponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class DispatchCounterCollectResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class CancelOrderResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class CancelFranchiseeOrderResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class QrResponseImg
    {
        public string? QrCodeImg { get; set; }
        public string? Pk_RequestId { get; set; }
    }
    public class OrderDeteailsResponse
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
        public OrderDeteails? Response { get; set; }
    }
    public class OrderDeteails
    {

        public List<OrderDeteailsData>? listOrderDeteails { get; set; }
    }

    public class OrderDeteailsData
    {
        public decimal? Amount { get; set; }
        public decimal? PV { get; set; }
        public int? Qty { get; set; }
        public string? OrderNo { get; set; }
        public string? ProductName { get; set; }
        public string? CompanyName { get; set; }
        public string? DispatchStatus { get; set; }

    }
    public class PromoCodeResponse
    {
        public string? Message { get; set; }
        public int? Status { get; set; }
        public decimal? OrderAmount { get; set; }
    }

}
