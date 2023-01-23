using Azure.Identity;
using AzureTest.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAzureAppConfiguration();
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(
            "Endpoint=https://my-app-config-test.azconfig.io;Id=+BWw-l9-s0:TxDnTC1wxlVzSAq1XoMi;Secret=r0aDBtDd/7R6W+gLXWsmwRRMqikLyh/N7AppaltM2bA=")
        .ConfigureKeyVault(kv =>
        {
            kv.SetCredential(new DefaultAzureCredential()); 
            kv.SetSecretRefreshInterval("MyApp:Settings:Sentinel",TimeSpan.FromSeconds(2));
        })
        .ConfigureRefresh(refresh =>
        {
            refresh.Register("MyApp:Settings:Sentinel", refreshAll: true)
                .SetCacheExpiration(TimeSpan.FromSeconds(3));
            
        });
});

builder.Services.AddSingleton(builder.Configuration.GetTestConfig());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAzureAppConfiguration();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();