using DotNetBank;
using Microsoft.AspNetCore.Http.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAccountDatabase,AccountDatabase>();
builder.Services.AddTransient<IBankDatabase, BankDatabase>();
builder.Services.AddTransient<IBranchDatabase,BranchDatabase>();
builder.Services.AddTransient<ITranscationDatabase, TranscationDatabase>();
builder.Services.AddTransient<IAccountUpdateService,AccountUpdateService>();
builder.Services.AddTransient<IValidations, Validations>();
builder.Services.AddTransient<Iifsc, ifsc>();




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
