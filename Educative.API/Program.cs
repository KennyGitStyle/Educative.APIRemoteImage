using Educative.API.Config;
using Educative.API.Extension;
using Educative.API.Filter;
using Educative.API.Middleware;
using Educative.Infrastructure.Data;
using Educative.Infrastructure.Interface;
using Educative.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddApiVersioningService();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ICacheResponseService, CacheResponseService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddDbContextExtension(builder.Configuration);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddLazyCache();
builder.Services.AddConfigureService();
builder.Services.AddScoped<HttpDbExceptionFilter>();

builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(configurePolicy: options =>
{
  options.WithMethods("GET", "POST", "PUT", "DELETE");
    options.AllowAnyOrigin();
    options.WithOrigins(); // client url
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(opts => {
    
  });
}

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.UseDbInitializer();


await app.RunAsync();
