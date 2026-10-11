# FruitDemo

A simple web application built with ASP.NET Core, Npgsql, PostgreSQL, and HTMX.

## What It Does
- Adds fruits to a PostgreSQL database.
- Retrieves and displays all stored fruits.
- Updates the page using HTMX without refreshing the browser.

## What's Inside
- C# / ASP.NET Core Minimal APIs — handles HTTP requests.
- Npgsql — connects C# to PostgreSQL and executes SQL queries.
- PostgreSQL — stores fruits in a `fruits` table with `id` and `name` columns.
- HTML / HTMX — provides the form and displays results dynamically.

## Database Connection
- The application reads the PostgreSQL connection string named `Default` from `appsettings.json`.
- The connection string specifies the database host, port, database name, username, and password.
- Npgsql uses this connection string to connect to PostgreSQL and execute SQL commands.

## Endpoints
- `POST /fruits` — inserts a new fruit into the database.
- `GET /getfruit` — retrieves all fruits and returns them as HTML.