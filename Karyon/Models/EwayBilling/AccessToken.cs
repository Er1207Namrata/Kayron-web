namespace Karyon.Models.EwayBilling
{
    public class AccessTokenRequest
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? client_id { get; set; }
        public string? client_secret { get; set; }
        public string? grant_type { get; set; }
    }
    public class AccessTokenResponse
    {
        public string? access_token { get; set; }
        public int expires_in { get; set; }
        public string? token_type { get; set; }
        public string? error { get; set; }
        public string? error_description { get; set; }
    }
   
}
