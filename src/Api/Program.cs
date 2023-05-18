using Api.Extensions;
using Api.IoC;
using Hellang.Middleware.ProblemDetails;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
builder.Services.AddSwagger();
builder.Services.AddVersioning();
builder.Services.AddApiProblemDetails();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(cors =>
        cors
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseProblemDetails();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();