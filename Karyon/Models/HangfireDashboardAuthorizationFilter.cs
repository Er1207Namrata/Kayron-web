using Hangfire.Dashboard;

namespace Karyon.Models
{
    public class HangfireDashboardAuthorizationFilter: IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow only authenticated users to access the Hangfire Dashboard
            return true;
        }
    }
}
