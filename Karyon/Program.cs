using Hangfire;
using Karyon.Controllers;
using Karyon.Hubs;
using Karyon.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var hangfireConnectionString = builder.Configuration["ConnectionString"];
builder.Services.AddHangfire(configuration => configuration.UseSqlServerStorage(hangfireConnectionString));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

}
app.UseCors(policy => policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials());
//app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
});
app.UseHangfireServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHangfireDashboard();
    endpoints.MapControllers();
});
BackgroundJob.Enqueue<JobsController>(service => service.GenerateEinvoice());
RecurringJob.AddOrUpdate<JobsController>("GenerateEinvoice", service => service.GenerateEinvoice(), Cron.MinuteInterval(30));

BackgroundJob.Enqueue<JobsController>(service => service.ValidateGST());
RecurringJob.AddOrUpdate<JobsController>("ValidateGST", service => service.ValidateGST(), Cron.MinuteInterval(2));

BackgroundJob.Enqueue<JobsController>(service => service.UpdateFranchiseOrderStatus());
RecurringJob.AddOrUpdate<JobsController>("UpdateFranchiseOrderStatus", service => service.UpdateFranchiseOrderStatus(), Cron.MinuteInterval(2));

app.UseAuthorization();
app.MapHub<ChatHub>("/chatHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
