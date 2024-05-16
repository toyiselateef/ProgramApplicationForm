
using ProgramApplicationForm.Infrastructure;
using ProgramApplicationForm.Application;
using ProgramApplicationForm.Api.Validators; 
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Azure.Cosmos;
using ProgramApplicationForm.Infrastructure.DAL;

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddSingleton<CosmosClient>(provider =>
{
    return new CosmosClient(builder.Configuration["CosmosDB:Endpoint"], builder.Configuration["CosmosDB:Key"], new CosmosClientOptions
    {
        ConnectionMode = ConnectionMode.Direct,
        MaxRetryAttemptsOnRateLimitedRequests = 9,
        MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(30)
    });
});

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<QuestionValidator>());


//dI
builder.Services.AddInfrastructureServices(builder.Configuration).AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();
await dbContext.InitializeAsync();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
