using IMS.Data;
using IMSClassLibrary.Context;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using IMSClassLibrary.Repos;
using IMSClassLibrary.repos;
using Microsoft.EntityFrameworkCore;
using Blazored.SessionStorage;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<SystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SystemDbContext"));
}
);
builder.Services.AddTransient<DepartmentRepository>();
builder.Services.AddTransient<InternProjectRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<UserProfileRepository>();
builder.Services.AddTransient<ProjectRepository>();
builder.Services.AddTransient<CommentRepository>();
builder.Services.AddTransient<ProfileModuleRepository>();
builder.Services.AddTransient<EmailRepository>();
builder.Services.AddTransient<ModuleRepository>();
builder.Services.AddTransient<ProfileRepository>();
builder.Services.AddTransient<NotificationService>();
builder.Services.AddTransient<Authorisation>();
builder.Services.AddBlazoredSessionStorage();



builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(300);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.Use(async delegate (HttpContext context, Func<Task> next)
{
    //this throwaway session variable will "prime" the setstring() method
    //to allow it to be called after the response has started
    var tempkey = Guid.NewGuid().ToString(); //create a random key
    context.Session.Set(tempkey, Array.Empty<byte>()); //set the throwaway session variable
    context.Session.Remove(tempkey); //remove the throwaway session variable
    await next(); //continue on with the request
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
