using FoodOrdering;
using FoodOrdering.Components;
using FoodOrdering.Data;
using FoodOrdering.Interop.TeamsSDK;
using FoodOrdering.Repositories;
using FoodOrdering.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Fast.Components.FluentUI;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using FoodOrdering.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var config = builder.Configuration.Get<ConfigOptions>();
builder.Services.AddTeamsFx(config.TeamsFx.Authentication);
builder.Services.AddScoped<MicrosoftTeams>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddHttpClient("WebClient", client => client.Timeout = TimeSpan.FromSeconds(600));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);

//
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<GraphService, GraphService>();
builder.Services.AddScoped<LibraryConfiguration, LibraryConfiguration>();
builder.Services.AddScoped<TeamsNotificationService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped(typeof(IOrderRepository<>), typeof(OrderRepository<>));
builder.Services.AddScoped(typeof(IProductsRepository<>), typeof(ProductsRepository<>));
builder.Services.AddScoped(typeof(IChatMessagesRepository<>), typeof(ChatMessagesRepository<>));
builder.Services.AddScoped(typeof(ICommentsRepository<>), typeof(CommentsRepository<>));

builder.Services.AddHttpClient("api", c => {
    c.BaseAddress = new Uri("https://localhost:44302");
});

builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});
//

var app = builder.Build();

app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    endpoints.MapControllers();
});

app.MapHub<ChatHub>("/chathub");

app.Run();