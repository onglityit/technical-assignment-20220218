using Microsoft.Extensions.Azure;
using v2cshtml.Middleware.Interface;
using v2cshtml.Middleware.Method;
using v2cshtml.Services;
using v2cshtml.Services.Interface;
using v2cshtml.Services.Method;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["darren-azurite:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["darren-azurite:queue"], preferMsi: true);
});
builder.Services.AddScoped<IValidateFileService, ValidateFileService>();
builder.Services.AddScoped<IStorageManager, StorageManager>();
builder.Services.AddScoped<IUploadFileService, UploadFileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
