using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var connstring = builder.Configuration.GetConnectionString("Default");
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/fruits",async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    var fruit = form["name"].ToString();

    if(string.IsNullOrWhiteSpace(fruit) || fruit.Length > 100)
    {
        return Results.BadRequest("Enter a name up to 100 Charachters.");
    }

    await using var conn= new NpgsqlConnection(connstring);
    await conn.OpenAsync();

    await using var command = new NpgsqlCommand(
        "INSERT INTO fruits (name) VALUES (@fruit)",
    conn);

    command.Parameters.AddWithValue("fruit", fruit);

    await command.ExecuteNonQueryAsync();
    
    return Results.Text($"Saved {fruit} successfully!");

});

app.MapGet("/getfruit", async (HttpContext context) =>
{
    await using var conn = new NpgsqlConnection(connstring);
    await conn.OpenAsync();

    var cmd = new NpgsqlCommand("SELECT *FROM fruits", conn);
    var reader = await cmd.ExecuteReaderAsync();

    var results = "";

    while (await reader.ReadAsync())
    {
        results += $"{reader["id"]}- {reader["name"]}<br>";
    }

    return Results.Content(results , "text/html");
});


app.Run();
