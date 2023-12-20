using Data;
using Microsoft.AspNetCore.Mvc;

// Services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<VolLiefdeRepository>();

var app = builder.Build();

// Middleware 
app.UseStaticFiles();
app.UseRouting();

// Repository for demonstration.
var repository = app.Services.GetRequiredService<VolLiefdeRepository>();

// Endpoints
var quotesGroup = app.MapGroup("/bnb/quotes/");

quotesGroup.MapGet("/", () => repository.GetAll());
quotesGroup.MapGet("/{name}", (string name) =>
{
    if (repository.ContainsQuoteBy(name))
    {
        return Results.Ok(repository.Get(name));
    }

    return Results.NotFound();
});

// New C# 12 language feature: Default lambda parameters.
quotesGroup.MapPost("/add/{name}", (string name, QuoteModel quote) =>
{
    if (!repository.ContainsQuoteBy(name))
    {
        repository.Add(name, quote);
        return Results.Ok();
    }

    return Results.BadRequest("Only one quote is allowed per participant.");
});

// Adding an endpoint filter can also be done by using a lambda function.
quotesGroup.MapDelete("/delete/{name}", (string name) =>
{
    if (repository.ContainsQuoteBy(name))
    {
        repository.Delete(name);
        return Results.Ok();
    }

    return Results.NotFound();
}).AddEndpointFilter(ValidationHelper.ValidateDeleteRequest);

// App

app.Run();

// Object defenitions.

class ValidationHelper
{
    internal static async ValueTask<object?> ValidateDeleteRequest(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var name = context.GetArgument<string>(0);

        if (string.Equals(name, "Olof", StringComparison.OrdinalIgnoreCase))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["name"] = ["You are not allowed to remove the famous Olof quote, shame!"]
            });
        }

        return await next(context);
    }
}