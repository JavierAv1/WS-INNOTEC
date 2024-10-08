﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = $"WS-INNOTEC API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = $"API for WS-INNOTEC - {description.ApiVersion}",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Support",
                    Email = "support@wsinnotec.com",
                    Url = new Uri("https://wsinnotec.com/support")
                }
            });
        }
    }
}
