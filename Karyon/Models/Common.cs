using Microsoft.AspNetCore.Http;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataTable = System.Data.DataTable;

namespace Karyon.Models
{
    public class Common
    {
        public Pager? Pager { get; set; }
        public int? Page { get; set; }
        public int Size { get; set; }
        public int OpCode { get; set; }
        public int AddedBy { get; set; }
        public string? FromDate { get; set; }
        public string? OrderNoSearch { get; set; }
        public string? ToDate { get; set; }
        public string? Status { get; set; }
        public string? ExportToExcel { get; set; }
        public string? Fk_MemId { get; set; }
        public string? Fk_FranchiseId { get; set; }
        public string? WalletType { get; set; }
        public DataTable? dtDetails { get; set; }
        public DataTable? dtSelfData { get; set; }
        public DataTable? dtDetailsR { get; set; }
        public DataTable? dtOrderDetails { get; set; }
        public DataTable? dtSecoundLevel { get; set; }
        public string? MobileNo { get; set; }
        public string? LoginType { get; set; }
        public string? Flag { get; set; }
        public DataTable? dtDetails1 { get; set; }
        public string? closingdate { get; set; }

        public static string MilliSecondToFormatDate(long timestamp, string outputDateFormat)
        {
            // Convert milliseconds to DateTime
            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
            // Return formatted date string
            return dateTime.ToString(outputDateFormat);
        }
        public DataSet GetAllMasters()
        {
            SqlParameter[] para = {
                new SqlParameter("@OpCode", OpCode)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
            return ds;
        }
        public DataSet GetClosingTime(string OrderId,string Type)
        {
            SqlParameter[] para = {
                new SqlParameter("@OrderId", OrderId),
                new SqlParameter("@Type", Type),
            };
            DataSet ds = Connection.ExecuteQuery("GetClosingTime", para);
            return ds;
        }
        public DataSet CommonCheckProductType()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_MemId", Fk_MemId),
                new SqlParameter("@WalletType", WalletType)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.CheckProduct, para);
            return ds;
        }
        public DataSet GetTermsLog()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo", MobileNo),
                new SqlParameter("@LoginType", LoginType)
            };
            DataSet ds = Connection.ExecuteQuery(ProcedureName.GetTermsLog, para);
            return ds;
        }  
        public DataSet GetDataForEinvoice()
        {
            DataSet ds = Connection.ExecuteQuery("GetDataForEinvoice");
            return ds;
        }
        public DataSet GetDataFOrGstVerification()
        {
            DataSet ds = Connection.ExecuteQuery("GetDataFOrGstVerification");
            return ds;
        } 
        public DataSet UpdateFranchiseOrderStatus()
        {
            DataSet ds = Connection.ExecuteQuery("UpdateFranchiseOrderStatus");
            return ds;
        }
        public DataSet SaveAPILog(string FK_MemId, string Request, string Response, string Type, string OrderId, string Status)
        {
            SqlParameter[] para = {
                new SqlParameter("@FK_MemId", FK_MemId),
                new SqlParameter("@Request", Request),
                new SqlParameter("@Response", Response),
                new SqlParameter("@Type", Type),
                new SqlParameter("@OrderId", OrderId),
                new SqlParameter("@Status", Status)
            };
            DataSet ds = Connection.ExecuteQuery("SaveAPILog", para);
            return ds;
        }

        public static string UploadFilesToRemoteUrl(string filePath, string uriString, NameValueCollection formFields = null)
        {
            string response = string.Empty;
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(uriString);
            string ContentType = "multipart/form-data; boundary=" + boundary;
            wr.ContentType = ContentType;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            if (formFields != null)
            {
                foreach (string key in formFields.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, formFields[key]);
                    byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);



            byte[] trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                response = reader2.ReadToEnd();
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            finally
            {
                if (wresp != null)
                {
                    wresp.Close();
                    wresp.Dispose();
                }
            }
            return response;
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string[] DatePart = InputDate.Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            string DateString;
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Year + "-" + Month + "-" + Day;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public string DecryptKey(byte[] stringToDecrypt)
        {
            X509Certificate2 certificate = getPrivateKey();
            RSA rsa_helper = (RSA)certificate.PrivateKey;

            RSAParameters certparams = rsa_helper.ExportParameters(false);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            RSAParameters paramcopy = new RSAParameters();
            paramcopy.Exponent = certparams.Exponent;
            paramcopy.Modulus = certparams.Modulus;
            rsa.ImportParameters(paramcopy);
            byte[] cryptedData = rsa.Decrypt(stringToDecrypt, false);
            return Convert.ToBase64String(cryptedData);
            //X509Certificate2 certificate = getPrivateKey();
            //RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PrivateKey;
            //byte[] cryptedData = null;
            //try
            //{
            //    cryptedData = rsa.Decrypt(stringToDecrypt, false);
            //}
            //catch (Exception ex)
            //{
            //    string abc = ex.Message;
            //}

            //return Encoding.UTF8.GetString(cryptedData, 0, cryptedData.Length);
        }

        public X509Certificate2 getPrivateKey()
        {
            string _keyURL = Path.Combine(Directory.GetCurrentDirectory(), "karyon_public.pfx");


            X509Certificate2 cert2 = null;

            try
            {
                cert2 = new X509Certificate2(_keyURL, "12345678", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
                cert2 = new X509Certificate2(_keyURL, "12345678", X509KeyStorageFlags.DefaultKeySet);
            }
            catch (CryptographicException ex)
            {

            }
            catch (Exception ex)
            {
            }
            Console.WriteLine(cert2);
            return cert2;
        }




        public string DecryptData(string data, string decryptedkey)
        {
            byte[] databyte1 = Encoding.UTF8.GetBytes(data);
            string ss = Convert.ToBase64String(databyte1);
            byte[] resbyte = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                resbyte[i] = databyte1[i];
            }
            string resp = AESDecryptNS(data, decryptedkey, resbyte);
            return resp;
        }

        public static string AESDecryptNS(string data, string pKey, byte[] piv)
        {
            string plaintext = string.Empty;
            byte[] inputBytes = Convert.FromBase64String(data);
            byte[] Key = Encoding.UTF8.GetBytes(pKey);
            byte[] iv = piv;// string.IsNullOrEmpty(piv) ? null : Encoding.UTF8.GetBytes(piv);

            byte[] plainText = GetCryptoAlgorithm().CreateDecryptor(Key, (iv == null ? Key : iv)).TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            plaintext = ASCIIEncoding.UTF8.GetString(plainText, 16, plainText.Length - 16);
            return plaintext.Replace("\u000e", string.Empty).Replace("\u0002", string.Empty).Replace("\u0006", string.Empty);
        }

        private static RijndaelManaged GetCryptoAlgorithm()
        {
            RijndaelManaged algorithm = new RijndaelManaged();
            //set the mode, padding and block size
            algorithm.Padding = PaddingMode.None;
            algorithm.Mode = CipherMode.CBC;
            algorithm.KeySize = 128;
            algorithm.BlockSize = 128;
            return algorithm;
        }




        public string EncryptData(string data)
        {
            byte[] databyte1 = Encoding.UTF8.GetBytes(data);

            byte[] RANDOMNO = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                RANDOMNO[i] = databyte1[i];
            }

            string encryptedKey = encryptRandomNo(RANDOMNO);

            AesManaged aes = new AesManaged();
            byte[] iv = aes.IV;

            byte[] ENCR_DATA = encryptSendData(data, RANDOMNO, iv);
            string encryptedData = Convert.ToBase64String(ENCR_DATA);
            var _parameterList = new Dictionary<string, object>();
            _parameterList.Add("encryptedKey", encryptedKey);
            _parameterList.Add("iv", Convert.ToBase64String(iv));
            _parameterList.Add("encryptedData", encryptedData);

            var encodejson = JsonConvert.SerializeObject(_parameterList);

            return encodejson;
        }

        public static byte[] encryptSendData(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);

                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public static string encryptRandomNo(byte[] stringToEncrypt)
        {
            X509Certificate2 certificate = getPublicKey();
            RSA rsa_helper = (RSA)certificate.PublicKey.Key;
            RSAParameters certparams = rsa_helper.ExportParameters(false);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            RSAParameters paramcopy = new RSAParameters();
            paramcopy.Exponent = certparams.Exponent;
            paramcopy.Modulus = certparams.Modulus;
            rsa.ImportParameters(paramcopy);
            byte[] cryptedData = rsa.Encrypt(stringToEncrypt, false);
            return Convert.ToBase64String(cryptedData);
            //RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key;

            //byte[] cryptedData = rsa.Encrypt(stringToEncrypt, false);

            //return Convert.ToBase64String(cryptedData);
        }

        public static X509Certificate2 getPublicKey()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            string _textURL = string.Empty;
            _textURL = Path.Combine(Directory.GetCurrentDirectory(), "karyon_organic.txt");
            //HttpContext.Current.Server.MapPath("/testkey/profile/bankkey/icicipub.txt");

            X509Certificate2 cert2 = new X509Certificate2(_textURL);

            return cert2;
        }

        public DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        public static string Aeskey = "Utility@#@132XYZ";
        public class ApiEncrypt_Decrypt
        {
            public static string EncryptString(string key, string plainText)
            {
                byte[] iv = new byte[16];
                byte[] array;

                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 128;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                            {
                                streamWriter.Write(plainText);
                            }

                            array = memoryStream.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(array);
            }
            public static string DecryptString(string key, string cipherText)
            {
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {

                    aes.KeySize = 128;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }

        }
        public class ShoppingWebResponse
        {
            public string? body { get; set; }
        }
        public class PaytmInitiateResponse
        {
            public body? body { get; set; }
            public head? head { get; set; }
        }
        public class head
        {
            public string? signature { get; set; }
        }
        public class body
        {
            public string? txnToken { get; set; }
            public string? bankTxnId { get; set; }
            public string? txnId { get; set; }
            public string? orderId { get; set; }
            public resultInfo? resultInfo { get; set; }
        }
        public class resultInfo
        {
            public string? resultCode { get; set; }
            public string? resultMsg { get; set; }
            public string? resultStatus { get; set; }
        }
        public static string HITAPI(string APIurl, string body)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    body = body

                });

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public static string HITAPI(string APIurl)
        {
            var result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public static string HITEWAYAPI(string APIurl, string json)
        {
            var result = "";
            try
            {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(APIurl);
                httpWebRequest.ContentType = "application/json";

                httpWebRequest.Method = "POST";
                using (var streamWriter = new

                StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                result = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            return result;
        }


        public static string SendMail(string SenderEmail, string Name)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                string MessageBody = "";
                MessageBody = "<h2>Hi " + Name + ",</h2>";
                message.From = new MailAddress(CompanyDetails.Email, CompanyDetails.ComapanyName);
                message.To.Add(new MailAddress(SenderEmail, Name));
                message.Subject = "Test Send Mail";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = MessageBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(CompanyDetails.Email, "Password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(message);
                return "Mail Sent Successfully!";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 8; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;
        }

    }
    [Serializable]
    public class Pager
    {
        public Pager(int? totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
        public int? TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }

    public class SessionManager : IDisposable
    {
        public static int Size => 30;

        public void Dispose()
        {

        }
    }
    public class MPaging
    {
        public int? Page { get; set; }
        public int Size { get; set; }
        public int TotalRecords { get; set; }
        public string? SearchKey { get; set; }
        public string? SearchValue { get; set; }
        public Pager? Pager { get; set; }
        public int EndPage { get; private set; }
    }
    public class Messages
    {
        public static string NoRecordFound = "No Record Found";
        public static string InvalidSponsorId = "Invalid Sponsor Id";
        public static string ProblemInConnection = "Problem In Connection.Please try after sometime.";
        public static string InvalidLoginId = "Invalid LoginId or Password";
        public static string InvalidOrderNo = "Invalid Order No.";
        public static string WalletSuccess = "Wallet Request Saved Successfully";
        public static string OtpSendSuccess = "Otp Send Successfully";
        public static string OtpVerifiedSuccess = "Otp Verified Successfully";
        public static string FileUploadedSuccess = "File Uploaded Successfully";
        public static string OrderPlaced = "Order Placed Successfully";
        public static string DispatchSuccess = "Product Dispatched Successfully";

    }
    //public class BaseUrl
    //{
    //    //public static string URL = "https://webapi.karyon.organic/api/karyon/";
    //    //public static string URL = "https://localhost:7095/api/karyon/";
    //    public static string URL = "https://karyon.organic/api/karyon/";
    //    public static string ImageURL = "https://admin.karyon.organic/";
    //    public static string LeftRightURL = "https://karyon.organic";
    //}

    public class BaseUrl
    {
        public static string? URL = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("URL").Value;
        public static string? ImageURL = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("ImageURL").Value;
        public static string? LeftRightURL = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("LeftRightURL").Value;
        public static string? LocalUrl = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("LocalUrl").Value;
    }
    public class SMSCredientials
    {
        public static string UserName = "karyon";
        public static string SenderId = "KARYON";
        public static string Password = "Pune123@";
        public static string DLTNo = "1101679880000010763";
        public static string ClientCode = "6e6a15080dXX";
        public static string URL = "http://redirect.ds3.in/submitsms.jsp?";
    }
    public class Gateway
    {
        public static string MIDLocal = "GRxxJu40963644697222";
        public static string CHANNEL_IDLocal = "WEB";
        public static string INDUSTRY_TYPE_IDLocal = "Retail";
        public static string WebSiteLocal = "WEBSTAGING";
        public static string MerchantIdLocal = "tJTE3IVwyaqstvfY";
        public static string PaytmURLLocal = "https://securegw-stage.paytm.in/theia/processTransaction";
        public static string CallBackURLLocal = "https://webapi.karyon.organic/Home/PaytmResponse";

        public static string CallBackURL = "https://karyon.organic/Home/PaytmResponse";
        public static string PaytmURL = "https://securegw.paytm.in/theia/processTransaction";
        public static string MID = "zSCfyY72432419176004";
        public static string CHANNEL_ID = "WEB";
        public static string INDUSTRY_TYPE_ID = "Retail";
        public static string WebSite = "DEFAULT";
        public static string MerchantId = "FiCMG3BkY8C&efYn";


    }
    public class SMSTemplate
    {
        public static string WelcomeSMS = "CONGRATULATIONS!! {#var#}ON BECOMING PART OF KARYON. YOUR ID NO: {#var#}AND PASSWORD: {#var#} VISIT OUR WEBISTE: www.karyon.organic";
        public static string OTP = "Dear Associate,OTP login request is {#var#}, OTP valid for 30 minute.http://karyon.organic/";
    }
    public class TemplateId
    {
        public static string OTPTemplateId = "1107161521219207952";
        public static string Registartion = "1107161744907182054";
        public static string FranchiseRegistartion = "1107166918602507107";
    }
    public class EwayDetails
    {

        public static string Username = "testeway@mastersindia.co";
        public static string Client_id = "fIXefFyxGNfDWOcCWnj";
        public static string Client_secret = "QFd6dZvCGqckabKxTapfZgJc";
        public static string Grant_type = "password";
        public static string Password = "!@#Demo!@#123";
        public static string Gstin_number = "05AAAAU6537D1ZO";
        public static string Eway_userName = "05AAAAU6537D1ZO";
        public static string Eway_password = "abc123@@";


    }
    public class EwayAPI
    {

        public static string EWayToken = "https://clientbasic.mastersindia.co/oauth/access_token";
        public static string GenerateBusiness = "https://clientbasic.mastersindia.co/";
    }
    public class APIURL
    {
        public static string ShoppingDashBaord = BaseUrl.URL + "GetShoppingDashBoard";
        public static string GetAllProducts = BaseUrl.URL + "GetAllProducts";
        public static string GetProductDetails = BaseUrl.URL + "GetProductDetails";
        public static string GetProductDetailsForOfferPoint = BaseUrl.URL + "GetProductDetailsForOfferPoint";
        public static string Login = BaseUrl.URL + "Login";
        public static string AdminLogin = BaseUrl.URL + "AdminLogin";
        public static string AddToCart = BaseUrl.URL + "AddToCart";
        public static string CartList = BaseUrl.URL + "CartList";
        public static string DeleteCart = BaseUrl.URL + "DeleteCart";
        public static string GetSponsorName = BaseUrl.URL + "GetSponsorName";
        public static string GetSponsorNameByNewId = BaseUrl.URL + "GetSponsorNameByNewId";
        public static string GetStateCity = BaseUrl.URL + "GetStateCity";
        public static string Registration = BaseUrl.URL + "Registration";
        public static string GetAssociateDashBoard = BaseUrl.URL + "GetAssociateDashBoard";
        public static string GetAssociateProfile = BaseUrl.URL + "GetAssociateProfile";
        public static string DirectList = BaseUrl.URL + "DirectList";
        public static string DownlineList = BaseUrl.URL + "DownlineList";
        public static string ChangePassword = BaseUrl.URL + "ChangePassword";
        public static string ViewProfile = BaseUrl.URL + "ViewProfile";
        public static string UpdateAssociateProfile = BaseUrl.URL + "UpdateAssociateProfile";
        public static string KaryonPointsRequest = BaseUrl.URL + "KaryonPointsRequest";
        public static string KaryonPointsList = BaseUrl.URL + "KaryonPointsList";
        public static string GetMasterData = BaseUrl.URL + "GetMasterData";
        public static string GetFranchiseSponsorName = BaseUrl.URL + "GetFranchiseSponsorName";
        public static string FranchiseRegistration = BaseUrl.URL + "FranchiseRegistration";
        public static string PlaceOrder = BaseUrl.URL + "PlaceOrder";
        public static string GetPincodeFranchise = BaseUrl.URL + "GetPincodeFranchise";
        public static string UpdatePickupFranchisee = BaseUrl.URL + "UpdatePickupFranchisee";
        public static string UpdatePickupFranchiseeForFranchiseeOrder = BaseUrl.URL + "UpdatePickupFranchiseeForFranchiseeOrder";
        public static string GetBusinessReport = BaseUrl.URL + "BusinessReport";
        public static string GetAddress = BaseUrl.URL + "GetAddress";
        public static string AddAddress = BaseUrl.URL + "AddUpdateAddress";
        public static string GetBusinessReportDetails = BaseUrl.URL + "BusinessReportDetails";
        public static string UpdateFranchiseProfile = BaseUrl.URL + "UpdateFranchiseProfile";
        public static string FranchiseViewProfile = BaseUrl.URL + "FranchiseViewProfile";
        public static string GetAssociatePayotReport = BaseUrl.URL + "AssociateDailyPayotReport";
        public static string AssociateWeeklyPayotReport = BaseUrl.URL + "AssociateWeeklyPayotReport";
        public static string LoginByAdmin = BaseUrl.URL + "LoginByAdmin";
        public static string HarmonyPayoutReport = BaseUrl.URL + "HarmonyPayoutReport";
        public static string CreatorPayoutReport = BaseUrl.URL + "CreatorPayoutReport";
        public static string Advisor1PayoutReport = BaseUrl.URL + "Advisor1PayoutReport";
        public static string Advisor2PayoutReport = BaseUrl.URL + "Advisor2PayoutReport";
        public static string Advisor3PayoutReport = BaseUrl.URL + "Advisor3PayoutReport";
        public static string RoyalPayoutReport = BaseUrl.URL + "RoyalPayoutReport";
        public static string FamilynWalletRequest = BaseUrl.URL + "FamilynWalletRequest";
        public static string FamilynWalletList = BaseUrl.URL + "FamilynWalletList";
        public static string FamilynWalletLedger = BaseUrl.URL + "FamilynWalletLedger";
        public static string FranchiseeFamilynWalletRequest = BaseUrl.URL + "FranchiseeFamilynWalletRequest";
        public static string FranchiseeFamilynWalletList = BaseUrl.URL + "FranchiseeFamilynWalletList";
        public static string FranchiseeFamilynWalletLedger = BaseUrl.URL + "FranchiseeFamilynWalletLedger";
        public static string AssociateWalletBalance = BaseUrl.URL + "AssociateWalletBalance";
        public static string FranchiseePayoutLedger = BaseUrl.URL + "FranchiseePayoutLedger";
        public static string AssociatePayoutLedger = BaseUrl.URL + "AssociatePayoutLedger";
        public static string FranchiseeWalletBalance = BaseUrl.URL + "FranchiseeWalletBalance";
        public static string RewardDetails = BaseUrl.URL + "RewardDetails";
        public static string DeleteAddress = BaseUrl.URL + "DeleteAddress";
        public static string FranchiseePayout = BaseUrl.URL + "FranchiseePayout";
        public static string FranchiseePayoutDetails = BaseUrl.URL + "FranchiseePayoutDetails";
        public static string CreatorBusiness = BaseUrl.URL + "CreatorBusiness";
        public static string CreatorHarmony = BaseUrl.URL + "CreatorHarmony";
        public static string CancelOrder = BaseUrl.URL + "CancelOrder";
        public static string CancelFranchiseeOrder = BaseUrl.URL + "CancelFranchiseeOrder";
        public static string ProductsReview = BaseUrl.URL + "ProductsReview";
        public static string Business = BaseUrl.URL + "Business";
        public static string FeedBack = BaseUrl.URL + "FeedBack";
        public static string FranchiseRequest = BaseUrl.URL + "FranchiseRequest";
        //public static string KaryonPointsRequestMobile = BaseUrl.URL + "KaryonPointsRequestMobile";
        public static string AddTempOpenOrder = BaseUrl.URL + "AddTempOpenOrder";
        public static string GenerateOpenOrder = BaseUrl.URL + "GenerateOpenOrder";
        public static string GetTempOpenOrder = BaseUrl.URL + "GetTempOpenOrder";
        public static string DeleteTempOpenOrder = BaseUrl.URL + "DeleteTempOpenOrder";
        public static string GetAssociateAddressDetails = BaseUrl.URL + "GetAssociateAddressDetails";
        public static string LoginFromMobileNumber = BaseUrl.URL + "LoginFromMobileNumber";
        public static string SendOtp = BaseUrl.URL + "SendOtp";
        public static string GetEventList = BaseUrl.URL + "GetEventList";
        public static string UploadAssociateImage = BaseUrl.URL + "UploadAssociateImage";
        public static string ApplyPromoCode = BaseUrl.URL + "PromoCode";
        public static string GetFranchiseeMenu = BaseUrl.URL + "GetFranchiseeMenu";
        public static string GetAssociateAddress = BaseUrl.URL + "GetAssociateAddress";
        public static string GetAssociateDashBoard2 = BaseUrl.URL + "GetAssociateDashBoard2";
        public static string DirectTree = BaseUrl.URL + "DirectTree";
        public static string PromoTracker = BaseUrl.URL + "PromoTracker";
        public static string PendingPromoTracker = BaseUrl.URL + "PendingPromoTracker";
        public static string AssociateUnpaidPayout = BaseUrl.URL + "AssociateUnpaidPayout";
        public static string AssociateRepurchasePayotReport = BaseUrl.URL + "AssociateRepurchasePayotReport";
        public static string GetPayoutDetails = BaseUrl.URL + "GetPayoutDetails";
        public static string GetPayoutDetailsStatement = BaseUrl.URL + "GetPayoutDetailsStatement";
        public static string GetPayoutDetailSummary = BaseUrl.URL + "GetPayoutDetailSummary";
        public static string UpdateBIDStatus = BaseUrl.URL + "UpdateBIDStatus";

        public static string Criteria = BaseUrl.URL + "Criteria";
        public static string GetBusiness = BaseUrl.URL + "GetBusiness";
        public static string AdminRenderMenu = BaseUrl.URL + "AdminRenderMenu";
        public static string GetSmartPointDetails = BaseUrl.URL + "GetSmartPointDetails";
        public static string GetOfferLedger = BaseUrl.URL + "GetOfferLedger";
        public static string GetOfferPointDetails = BaseUrl.URL + "GetOfferPointDetails";




    }
    public class ProcedureName
    {
        public static string? UpdateCustomerDispatchDetailExcel = "UpdateCustomerDispatchDetailExcel";
        public static string? InsTransactionHistory = "InsTransactionHistory";
        public static string? GetAllCategories = "GetAllCategories";
        public static string? GetProducts = "GetProducts";
        public static string? GetAssociateProfile = "GetAssociateList";
        public static string? Login = "Login";
        public static string? AdminLogin = "AdminLogin";
        public static string? Registration = "Registration";
        public static string? MembersCart = "MembersCart";
        public static string? ChangePassword = "ChangePassword";
        public static string? SendOtp = "SendOtp";
        public static string? VerifyOTP = "VerifyOTP";
        public static string? ForgotPassword = "ForgotPassword";
        public static string? GetMasterData = "GetMasterData";
        public static string? GetTreeMembers = "GetTreeMembers";
        public static string? GetSearchTreeData = "GetSearchTreeData";
        public static string? CheckParentForTreeView = "CheckParentForTreeView";
        public static string? GetUserFirst = "GetUserFirst";
        public static string? GetShoppingDashBoard = "GetShoppingDashBoard";
        public static string? GetAssociateDashBoard = "GetAssociateDashBoard";
        public static string? GetProductDetails = "GetProductDetails";
        public static string? GetProductDetailsForOfferPoint = "GetProductDetailsForOfferPoint";
        public static string? GetDirectList = "GetDirectList";
        public static string? GetDownlineList = "GetDownlineList";
        public static string? EwalletRequest = "EwalletRequest";
        public static string? UpdateAssociateProfile = "UpdateAssociateDetails";
        public static string? UpdateKYC = "UpdateKYC";
        public static string? FranchiseRegistration = "FranchiseRegistration";
        public static string? EwalletList = "EwalletList";
        public static string? ChangeSponsorLeg = "ChangeSponsorLeg";
        public static string? ChangeSponsorList = "ChangeSponsorList";
        public static string? CreateOrderTemp = "CreateOrderTemp";
        public static string? CreateOfferPointTemp = "CreateOfferPointTemp";
        public static string? GetTempOrder = "GetTempOrder";
        public static string? GetOfferPointTempOrder = "GetOfferPointTempOrder";
        public static string? DeleteOrderTemp = "DeleteOrderTemp";
        public static string? DeleteOfferPointTemp = "DeleteOfferPointTemp";
        public static string? DeleteCustomerTempOrder = "DeleteCustomerTempOrder";
        public static string? GenerateFranchiseOrder = "GenerateFranchiseOrder";
        public static string? GetFranchiseOrders = "GetFranchiseOrders";
        public static string? GetFranchiseStock = "GetFranchiseStock";
        public static string? FranchisewalletRequest = "FranchisewalletRequest";
        public static string? FranchiseWalletList = "FranchiseWalletList";
        public static string? GetFranchiseLedger = "GetFranchiseLedger";
        public static string? GetAssociateLedger = "GetAssociateLedger";
        public static string? GetFranchiseeBalance = "GetFranchiseeBalance";
        public static string? CreateCustomerOrderTemp = "CreateCustomerOrderTemp";
        public static string? GenerateCustomerOrder = "GenerateCustomerOrder";
        public static string? GetCustomerTempOrder = "GetCustomerTempOrder";
        public static string? InsertSetCapping = "SetCappingMaster";
        public static string? InsertSetId = "SetId";
        public static string? GetCustomerOrders = "GetCustomerOrders";
        public static string? GetDirectTeam = "GetDirectTeam";
        public static string? GetAssociatePayoutReport = "AssociatePayoutReport";
        public static string? UpdateProfileByAdmin = "UpdateProfileBySuperAdmin";
        public static string? PrintFranchiseOrder = "PrintFranchiseOrder";
        public static string? DispatchOrder = "DispatchOrder";
        public static string? PlaceOrder = "PlaceOrder";
        public static string? PlaceOrderForMobile = "PlaceOrderForMobile";
        public static string? PrintCustomerOrder = "PrintCustomerOrder";
        public static string? UpdatePickupFranchise = "UpdatePickupFranchise";
        public static string? UpdatePickupFranchiseForFranchiseOrder = "UpdatePickupFranchiseForFranchise";
        public static string? DispatchCustomerOrder = "DispatchCustomerOrder";
        public static string? DispatchCounterCollectOrder = "DispatchCounterCollectOrder";
        public static string? SkipOrders = "SkipOrders";
        public static string? SkipFranchiseOrder = "SkipFranchiseOrder";
        public static string? CounterPickup = "CounterPickup";
        public static string? GetFranchiseeDashBoard = "FranchiseeDashboard";
        public static string? PrintDispatchFranchiseOrder = "PrintDispatchFranchiseOrder";
        public static string? PrintDispatchParentFranchiseOrder = "PrintDispatchParentFranchiseOrder";
        public static string? GetWeeklyPayoutReport = "GetWeeklyPayout";

        public static string? GetTreeMembersAdmin = "GetTreeMembersAdmin";
        public static string? GetSearchTreeDataAdmin = "GetSearchTreeDataAdmin";
        public static string? CheckParentForTreeViewAdmin = "CheckParentForTreeViewAdmin";
        public static string? GetUserFirstAdmin = "GetUserFirstAdmin";
        public static string? ActivateMember = "ActivateMember";
        public static string? GetBusinessReport = "GetBusinessReport";
        public static string? GetStockDetails = "GetStockDetails";
        public static string? ManageAddress = "ManageAddress";
        public static string? GetBusinessReportDetails = "GetBusinessReportDetails";
        public static string? UpdateFranchiseProfile = "UpdateFranchiseByFranchise";
        public static string? GetFranchiseProfile = "GetFranchiseeList";
        public static string? FranchiseUpdateKYC = "FranchiseUpdateKYC";
        public static string? LoginByAdmin = "LoginByAdmin";
        public static string? GetAssociateWalletBalance = "GetAssociateWalletBalance";
        public static string? GetAssociateOrderAPI = "GetAssociateOrderAPI";
        public static string? GiveProductReview = "GiveProductReview";
        public static string? ProductReview = "ProductReview";
        public static string? GetReview = "GetProductReview";
        public static string? BinaryTreeForMobile = "BinaryTreeForMobile";
        public static string? GetAllIncomePayoutReport = "GetAllIncomeReport";
        public static string? SaveEnquiryInfo = "SaveEnquiryInfo";
        public static string? SaveFeedback = "SaveFeedback";
        public static string? FamilynWalletRequest = "FamilynWalletRequest";
        public static string? FamilynWalletList = "FamilynWalletList";
        public static string? FamilynWalletLedger = "FamilynWalletLedger";
        public static string? FranchiseeFamilynWalletRequest = "FranchiseeFamilynWalletRequest";
        public static string? FranchiseeFamilynWalletList = "FranchiseeFamilynWalletList";
        public static string? FranchiseeFamilynWalletLedger = "FranchiseeFamilynWalletLedger";
        public static string? CheckProduct = "CheckProduct";
        public static string? DeleteAccount = "DeleteAccount";
        public static string? GetFranchisePayoutDetails = "GetFranchisePayoutDetails";
        public static string? GetFranchisePayoutLedger = "GetFranchisePayoutLedger";
        public static string? GetPayoutLedger = "GetPayoutLedger";
        public static string? VerifyOTPForForgotId = "VerifyOTPForForgotId";
        public static string? RewardDetails = "webGetRewardAchiever";
        public static string? ViewFranchiseOrder = "ViewFranchiseOrder";
        public static string? ViewCustomerOrder = "ViewCustomerOrder";
        public static string? GetAssociateDetails = "GetAssociateList";
        public static string? BlockUnblockAssociate = "BlockUnblockAssociate";
        public static string? RegistrationByAdmin = "RegistrationByAdmin";
        public static string? BinaryTreeForWeb = "BinaryTreeForWeb";
        public static string? ProductsListForFranchisee = "ProductsListForFranchisee";
        public static string? CreatorBusiness = "CreatorBusiness";
        public static string? CancelOrder = "CancelOrder";
        public static string? CancelFranchiseeOrder = "CancelFranchiseeOrder";
        public static string? GetBusiness = "GetBusiness";
        public static string? GetFeedback = "GetFeedback";
        public static string? FranchiseeRequest = "FranchiseeRequest";
        public static string? CreateTempOpenOrder = "CreateCustomerOrderTempbyAdmin";
        public static string? GetCustomerTempOpenOrder = "GetCustomerOrderTempAdmin";
        public static string? DeleteCustomerTempOpenOrder = "DeleteCustomerTempOrderByAdmin";
        public static string? GenerateCustomerOpenOrder = "GenerateCustomerOrderAdmin";
        public static string? LoginFromOtp = "LoginFromOtp";
        public static string? ValidateInfo = "ValidateInfo";
        public static string? GetCartAmount = "GetCartAmount";
        public static string? UploadDispatchDetails = "UploadDispatchDetails";
        public static string? UpdateDispatchDetails = "UpdateDispatchDetails";
        public static string? QrCodeStatus = "QrCodeStatus";
        public static string? EventManage = "EventManage";
        public static string? ConfirmEventBooking = "ConfirmEventBooking";
        public static string? CheckPromoCode = "CheckPromoCode";
        public static string Eway = "Eway";
        public static string AddEWayToken = "AddEWayToken";
        public static string BindMenuMaster = "BindMenuMaster";
        public static string Criteriadashboard = "Criteriadashboard";
        public static string GetCriteriaDetails = "GetCriteriaDetails";
        public static string GetTermsLog = "GetTermsLog";
        public static string AutoLogin = "AutoLogin";
        public static string GetOrderDetails = "GetOrderDetails";
        public static string GetPackPurchase = "GetPackPurchase";
        public static string GetFranchiseeDispatchList = "SP_GetFranchiseeDispatchList";
        public static string GetTodaysDispatch = "GetTodaysDispatch";
        public static string GetAssociateDashboardNew = "GetAssociateDashboardNew";
        public static string UpdateBID = "UpdateBID";
        public static string GetDirectListTree = "GetDirectListTree";

        public static string UpdateRank = "UpdateRank";

        public static string getCriteria = "getCriteria";
        public static string getUnpaidPayout = "getUnpaidPayout";
        public static string Rep_AssociatePayoutReport = "Rep_AssociatePayoutReport";
        public static string GetOfferPointLedger = "GetOfferPointLedger";
        public static string Rep_OfferPointsDetails = "Rep_OfferPointsDetails";
        public static string CreateRazOrder = "CreateRazOrder";
        public static string SaveWebHookRazorpay = "SaveWebHookRazorpay";
        public static string GetPaymentDetailRaz = "GetPaymentDetailRaz";
        public static string ActiveMemberDetails = "ActiveMemberDetails";
        public static string RankUpdateDetails = "RankUpdateDetails";
        public static string? Redeemofferpoint = "RedeemOfferpoint";
        public static string? GetAddress = "GetAddress";

    }
    public class CompanyDetails
    {
        public static string? ComapanyName = "Karyon Organic Pvt Ltd";
        public static string? Address1 = "409, Balewadi highstreet,";
        public static string? Address2 = "Baner,";
        public static string? Address3 = "Pune-411045, Maharashtra(India).";
        public static string? Phone = "(Tel) 020-41228973, +91 96040 97799";
        public static string? WhatsUp = "+91 7720077965";
        public static string? Email = "info@karyon.organic";
        public static string? Website = "http://karyon.organic/";
        public static string? GSTIN = "27AAHCK4739Q1ZQ";
    }

    public class CommonResponse
    {
        public string? Message { get; set; }
        public int ResponseCode { get; set; }
    }
    public class ResponseModel
    {
        public string? Body { get; set; }
    }
    public class RequestModel
    {
        public string? Body { get; set; }

    }

    public static class FileManagement
    {
        public static async Task<string> WriteFiles(this IFormFile files, string FolderName)
        {
            bool isSaveSuccess = false;

            string obj = "";
            string final = "";


            string fileName;
            try
            {
                var extension = "." + files.FileName.Split('.')[files.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension;
                var pathBuilt = Path.Combine("C:\\inetpub\\vHost\\karyon.organic\\wwwroot\\", FolderName);

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }
                var path = Path.Combine("C:\\inetpub\\vHost\\karyon.organic\\wwwroot\\", FolderName, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }

                obj = "/" + FolderName + "/" + fileName;

                isSaveSuccess = true;
            }
            catch (Exception ex)
            {
                obj = ex.Message;
                return obj;
            }
            return obj;
        }

    }
    public class UploadFileRequest
    {
        public string? FolderName { get; set; }
    }
    public class UploadFileResponse
    {
        public string? FilePath { get; set; }
        public string? Message { get; set; }
        public int? Status { get; set; }
    }
    public class Pushnotification
    {
        public void Notification(string DeviceId, string msgBody, string title, string deviceType, string notificationType)
        {
            string postbody = "";
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAq1Fm-zU:APA91bFkjX1Fla75FTZYCzG71C1ZGvlR8N_DypotqnowIVPiTx7sAli22Fk9gfX8rGoyTNYKKaqHYSBN1wL1tNiLRxVn9leFvhDNecr191ddzGbX7lEak9pQuKPkm-ZqvA7SeoiaAvdK"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "735805111093"));
            tRequest.ContentType = "application/json";
            if (deviceType == "Android")
            {
                var message = new
                {
                    to = DeviceId,
                    collapse_key = "green",
                    notification = new
                    {
                        body = msgBody,
                        title = title,
                    },
                    data = new
                    {
                        type = notificationType,
                        message = msgBody,
                    }
                };
                postbody = JsonConvert.SerializeObject(message).ToString();
            }
            else
            {

                var message = new
                {
                    to = DeviceId,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = msgBody,
                        title = title,
                        badge = "12"
                    },
                };
                postbody = JsonConvert.SerializeObject(message).ToString();
            }



            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;

            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                dynamic json = JsonConvert.DeserializeObject(sResponseFromServer);
                            }
                    }
                }
            }


        }
    }
    //public static class SessionExtensions
    //{
    //    public static void SetObjectAsJson(this ISession session, string key, object value)
    //    {
    //        session.SetString(key, JsonConvert.SerializeObject(value));
    //    }

    //    public static T GetObjectFromJson<T>(this ISession session, string key)
    //    {
    //        var value = session.GetString(key);
    //        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    //    }

    //}
    public class PAYTMGateway
    {
        //For Testing 
        public static string PAYTM_TEST_MID = "GRxxJu40963644697222";
        public static string PAYTM_TEST_MERCHANT_KEY = "tJTE3IVwyaqstvfY";
        public static string PAYTMTEST_URL_Status = "https://securegw-stage.paytm.in/v3/order/status";
        public static string WebSiteLocal = "WEBSTAGING";

        //For Live
        public static string PAYTM_LIVE_MERCHANT_KEY = "FiCMG3BkY8C&efYn";
        public static string PAYTM_LIVE_MID = "zSCfyY72432419176004";
        public static string PAYTMLive_URL_Status = "https://securegw.paytm.in/v3/order/status";
    }

}