using Microsoft.EntityFrameworkCore;
using PaperShop.BackPaper.DataAccess;
using PaperShop.BackPaper.DataAccess.RepoInterfaces;
using PaperShop.BackPaper.DataAccess.Repositories;
using PaperShop.BackPaper.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<PaperService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaperRepository, PaperRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PaperShop API", Version = "v1" });
});
builder.Services.AddDbContext<PaperShopContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaperShop API V1");
        c.RoutePrefix = string.Empty;});
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("AllowAllOrigins");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PaperShopContext>();
    context.Database.Migrate(); 
    PaperShopContext.Seed(context); 
}


app.Run();


