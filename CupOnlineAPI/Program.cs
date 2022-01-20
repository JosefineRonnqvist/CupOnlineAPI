using CupOnlineAPI.Context;
using CupOnlineAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<CupRepository>();
builder.Services.AddScoped<SportRepository>();
builder.Services.AddScoped<CupByIdRepository>();
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
