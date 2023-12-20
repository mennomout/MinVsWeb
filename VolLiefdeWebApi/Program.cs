using Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<VolLiefdeRepository>();
builder.Services.AddControllers()
    // This also adds an input formatter that allows XML as the body of a request.
    .AddXmlSerializerFormatters();

var app = builder.Build();

app.MapControllers();

app.Run();
