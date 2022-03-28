using BlazorChat.Server.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//agregamos el servicio de signalR
builder.Services.AddSignalR();
//se establece el tipo de mimica o interaccion
builder.Services.AddResponseCompression(
    options => options.MimeTypes = ResponseCompressionDefaults 
    .MimeTypes
    .Concat
    (new[] { "application/octet-stream" })
    
    );

var app = builder.Build();
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
//agregamos lo que seria el mapeo entre servidor y cliente
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToFile("index.html");

app.Run();
