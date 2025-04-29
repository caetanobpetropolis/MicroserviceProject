using Amazon.Runtime;
using Amazon.SQS;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Credentials = new BasicAWSCredentials(
    builder.Configuration["AWS:AccessKey"],
    builder.Configuration["AWS:SecretKey"]
);

builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonSQS>();

builder.Services.AddSingleton<SqsService>();

builder.WebHost.UseUrls("http://+:80");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
