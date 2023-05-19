using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Web.ApiClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<IClient, Client>((service, client) =>
{
    var uri = builder.Configuration.GetSection("Api:Uri").Value;
    client.BaseAddress = new Uri(uri);
    if (service.GetService(typeof(Client)) is not Client c) return;
    c.JsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    c.JsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    c.JsonSerializerSettings.Converters.Add(new StringEnumConverter());
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();