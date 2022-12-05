using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();


#region GraphQl
builder.Services
           .AddGraphQLServer();


var whiteList = builder.Configuration.GetSection("WhiteList");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins();
        });
});

#endregion

var app = builder.Build();

app.UseCors(builder => builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials()
    );
app.MapGraphQL();

app.Run();