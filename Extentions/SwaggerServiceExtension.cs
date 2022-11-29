using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace OrderService.Extentions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Order Service API",
                Version = "v1",
                Description = "ASP.NET 6 API",
                TermsOfService = null,
                Contact =
                    new OpenApiContact
                    {
                        Name = "Order Service",
                        Email = "v.kjosevski@gmail.com"
                    }
            });  
            
            c.SchemaFilter<EnumSchemaFilter>();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("v1/swagger.json", "v1");
            c.DocumentTitle = "Order Service swagger documentation";
            c.DocExpansion(DocExpansion.None);
        });

        return app;
    }
}

internal class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) return;

        schema.Enum.Clear();
        Enum.GetNames(context.Type)
            .ToList()
            .ForEach(n => schema.Enum.Add(new OpenApiString(n)));
    }
}
