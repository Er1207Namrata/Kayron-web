using System.Data;
using System.Data.SqlClient;

namespace Karyon.Models
{
    public class PaytmResponse : Menu
    {
        public string? MID { get; set; }
        public string? Pk_AddressId { get; set; }
        public string? CUST_ID { get; set; }
        public string? ORDERID { get; set; }
        public string? TXNAMOUNT { get; set; }
        public string? CURRENCY { get; set; }
        public string? TXNID { get; set; }
        public string? BANKTXNID { get; set; }
        public string? STATUS { get; set; }
        public string? RESPCODE { get; set; }
        public string? RESPMSG { get; set; }
        public string? TXNDATE { get; set; }
        public string? GATEWAYNAME { get; set; }
        public string? BANKNAME { get; set; }
        public string? PAYMENTMODE { get; set; }
        public string? CHECKSUMHASH { get; set; }
    }
    public class AddressRequest
    {
        public int CustomerId { get; set; }
        public int Pk_AddressId { get; set; }
        public int OpCode { get; set; }
        public string? Name { get; set; }
        public string? WalletType { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public string? Locality { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Landmark { get; set; }
        public string? AddressType { get; set; }
        public string? Type { get; set; }
        public string? MerchantTranId { get; set; }
        public string? Amount { get; set; }
        public string? LoginId { get; set; }
  
        public List<AddressList>? AddressList { get; set; }

        public DataSet ManageAddress()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Pk_AddressId",Pk_AddressId),
                                      new SqlParameter("@Name",Name),
                                      new SqlParameter("@MobileNo",MobileNo),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Locality",Locality),
                                      new SqlParameter("@Pincode",Pincode),
                                      new SqlParameter("@State",State),
                                      new SqlParameter("@City",City),
                                      new SqlParameter("@Landmark",Landmark),
                                      new SqlParameter("@AddressType",AddressType),
                                      new SqlParameter("@Fk_MemId",CustomerId),
                                      new SqlParameter("@OpCode",OpCode),
                                      new SqlParameter("@Type",Type)
                                  };

                DataSet ds = Connection.ExecuteQuery(ProcedureName.ManageAddress, para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    

    }
    public class AddressResponse
    {
        public Address? Response { get; set; }
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
    public class Address
    {
        public List<AddressList>? AddressList { get; set; }
    }
    public class AddressList
    {
        public int Pk_AddressId { get; set; }
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public string? Locality { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Landmark { get; set; }
        public string? AddressType { get; set; }

    }
}
