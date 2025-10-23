using WebApplication2.Services;
using WebApplication2.Services2;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// add custom services...
builder.Services.AddSingleton<IGuidService, GuidService>();

builder.Services.AddSingleton<IRandomInt, RandomInt>();

builder.Services.AddSingleton<PhoneService>();

var app = builder.Build();

//middleware

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync($"test MW1 GO! {Environment.NewLine}");

//    await next();

//    await context.Response.WriteAsync($"test MW1 RETURN ! {Environment.NewLine}");
//});

//app.UseMyMiddleware();

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync($"FINISH! {Environment.NewLine}");
//});

//app.Map("/admin", adminApp =>
//{
//    adminApp.Use(async (context, next) =>
//    {
//        await context.Response.WriteAsync($"test ADMIN MW GO! {Environment.NewLine}");

//        await next();

//        await context.Response.WriteAsync($"test ADMIN MW RETURN ! {Environment.NewLine}");
//    });
//});

//app.MapWhen(context => context.Request.Query.ContainsKey("admin"), adminApp =>
//{
//    adminApp.Run(async context =>
//    {
//        await context.Response.WriteAsync("Accesso condizionato all'area admin");
//    });
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync($"Hello from MW2 GO ! {Environment.NewLine}");
//    await next();
//    await context.Response.WriteAsync($"Hello from MW2 RETURN {Environment.NewLine}");
//});

app.Run();
