using CupOnlineAPI.Context;
using CupOnlineAPI.Repositories;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<CupRepository>();
builder.Services.AddScoped<SearchParamRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddControllers();
builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(p =>
    {
        //p.AllowAnyOrigin();
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.SetIsOriginAllowed(origin => true); // allow any origin, kanske inte denna
                                              //p.AllowCredentials();
    });
});
// IEmailService implementation using MailKit
builder.Services.AddMailKit(optionBuilder =>
{
    optionBuilder.UseMailKit(new MailKitOptions()
    {
        Server = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"],
        Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]),
        Account = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"],
        Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"],
        SenderEmail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"],
        SenderName = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"],
        // Set it to TRUE to enable ssl or tls, FALSE otherwise
        Security = true
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();
