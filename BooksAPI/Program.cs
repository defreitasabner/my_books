using Microsoft.EntityFrameworkCore;
using BooksAPI.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BookConnection");
builder.Services.AddDbContext<MyBooksContext>(
    opts => opts.UseLazyLoadingProxies().UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
        )
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
