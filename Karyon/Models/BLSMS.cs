using System.Net;

namespace Karyon.Models
{
    public class BLSMS : Menu
    {
        public static string SendSMS(string mobile, string msg, string templateid)
        {
            try
            {
                //string strUrl = SMSCredientials.URL+"user="+ SMSCredientials.UserName+ "&key="+SMSCredientials.ClientCode+ "&mobile="+mobile+ "&message="+msg+ "&senderid="+SMSCredientials.SenderId+ "&accusage=1";
                string strUrl = SMSCredientials.URL+"user="+ SMSCredientials.UserName+ "&key="+SMSCredientials.ClientCode+ "&mobile=+91"+mobile+ "&message="+msg+ "&senderid="+SMSCredientials.SenderId+ "&accusage=1"+ "&entityid="+SMSCredientials.DLTNo+ "&tempid="+templateid;
                WebRequest request = HttpWebRequest.Create(strUrl);
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
                return dataString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
