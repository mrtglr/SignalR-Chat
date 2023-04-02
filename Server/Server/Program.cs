using Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        builder.WithOrigins("http://localhost:4200/")
            .AllowCredentials()
            //.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains());
});

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapHub<ChatHub>("/chatsocket");

app.MapControllers();

app.Run();
