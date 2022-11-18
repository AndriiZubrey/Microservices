using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KnowYourPostTaxes.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TaxContext>(ops => 
    ops.UseNpgsql(builder.Configuration.GetSection("DbConnection").Value));

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TaxContext>();
    context.Database.EnsureCreated();
}

app.MapPost("/tax", (TaxVM tax, TaxContext context) =>
{
    context.Taxes.Add(new Tax(tax.Email, tax.Password));
    context.SaveChanges();
    return Results.Ok();
});

app.MapGet("/tax", (TaxContext context) => 
    context.Taxes.ToList());

app.MapGet("/tax/{id}", (int id, TaxContext context) =>
{
    var tax = context.Taxes.Find(id);
    return tax == null ? Results.NotFound() : Results.Ok(tax);
});

app.MapPost("/tax/exists", (TaxVM tax, TaxContext context) =>
{
    var exists = context.Taxs.Any(u => u.Email == tax.Email && u.Password == tax.Password);
    return Results.Ok(exists);
});

app.MapDelete("/tax/{id}", (int id, TaxContext context) =>
{
    var tax = context.Taxes.Find(id);
    if (tax == null) return Results.NotFound();
    context.Taxes.Remove(tax);
    context.SaveChanges();
    return Results.Ok();
});

app.Run();


class TaxVM
{
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("password")] public string Password { get; set; }
}
