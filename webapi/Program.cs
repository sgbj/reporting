using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Components.Web;
using webapi;
using webapi.Reports;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ProductService>();
builder.Services.AddRazorComponents();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var api = app.MapGroup("/api");

api.MapGet("inventory", () => new RazorComponentResult<InventoryReport>());

api.MapGet("sales", async (IServiceProvider serviceProvider, ILoggerFactory loggerFactory) =>
{
    await using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);
    var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
    {
        var htmlComponent = await htmlRenderer.RenderComponentAsync<SalesReport>();
        return htmlComponent.ToHtmlString();
    });
    return Results.Content(html, "text/html");
});

app.Run();
