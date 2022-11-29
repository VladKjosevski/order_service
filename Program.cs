using FluentValidation.AspNetCore;
using OrderService.Data;
using OrderService.Extentions;
using OrderService.OrderService;
using OrderService.Validators;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson()
     .AddFluentValidation(options =>
     {
         options.ImplicitlyValidateChildProperties = true;
         options.ImplicitlyValidateRootCollectionElements = true;

         options.RegisterValidatorsFromAssemblyContaining<OrderRequestValidator>();
     }).AddJsonOptions(opt =>
     {
         opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
     }); 
builder.Services.AddSwaggerDocumentation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProductItems, ProductItems>();
builder.Services.AddTransient<IOrderServices, OrderServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwaggerDocumentation();
app.Run();
