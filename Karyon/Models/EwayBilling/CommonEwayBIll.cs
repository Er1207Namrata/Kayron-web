namespace Karyon.Models.EwayBilling
{
    public class CommonEwayBIll
    {
#if DEBUG
        //public static string? username = "testeway@mastersindia.co";
        //public static string? password = "Client!@#Demo987";
        //public static string? client_id = "TMDIIbTwzkCQWFTHpA";
        //public static string? client_secret = "BZpqtmFRIkfIrgjOfhyzQjLX";
        public static string? username = "lalit@karyon.organic";
        public static string? password = "Lalit@1234";
        public static string? client_id = "CctUEPQLprdxpzNFve";
        public static string? client_secret = "dwEbvNPqGIYXVB7HJBfK0LmB";

#else
        public static string? username = "lalit@karyon.organic";
        public static string? password = "Lalit@1234";
        public static string? client_id = "CctUEPQLprdxpzNFve";
        public static string? client_secret = "dwEbvNPqGIYXVB7HJBfK0LmB";
#endif
        //public static string? username = "testeway@mastersindia.co";
        //public static string? password = "Client!@#Demo987";
        //public static string? client_id = "TMDIIbTwzkCQWFTHpA";
        //public static string? client_secret = "BZpqtmFRIkfIrgjOfhyzQjLX";        


        public static string? grant_type = "password";
        public static string? userGstin = "27AAHCK4739Q1ZQ";
        public static string? legal_name_of_consignor = "KARYON ORGANIC PRIVATE LIMITED";
        public static string? address1_of_consignor = "409, Balewadi highstreet";
        public static string? address2_of_consignor = "Baner";
        public static string? place_of_consignor = "Pune";
        public static int pincode_of_consignor = 411045;
        public static string? state_of_consignor = "MAHARASHTRA";
        public static string? actual_from_state_name = "MAHARASHTRA";
        public static string? StateName = "MAHARASHTRA";
        public static string? state_code = "27";
        public static long phone_number = 7720077965;
        public static string? email = "info@karyon.organic";
    }
}
