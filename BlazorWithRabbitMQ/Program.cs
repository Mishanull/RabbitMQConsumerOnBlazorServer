using FileDAO;
using Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RabbitMQ.Client.Logging;
using RabbitMqConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// builder.Services.AddHostedService<RabbitMQConsumer>();
builder.Services.AddScoped<IProductDAO, ProductFileDao>();
builder.Services.AddScoped<ProductContext>();
builder.Services.AddSingleton<StateContainer.StateContainer>();
builder.Services.AddSingleton<Consumer>();
builder.Services.AddHostedService(sp=>sp.GetService<Consumer>());
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();