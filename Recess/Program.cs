using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Recess.Helpers;
using Recess.Providers;
using Recess.Queries;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);



GoogleCredential cred = GoogleCredential.FromFile("D:\\Recess\\Recess\\Recess\\service-acc.json");


var jsonString = File.ReadAllText("D:\\Recess\\Recess\\Recess\\service-acc.json");
var defaultApp = FirebaseApp.Create(new AppOptions()
{
    Credential = cred,
});

builder.Services.AddHttpClient();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHealthChecks();


builder.Services
           .AddGraphQLServer()
           .AddAuthorization()
                .AddQueryType(x => x.Name("Query"))
                      .AddTypeExtension<UserQueries>()
                   .AddFiltering()
                   .AddSorting()
                   .AddHttpRequestInterceptor(
                       (context, executor, builder, ct) =>
                       {
                           var identity = new ClaimsIdentity();
                           identity.AddClaim(new Claim(ClaimTypes.Country, "tr"));
                           context.User.AddIdentity(identity);
                           return new ValueTask(Task.CompletedTask);
                       })
                   .SetRequestOptions(_ => new HotChocolate.Execution.Options.RequestExecutorOptions { ExecutionTimeout = TimeSpan.FromMinutes(3) });


builder.Services.AddSingleton(_ => new FirestoreProvider(
    new FirestoreDbBuilder
    {
        ProjectId = "recess-7a138",
        JsonCredentials = jsonString,  // <-- service account json file
    }.Build()
));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins();
        });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler("/error");
app.UseRouting();

app.UseHttpsRedirection();

app.UseCors(builder => builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials()
    );

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
    endpoints.MapBananaCakePop("/ui");
});


HttpHelper.Configure(((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IHttpContextAccessor>());

app.MapGet("/", () => "Audit isn't required.");
app.Run();
