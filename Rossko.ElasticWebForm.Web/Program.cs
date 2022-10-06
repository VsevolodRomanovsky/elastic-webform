using BlazorDateRangePicker;
using Microsoft.EntityFrameworkCore;
using Rossko.ElasticWebForm.Web.Data;
using Rossko.ElasticWebForm.Web.Interfaces;
using Rossko.ElasticWebForm.Web.Services;
using Rossko.ElasticWebForm.Data.Model;
using Rossko.ElasticWebForm.Application.ElasticSearch;
using Rossko.ElasticWebForm.Application.Database;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IRequestService<OemCatalogRequest>, OemCatalogService>();

builder.Services.AddDbContext<OemCatalogDbContext>(options => 
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOemCatalogDbContext>(options => options.GetService<OemCatalogDbContext>()!);
builder.Services.AddElasticsearch(builder.Configuration);
builder.Services.AddSyncfusionBlazor(configure: options => { options.IgnoreScriptIsolation = true; });
builder.Services.AddScoped<StateContainer>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDateRangePicker(config =>
{
    config.Attributes = new Dictionary<string, object>
    {
        { "class", "form-control form-control-sm" }
    };
});

builder.Services.AddTransient<IElasticSearchClient, ElasticSearchClient>();
builder.Services.AddTransient<IDbService, DbService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
